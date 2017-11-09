using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006readonlyAndconstUse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string Value = "Hello";
        //static readonly string Value = "Hello";//同上面一行
        private void Form1_Load(object sender, EventArgs e)
        {
            //=== const 部分 ====
            string setTalk = Value;
            Console.WriteLine(setTalk);
            //實際上邊一時為
            Console.WriteLine("Hello");
      
            //=== Readonly 部分 2 : 呼叫
            //ReadOnlyValue  = 100;無法指定賦予值

            //參考型別 的不可改變
            myClass itemA = new myClass();

            //參考型別建構式 - 可以賦予內部的readonly屬性值
            myClass itemB = new myClass(1234);
        }

        //=== Readonly 部分 1 : 宣告
        //基底型別 的不可改變
        readonly int ReadOnlyValue = 100;

    public class myClass {
            //Field
            readonly int score = 10;

            /// <summary>
            /// 建構式 - 賦予Readonly值
            /// </summary>
            /// <param name="input"></param>
            public myClass(int input)
            {
                this.score = input;
            }

            /// <summary>
            /// 建構式 - 無參數
            /// </summary>
            /// <param name="input"></param>
            public myClass()
            {
            }
        }
    }
}
