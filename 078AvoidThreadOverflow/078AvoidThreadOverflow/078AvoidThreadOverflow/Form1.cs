using System;
using System.Threading;
using System.Windows.Forms;

namespace _078AvoidThreadOverflow
{
    /// <summary>
    /// 078. 應避免線程數量過多 (線程使用過多，會造成系統緩慢，因為線程之間會進行切換的動作)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                Thread t = new Thread(() =>
                {
                    int j = 1;
                    while (true)
                    {
                        j++;
                    }
                });
                t.IsBackground = true;
                t.Start();
            }
            Thread.Sleep(5000);
            Thread t201 = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("T201 正在執行");
                }

            }) ;
            t201.Start();
            Console.WriteLine();
            
        }
    }
}
