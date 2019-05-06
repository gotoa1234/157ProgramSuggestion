using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _087WPF_WinFormThreadDiff
{
    /// <summary>
    /// 087. 區分 WPF 和 Winform 的線程差別 (Winform 用 BeginInvoke ; WPF 必須用 Dispatcher.BeginInvoke)
    ///      ※兩者畢竟有差異
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Thread mainThread;

        public bool CheckAccess()
        {
            return mainThread == Thread.CurrentThread;
        }

        public void VerifyAccess()
        {
            if (false == CheckAccess())
            {
                Console.WriteLine("不同的執行緒");
            }
        }

        /// <summary>
        /// Winform 的按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            mainThread = Thread.CurrentThread;
            Task t = new Task(() =>
            {
                while (true) {
                    if (false == CheckAccess())
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.Text = $@"{DateTime.Now.ToString()}";
                        }));
                    }
                    else
                    {
                        this.Text = $@"{DateTime.Now.ToString()}";
                    }
                    Thread.Sleep(1000);
                }
            });

            t.ContinueWith((task) =>
            {
                try
                {
                    task.Wait();
                }
                catch (AggregateException ex)
                {
                    foreach (Exception inner in ex.InnerExceptions)
                    {
                        Console.WriteLine($@"異常類型 : {inner.GetType()}");
                        Console.WriteLine($@"來自 : {inner.Source}");
                        Console.WriteLine($@"異常內容 : {inner.Message}");
                    }
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
            t.Start();

        }
    }
}
