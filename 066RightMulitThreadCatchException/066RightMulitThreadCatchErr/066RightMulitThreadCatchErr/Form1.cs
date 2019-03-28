using System;
using System.Threading;
using System.Windows.Forms;

namespace _066RightMulitThreadCatchErr
{
    /// <summary>
    /// 066 正確捕捉多執行緒的異常
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ErrorThreadCatchException();
            RightThreadCatchException();
        }

        /// <summary>
        /// 錯誤的執行緒拋出例外
        /// </summary>
        public void ErrorThreadCatchException()
        {
            try
            {
                Thread t = new Thread((ThreadStart)delegate
                {
                    throw new Exception("多執行緒異常");
                });
                t.Start();
            }
            catch (Exception ex)
            {
                //不會跳到此行，上面發生錯誤時直接結束
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }

        /// <summary>
        /// 正確的執行緒處理例外
        /// </summary>
        public void RightThreadCatchException()
        {
            Thread t = new Thread((ThreadStart)delegate
            {
                try
                {
                    throw new Exception("多執行緒異常");
                }
                catch (Exception ex)
                {
                        //不會跳到此行，上面發生錯誤時直接結束
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            });
            t.Start();

        }
    }
}
