using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _032ThinkingGeneric
{
    /// <summary>
    /// 032 總是優先考慮使用泛型 (Generic 具有3個特性 1.可重用 2.安全 3.高效率，故應優先考慮使用)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1、2. 可重用性、安全性 <T> 
            IntClass itemA = new IntClass();
            itemA.items.Add(10);
            itemA.items.Add(20);

            StringClass itemB = new StringClass();
            itemB.items.Add("10");
            itemB.items.Add("20");

            //使用泛型可以節省代碼，並且明確知道型別
            GenericClass<int> itemT_A = new GenericClass<int>();
            itemT_A.items.Add(10);
            GenericClass<string> itemT_B = new GenericClass<string>();
            itemT_B.items.Add("10");

            //3. 高效率 
            //100萬筆資料 泛型只需 1.2 秒左右 ，Object 雖然也可以接資料，但最後要轉型為指定的型別(封裝、解封裝)耗費時間較多
            //            Object 花費約7.6 秒，效率差了6倍
            Stopwatch sw = new Stopwatch();
            sw.Start();
            GenericClass<int> itemT_C = new GenericClass<int>();
            for (int i = 0; i < 100000000; i++)
            {
                itemT_C.items.Add(i);
            }
            foreach (var item in itemT_C.items)
            {
                var temp = item;
            }
            sw.Stop();
            textBox1.Text += $@"泛型花費時間(毫秒) : {sw.Elapsed.TotalSeconds}"+"\r\n";


            sw.Restart();
            ObjectClass itemO_C = new ObjectClass();

            for (int i = 0; i < 100000000; i++)
            {
                itemO_C.items.Add(i);
            }
            foreach (var item in itemO_C.items)
            {
                var temp = (int)item;
            }
            sw.Stop();
            textBox1.Text += $@"Object 花費時間(毫秒) : {sw.Elapsed.TotalSeconds}  "+"\r\n";

        }

        /// <summary>
        /// 接收【整數】型別的Class
        /// </summary>
        public class IntClass
        {
            public List<int> items { get; set; } = new List<int>();
        }

        /// <summary>
        /// 接收【字串】型別的Class
        /// </summary>
        public class StringClass
        {
            public List<string> items { get; set; } = new List<string>();
        }

        /// <summary>
        /// 泛型 可接受各種型別的Class
        /// </summary>
        public class GenericClass<T>
        {
            public List<T> items { get; set; } = new List<T>();
        }

        /// <summary>
        /// Object 可接受各種型別的Class
        /// </summary>
        public class ObjectClass
        {
            public List<object> items { get; set; } = new List<object>();
        }
    }
}
