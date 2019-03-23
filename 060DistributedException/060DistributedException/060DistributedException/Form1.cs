using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace _060DistributedException
{
    /// <summary>
    /// 060. 重新引發異常時使用Inner Exception (可以使用 .Data 拋出例外)
    /// 通常錯誤訊息都會包裝後給用戶前端看，而內部的Error會記錄到Log中，此時可以善加利用 .Data.Add語法
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                InnerExceptionError();

            }
            catch (Exception ex)
            {
                //寫入Log 給系統看
                Console.WriteLine($@"寫入本地Log => {ex.Message}");
                //回傳給用戶看
                Console.WriteLine($@"回傳給用戶看 => {ex.Data["PackageInfo"]}");
            }

        }

        public void InnerExceptionError()
        {
            try
            {
                int a = 0;
                int b = 1;
                int c = b / a;
            }
            catch (Exception ex)
            {
                ex.Data.Add("PackageInfo", "錯誤的運算請檢察運算試");
                throw ex;
            }
        }
        


    }
}
