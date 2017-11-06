using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _005intSetNull
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //可為Null的型別變數 可以直接對型別變數存放值 (隱式存值)
            //Nullable<int> score = null;//完整定義
            int? score = null;//語法糖，簡略定義
            int j = 0;
            score = j;//Nullable<int>  = int :可以這樣寫

            //但可為Null的型別變數 不可被型別變數存放值

            int? score2 = 123;
            int j2 = 0;
            //j2 = score2; 該行出錯   ==> int = Nullable<int>  :不可以這樣寫

            //正確做法
            j2 = score2 ?? 0;

            //如同
            j2 = score2.HasValue == true ? 0 :j2;

            //如同
            if (score2.HasValue == true)
            {
                j2 = score2.Value;
            }
            
             
        }

    }
}
