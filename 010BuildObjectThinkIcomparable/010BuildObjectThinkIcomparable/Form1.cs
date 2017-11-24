using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _010BuildObjectThinkIcomparable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 010. 建議物件時需要考慮是否實作比較器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            List<Salary> salaryList = new List<Salary>();
            salaryList.Add(new Salary() { BaseSalary = 22000, Country = "B-Taiwan" , Bonus = 50});
            salaryList.Add(new Salary() { BaseSalary = 18000, Country = "C-China" , Bonus = 70});
            salaryList.Add(new Salary() { BaseSalary = 9000, Country = "A-India" , Bonus =60});
            salaryList.Add(new Salary() { BaseSalary = 31000, Country = "D-DUSA" ,Bonus =55});

            //排序做法1. IComaparable
            salaryList.Sort();//Sort預設內建的排序功能，我們覆寫IComparable
            //印出排序結果
            foreach(var obj in salaryList)
            {
                Console.WriteLine(obj.BaseSalary);
            }
            //排序做法2. IComparer => 非預設性(可擴充的) .sort() 排序
            salaryList.Sort(new BonusComparer());
            //排序做法3. LinQ
            var temp = salaryList.OrderBy(o => o.Bonus).ToList();

            //效能比較
            List<Salary> salaryListTestA = new List<Salary>();
            List<Salary> salaryListTestB = new List<Salary>();
            List<Salary> salaryListTestC = new List<Salary>();
            Random Dice = new Random();
            int DiceVar = 0;//骰子變數
            string resultMessage = "";
            for (int i = 0; i < 10000000; i++)
            {
                DiceVar = Dice.Next(0, 10000000);
                //兩者存放資料相同才有比較意義
                salaryListTestA.Add(new Salary() { Bonus = DiceVar });
                salaryListTestB.Add(new Salary() { Bonus = DiceVar });
                salaryListTestC.Add(new Salary() { Bonus = DiceVar });
            }
            Stopwatch sw = Stopwatch.StartNew();


            //排序做法1. IComparer => 非預設性(可擴充的) .sort() 排序
            //計時開始
            sw.Restart();
            salaryListTestA.Sort(new BonusComparer());
            sw.Stop();//計時結束
            resultMessage += string.Format("IComparer => 非預設性(可擴充的) .sort() 排序: {0}{1}", sw.Elapsed.ToString(), "\r\n");


            //排序做法2. LinQ 轉ToList
            sw.Restart();
            List<Salary> tempB = salaryListTestB.OrderBy(o => o.Bonus).ToList();
            sw.Stop();//計時結束
            resultMessage += string.Format("排序做法2. LinQ 轉ToList: {0}{1}", sw.Elapsed.ToString(), "\r\n");
            
            //排序做法3. LinQ 不轉換
            sw.Restart();
            salaryListTestC.OrderBy(o => o.Bonus);
            sw.Stop();//計時結束
            resultMessage += string.Format("排序做法3. LinQ 不轉換: {0}{1}", sw.Elapsed.ToString(), "\r\n");

            textBox1.Text += resultMessage;
        }

        /// <summary>
        /// 薪資類別
        /// </summary>
        public class Salary : IComparable
        {
            /// <summary>
            /// 國家
            /// </summary>
            public string Country { get; set;}
            /// <summary>
            /// 基本薪資
            /// </summary>
            public int BaseSalary { get; set; }
            /// <summary>
            /// 紅利
            /// </summary>
            public int Bonus { get; set; }

            public int CompareTo(object obj)
            {
                Salary staff = obj as Salary;
                #region 這段如同下面 BaseSalary.CompareTo(staff.BaseSalary)

                if (BaseSalary > staff.BaseSalary)
                    return 1;
                else if (BaseSalary == staff.BaseSalary)
                    return 0;
                else
                    return -1;
                #endregion
                //return BaseSalary.CompareTo(staff.BaseSalary);//當被呼叫 .sort()時優先以BaseSalary資料做排序

                //return Bonus.CompareTo(staff.Bonus);//當被呼叫 .sort()Bonus
            }

        }

        /// <summary>
        /// 建立一個自訂義的比較器 
        /// </summary>
        public class BonusComparer : IComparer<Salary>
        {
            /// <summary>
            /// 針對Salary 的 Bonus 進行比較
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(Salary x, Salary y)
            {
                //將Bonus變為Sort()的參考物件
                return x.Bonus.CompareTo(y.Bonus);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
