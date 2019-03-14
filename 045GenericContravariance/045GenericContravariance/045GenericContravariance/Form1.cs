using System;
using System.Windows.Forms;

namespace _045GenericContravariance
{
    /// <summary>
    /// 045為泛型類型參數指定逆變 (泛型中 in 為逆變 out 為協變)
    /// ※逆變 : 不和諧的變化，由衍生類別轉為父類類別 
    /// ※共變，斜變 : 和諧的變化，由父類類別轉為衍生類別 
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Progreammer p = new Progreammer() { Name = "Mike" };
            Manager m = new Manager { Name = "Steve" };

            // Prgorame (孩子) :  Employee (父親)  : IMyComparable<in T> (祖父)
            // Manager  (孩子) :  Employee (父親)  : IMyComparable<in T> (祖父)
            // 因此將p 帶入 等於 衍生類別轉為父類
            Test(p, m);
        }

        /// <summary>
        /// 建立一個泛型函式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1">逆變的型態</param>
        /// <param name="t2"></param>
        static void Test<T>(IMyComparable<T> t1, T t2)
        {

        }

        /// <summary>
        /// 增加逆變的代碼 in
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IMyComparable<in T>
        {
            int Compare(T other);
        }

        public class Employee : IMyComparable<Employee>
        {
            public string Name { get; set; }

            public int Compare(Employee other)
            {
                return Name.CompareTo(other.Name);
            }
        }

        public class Progreammer : Employee, IComparable<Progreammer>
        {
            public int CompareTo(Progreammer other)
            {
                return Name.CompareTo(other.Name);
            }
        }

        public class Manager : Employee
        {

        }

    }
}
