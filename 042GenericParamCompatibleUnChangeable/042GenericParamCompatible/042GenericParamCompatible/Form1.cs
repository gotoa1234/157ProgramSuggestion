using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _042GenericParamCompatible
{
    /// <summary>
    /// 042 使用泛型參數兼容泛型接口的不可變性(理解共變)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1. 理解共變 
            GetAEmployee("Louis");

            //2. 共變應用 ※該問題已經被微軟修復
            //ISalary<Programmer> s = new BaseSalaryCounter<Programmer>();
            //PrintSalary(s);
        }    

        /// <summary>
        /// 理解共變(中國通常翻譯為協變 ，英文 Covariance)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Employee GetAEmployee(string name)
        {
            Console.WriteLine(" 我是雇員: " + name);
            //Programmer 繼承 Employee 故可以回傳Programmer 視為 Employee
            return new Programmer() { Name = name };
        }

        #region .net Framework 4.0 有常見的共變做法

        /// <summary>
        /// 薪資介面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        interface ISalary<T>
        {
            void Pay();
        }

        /// <summary>
        /// 薪資的支付類 - 包含支付Pay()方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class BaseSalaryCounter<T> : ISalary<T>
        {
            public void Pay()
            {
                Console.WriteLine("在 BaseSalaryCounter 類別中 Pay() 的方法");
            }
        }

        /// <summary>
        /// 父類別
        /// </summary>
        public class Employee
        {
            public string Name { get; set; }

        }

        /// <summary>
        /// 子類 : 繼成於Employee
        /// </summary>
        public class Programmer : Employee
        {
    
        }

        /// <summary>
        /// 子類 : 繼成於Employee
        /// </summary>
        public class Manage : Employee
        {

        }

        #endregion

    }
}
