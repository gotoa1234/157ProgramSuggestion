using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _038ClosuresTrap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1.不如預期的範例
            Console.WriteLine(@"不如預期的範例");
            new ClosureTrap().ClosureActionError();
            Console.WriteLine();
            //2. 1.的原型
            Console.WriteLine(@"1.的原型");
            new ClosureTrap.ClosureActionErrorOringen();
            Console.WriteLine();
            //3. 如預期的範例
            Console.WriteLine(@"如預期的範例");
            new ClosureTrap().ClosureActionRight();
            Console.WriteLine();
            //4. 3.的原型
            Console.WriteLine(@" 3.的原型");
            new ClosureTrap.ClosureActionRightOringen();
            Console.WriteLine();
        }

        /// <summary>
        /// 委派的閉包陷阱
        /// </summary>
        public class ClosureTrap
        {
            /// <summary>
            /// Step1:我們建立5個Action 預期可以顯示每個Int元素值
            /// </summary>
            public void ClosureActionError()
            {
                //建立Action - 此為閉包 : 將5個Action 建立，並且顯示值
                List<Action> lists = new List<Action>();
                for (int i = 0; i < 5; i++)
                {
                    Action t = () =>
                    {
                        Console.Write($@"{i} ");
                    };
                    lists.Add(t);
                }
                //顯示為 5 5 5 5 5 
                foreach (Action t in lists)
                {
                    t();
                }
            }

            /// <summary>
            /// Step2:基於Step1，ClosureActionError()的原型
            /// </summary>
            public class ClosureActionErrorOringen
            {
                /// <summary>
                /// 實際上ClosureActionError 等於以下Fucntion
                /// </summary>
                public ClosureActionErrorOringen()
                {
                    List<Action> lists = new List<Action>();
                    //在Function內For相當於用傳址呼叫，故每次都使用相同的變數進行委派
                    TempClass tempClass = new TempClass();
                    for (tempClass.i = 0; tempClass.i < 5; tempClass.i++)
                    {
                        Action t = tempClass.TempFunc;
                        lists.Add(t);
                    }
                    //顯示 : 5 5 5 5 5
                    foreach (Action t in lists)
                    {
                        t();
                    }

                }
                
                /// <summary>
                /// 顯示類型
                /// </summary>
                private class TempClass
                {
                    public int i;
                    public void TempFunc()
                    {
                        Console.Write($@"{i} ");
                    }
                }
            }

            /// <summary>
            /// Step3:正確做法 ，每次new個新變數，建立儲存
            /// </summary>
            public void ClosureActionRight()
            {
                //建立Action - 此為閉包 : 將5個Action 建立，並且顯示值
                List<Action> lists = new List<Action>();
                for (int i = 0; i < 5; i++)
                {
                    int temp = i;
                    Action t = () =>
                    {
                        Console.Write($@"{temp} ");
                    };
                    lists.Add(t);
                }
                //顯示為 0 1 2 3 4 
                foreach (Action t in lists)
                {
                    t();
                }
            }

            /// <summary>
            /// Step4:基於Step1，ClosureActionRight()的原型
            /// </summary>
            public class ClosureActionRightOringen
            {
                /// <summary>
                /// ClosureActionRight 等於以下Fucntion
                /// </summary>
                public ClosureActionRightOringen()
                {
                    List<Action> lists = new List<Action>();                   
                    for (int i  = 0; i < 5; i++)
                    {
                        TempClass tempClass = new TempClass();
                        tempClass.i = i;
                        Action t = tempClass.TempFunc;
                        lists.Add(t);
                    }
                    //顯示 : 0 1 2 3 4 
                    foreach (Action t in lists)
                    {
                        t();
                    }

                }

                /// <summary>
                /// 顯示類型
                /// </summary>
                private class TempClass
                {
                    public int i;
                    public void TempFunc()
                    {
                        Console.Write($@"{i} ");
                    }
                }
            }
        }
    }
}
