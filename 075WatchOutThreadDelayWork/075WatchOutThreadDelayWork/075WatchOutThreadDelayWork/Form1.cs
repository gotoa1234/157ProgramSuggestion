using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _075WatchOutThreadDelayWork
{
    /// <summary>
    /// 075. 警惕線程不會立即啟動 (線程是非同步的，並沒有正確順序，除非代碼內部進行控制)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string _getResult = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            //01. Thread 啟動執行順序沒有同步，會被打亂
            var textBoxItem =  this.Controls.Find("textBox1", false).FirstOrDefault() as TextBox;
            new WorkingThread2(textBoxItem);

            
        }


        /// <summary>
        /// 01. Thread 啟動執行順序沒有同步，會被打亂
        /// </summary>
        public class WorkingThread
        {
            private int _id = 0;

            public WorkingThread(TextBox item)
            {
                List<Thread> tList = new List<Thread>();

                for (int i = 0; i < 10; i++ , _id++)
                {
                    Thread t = new Thread(() =>
                    {
                        _getResult += $@"{Thread.CurrentThread.Name} {_id} " + Environment.NewLine;
                    });
                    tList.Add(t);
                    t.Name = $@"Thread :{ i }";
                    t.IsBackground = true;
                    t.Start();
                }

                foreach (var t in tList)
                {
                    t.Join();
                }
                item.Text = _getResult;
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 02. 調整成具有順序(同步)的Thread
        /// </summary>
        public class WorkingThread2
        {
            private int _id = 0;

            public WorkingThread2(TextBox item)
            {
                for (int i = 0; i < 10; i++ , _id++)
                {
                    Thread t = new Thread(() =>
                    {
                        _getResult += $@"{Thread.CurrentThread.Name} {_id} " + Environment.NewLine;
                    });

                    t.Name = $@"Thread :{ i }";
                    t.IsBackground = true;
                    t.Start();
                    t.Join();
                }
                item.Text = _getResult;
                Console.ReadLine();
            }
        }


        

    }
}
