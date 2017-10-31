using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _001UseTheStringCorrectly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //效率差的字串相加 ※ 一個封裝(Boxing)經行使用都會在進行解封裝(unBoxing) 

            string exampleA = "string" + 123;//-----------較慢， 123 需要經過封裝物件 簡易概念: String -> object -> string 

            string exapmleB = "string" + 123.ToString();//較快, 456透過 ToString() 使用了StringBuilder，少了封裝的效能消耗

            /*  封裝影響的是記憶體的操作
                1. stringBuilder 在 以16字元為單位 ，當超過時才會進行記憶體配置
                2. System.String 操作則是封裝解封裝，使用多少字元給予多少記憶體。但性能耗費很多             
             */
 
            //字串相加正確的作法有以下2種

            //1. StringBuilder
            StringBuilder correctlyString = new StringBuilder("string");
            correctlyString.Append(123);

            string A = "123456789012345";
            var temp = System.Text.ASCIIEncoding.Unicode.GetByteCount(A);

            //2. String.Format (StringBuilder擴充，內部還是用StringBuilder)
            string correctlyStringB = string.Format("{0}{1}", "string", 123);

        }
    }
}
