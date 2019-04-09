using System;
using System.Threading;
using System.Windows.Forms;

namespace _076WatchOutThreadPriorityLevel
{
    /// <summary>
    /// 076 警惕線程的優先級 ※優先級越高，執行率會相對變多，
    ///                        高優先級特性: 運行時間短，快速進入線程
    ///                       
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Example();
        }

        class Example
        {
            
            /// <summary>
            /// 範例 兩個線程
            /// </summary>
            public Example()
            {
                long t1Num = 0;
                long t2Num = 0;
                CancellationTokenSource cts = new CancellationTokenSource();

                Thread t1 = new Thread(() =>
                {
                    while (true && !cts.Token.IsCancellationRequested)
                    {
                        t1Num++;
                    }
                });

                t1.IsBackground = true;
                t1.Priority = ThreadPriority.Lowest;//較低的優先級
            
                Thread t2 = new Thread(() =>
                {
                    while (true && !cts.Token.IsCancellationRequested)
                    {
                        t2Num++;
                    }
                });
                t2.IsBackground = true;
                t2.Priority = ThreadPriority.Highest;//較高的優先級

                //兩個線程進行工作
                t1.Start();
                t2.Start();

                Thread.Sleep(1000);//休息一秒，看兩個線程的執行變化
                cts.Cancel();//停止所有線程，可以發現優先級較大的執行率會較多

                Console.WriteLine($@"t1 : {t1Num}");
                Console.WriteLine($@"t2 : {t2Num}");
            }

        }
    }
}
