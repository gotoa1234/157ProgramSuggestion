using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _008VoidEnumUseExplicitVariable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //我們要避免給Enum顯示的值，如果在連續性(不重複)的累加資料中。
            //除非有客製化的需求，有重複性的值在Enum中

            // valueTemp  = 3  ==>基於 Tuesday =2 + 1    
            int valueA = (int)MyWeek.ValueA;
            // valueTemp2 = 4  ==>基於 Tuesday =2 + 2  (前一個Enum為 ValueTemp)
            int valueB = (int)MyWeek.ValueB;
            // valueTemp3 = 4  ==>基於 Wednesday =3 + 1  
            int valueC = (int)MyWeek.ValueC;
            // True 
            MyWeek week = MyWeek.ValueA;
            bool valueAResult = week == MyWeek.Wednesday ? true : false;

            //非連續性的應用
            int today = (int)CustomerMyweek.Saturday;
            switch (today)
            {
                case 0:
                    Console.Write("每周的痛苦期");
                    break;
                case 1:
                    Console.Write("每周的放空期");
                    break;
                case 2:
                    Console.Write("每周的快樂期");
                    break;

            }
        }


        public enum MyWeek
        {

            Monday = 1,
            Tuesday = 2,
            ValueA,
            ValueB,
            Wednesday = 3,
            ValueC,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }

        /// <summary>
        /// 標準的Enum 定義 - 不該提供顯示的值 
        /// </summary>
        public enum MyWeekRight
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        /// <summary>
        /// 如果有以下非連續性的Enum ，提供顯示的值才是明確的做法
        /// </summary>
        public enum CustomerMyweek
        {
            Monday = 0,
            Tuesday = 0,
            Wednesday = 1,
            Thursday = 1,
            Friday = 2,
            Saturday = 2,
            Sunday = 2

        }
    }
}
