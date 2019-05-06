using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _086ParallelExceptionHandle
{
    /// <summary>
    /// 086. Parallel 中的異常處理 (使用 ConcurrentQueue<Exception>(); 將Parallel 的例外事件存放進去)
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
                var parallelExceptions = new ConcurrentQueue<Exception>();

                //這邊測試跑兩次
                Parallel.For(0, 2, (i) =>
                {
                    try
                    {
                        throw new InvalidOperationException("Parallel 拋出Excepition");
                    }
                    catch (Exception ex)
                    {
                        parallelExceptions.Enqueue(ex);
                    }

                    //每個Parallel 的子Task ，最多parallelExceptions.Count = 1 ，有1時表示有例外拋出
                    if (parallelExceptions.Count > 0)
                    {
                        throw new AggregateException(parallelExceptions);
                    }
                });

            }
            catch (AggregateException ex)
            {
                foreach (Exception item in ex.InnerExceptions)
                {
                    Console.WriteLine($@"異常類型 : {item.InnerException.GetType()}");
                    Console.WriteLine($@"來自 : {item.InnerException.Source}");
                    Console.WriteLine($@"異常內容 : {item.InnerException.Message}");

                }
            }
            Console.WriteLine("Parallel Throw Exception Finish");
        }
    }
}
