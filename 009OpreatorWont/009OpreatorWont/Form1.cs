using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _009OpreatorWont
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //兩區域的各別總薪資
            salary_Unit_Total regionTaipei = new salary_Unit_Total() {  salary_Total = 22000};
            salary_Unit_Total regionNewTaipei = new salary_Unit_Total() { salary_Total = 30200 };
            //相加內部的屬性取得總值
            int regionTotalA = regionTaipei.salary_Total + regionNewTaipei.salary_Total;
            //使用擴展後的operator 使得類別可以用 +號進行運算
            int regionTotalB = regionTaipei + regionNewTaipei;
        }

        public class salary_Unit_Total
        {
            /// <summary>
            /// 區域總薪水
            /// </summary>
            public int salary_Total { get; set; }


            /// <summary>
            /// 擴展operator 使得類別可以用 + 號進行運算
            /// </summary>
            /// <param name="A"></param>
            /// <param name="B"></param>
            /// <returns></returns>
            public static int operator +(salary_Unit_Total A, salary_Unit_Total B)
            {
                //將兩個類別的內部屬性總值相加 回傳
                return A.salary_Total + B.salary_Total ;
            }
        }

    }
}
