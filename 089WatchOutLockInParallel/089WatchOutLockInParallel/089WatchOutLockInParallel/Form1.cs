using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _089WatchOutLockInParallel
{
    /// <summary>
    /// 089. 在並行方法中謹慎使用Lock (如果在Parallel 中，所有項目皆要lock運行，不如執行用同步，效能較強，差了約75%)
    /// 效能比較:
    /// Parallel + Lock 耗費時間: 24.3342902 
    /// Sync 耗費時間: 18.9725347 
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            ParallelExample(1000);
            sw.Stop();
            textBox1.Text = ($@"Parallel + Lock 耗費時間: {sw.Elapsed.TotalSeconds} {Environment.NewLine}");

            sw.Restart();
            SyncExample(1000);
            sw.Stop();
            textBox1.Text += ($@"Sync 耗費時間: {sw.Elapsed.TotalSeconds} {Environment.NewLine}");

        }

        /// <summary>
        /// 非同步
        /// </summary>
        /// <param name="EXCUTE_TIMES"></param>
        public void ParallelExample(int EXCUTE_TIMES)
        {
            object lockdata = new object();
            SampleClass sample = new SampleClass();
            Parallel.For(0, EXCUTE_TIMES, (i) =>
            {
                lock (lockdata)
                {
                    sample.SimpleAdd();
                }
            });
            Console.WriteLine(sample.SomeCount);

        }

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="EXCUTE_TIMES"></param>
        public void SyncExample(int EXCUTE_TIMES)
        {
            SampleClass sample = new SampleClass();
            for (int i = 0; i < EXCUTE_TIMES; i++)
            {
                sample.SimpleAdd();

            }
            Console.WriteLine(sample.SomeCount);
        }

        public class SampleClass
        {
            public long SomeCount { get; private set; }

            public void SimpleAdd()
            {
                try//耗費效能 每個Exception 都是浪費大量資源
                {
                    int a = 0;
                    int b = 0;
                    int c = a / b;
                }
                catch (Exception ex)
                {

                }

                SomeCount++;
            }

        }
    }
}
