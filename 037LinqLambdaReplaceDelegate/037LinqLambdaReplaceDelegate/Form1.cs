using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _037LinqLambdaReplaceDelegate
{
    /// <summary>
    /// 037 使用Lambda表達式代替方法和匿名方法(當函式簡短，可以用隱匿方法來簡化代碼 , Lambda是最佳方案)
    /// 案例 :
    /// 委派加法簡化成Lambda的步驟
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LambdaReduceCodeExample();
        }

        /// <summary>
        /// 1. 委派加法簡化成Lambda的步驟
        /// </summary>
        public void LambdaReduceCodeExample()
        {
            //1. 簡化成Func<int ,int> 型別 
            Func<int, int, int> add = new Func<int, int, int>(delegate (int i, int j)
            {
                return i + j;
            });
            //2. 更進一步簡化
            Func<int, int, int> add2 = delegate (int i, int j)
            {
                return i + j;
            };
            //3. 使用Lambda 最佳簡化
            Func<int, int, int> add3 = (i, j) =>
            {
                return i + j;
            };

        }

    }
}
