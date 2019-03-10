using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _033.AvoidGenericSetStatic
{
    /// <summary>
    /// 033. 避免在泛型類別中建立靜態成員 (知道泛型內靜態變數為各別獨立存在)
    /// 本章節分兩個部分，1.理解泛型內靜態變數各別獨立 2.泛型內靜態成員共同使用
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

          GenericStaticExample2();

        }

        /// <summary>
        /// 1. 理解泛型內靜態變數各別獨立
        /// </summary>
        public void GenericStaticExample()
        {

            //以下建立 2個int型別 1個string型別
            MyList<int> list1 = new MyList<int>();
            MyList<int> list2 = new MyList<int>();
            MyList<string> list3 = new MyList<string>();

            Console.WriteLine(MyList<int>.Count);
            Console.WriteLine(MyList<string>.Count);
            //輸出結果為 2 , 1 
            //可以得知泛型中靜態成員也是各別獨立的。在泛型內使用靜態成員需要注意這個特性。
        }

        /// <summary>
        /// 2. 泛型內靜態成員共同使用
        /// </summary>
        public void GenericStaticExample2()
        {
            Console.WriteLine( MyList2.WorkingMethod<int>());
            Console.WriteLine(MyList2.WorkingMethod<string>());
            Console.WriteLine(MyList2.WorkingMethod<int>());
            //共用一個靜態變數 ，輸出為 1,2,3
        }

        /// <summary>
        /// 泛型類別 - 個別獨立靜態變數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class MyList<T>
        {
            public static int Count { get; set; }

            public MyList()
            {
                Count++;
            }
        }

       

        /// <summary>
        /// 泛型類別 - 共用靜態變數
        /// </summary>
        class MyList2
        {
            public static int count;

            /// <summary>
            /// 必須使用函式的方式
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static int WorkingMethod<T>()
            {
               count++;
               return count;
            }
        }
            
    }
}