using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _061AvoidFinallyWriteDirtyCode
{
    /// <summary>
    /// 061. 避免在Finally 內撰寫無用程式碼 (例外處理的finally不是實質上的最後執行，視情況而會發生無效)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = ExecuteFunc();
            Console.WriteLine(result);
        }

        public static int ExecuteFunc()
        {
            int result = 0;
            try
            {
                result = 1;
                return result;
            }
            catch 
            {
                return 2;
            }
            finally
            {
                //可以發現在Try已經回傳值，並不會最終亦執行finally
                result = 3;
            }
        }
    }
}
