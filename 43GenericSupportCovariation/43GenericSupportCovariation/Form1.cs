using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _43GenericSupportCovariation
{
    /// <summary>
    /// 043 讓接口中的泛型參數支持協變 (使用Out 讓泛型支持協變、共變)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1. 讓代碼支持協變
            ISalary<Programmer> s = new BaseSalaryCounter<Programmer>();
            ISalary<Manage> t = new BaseSalaryCounter<Manage>();
            PrintSalary(s);
            PrintSalary(t);

            //2. 為何IList可以用List 進行協變，是因為 =>  IEnumerable<out T>  本身就使用out T 
            IList<Programmer> iList = new List<Programmer>();
            PrintPersonName(iList);
        }

        static void PrintPersonName(IEnumerable<Employee> persons)
        {
        }

        /// <summary>
        /// 042中. 原本不支持這樣呼叫 但是ISalary<out T>  我們增加了Out 使得可以使用斜變
        /// </summary>
        /// <param name="s"></param>
        void PrintSalary(ISalary<Employee> s)
        {
            s.Pay();
        }

        #region .net Framework 4.0 有常見的共變做法

        /// <summary>
        /// 薪資介面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        interface ISalary<out T>
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
