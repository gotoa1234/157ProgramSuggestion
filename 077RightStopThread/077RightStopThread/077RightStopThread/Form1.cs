using System;
using System.Threading;
using System.Windows.Forms;
namespace _077RightStopThread
{
    /// <summary>
    /// 077 正確停止線程的方法 (停止線程應該使用CancellationTokenSource )
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //設定一個取消線程的Token
            CancellationTokenSource cts = new CancellationTokenSource();

            Thread t = new Thread(() =>
            {
                while (true)
                {
                    //判斷是否被呼叫 Cancel ，當被呼叫Cancel時 此值為True 
                    if (cts.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("線程被終止!");
                        break;
                    }
                    Console.WriteLine(DateTime.Now.ToString());
                    Thread.Sleep(1000);
                }
            });
            t.Start();
            Console.ReadLine();

            //觸發Token.Cancel 時的事件
            cts.Token.Register(() =>
            {
                Console.WriteLine("此線程進行中止");
            });

            //將Token.IsCancellationRequested 設為 true 且觸法 Register
            cts.Cancel();
        }
    }
}
