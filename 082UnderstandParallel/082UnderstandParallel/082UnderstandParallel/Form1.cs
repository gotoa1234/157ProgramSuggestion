using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _082UnderstandParallel
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 082 : Parallel 簡化但不等於Task默認行為 (Parallel雖然是非同步，但在Parallel執行時 (For,Foreach,Invoke)裡面的項目全部完成，才繼續主線程 ; Task 則否)
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //會順利結束
            InfiniteLoopForTask();

            //永遠鎖住
            InfiniteLoopForParallel();
        }

        /// <summary>
        /// 範例: 執行了無窮迴圈的Task
        /// 結果: 會輸出 => Task結束
        /// </summary>
        public void InfiniteLoopForTask()
        {
            Task t = new Task(() =>
            {
                while (true)
                {

                }
            });

            t.Start();
            Console.WriteLine("Task 結束");

        }

        /// <summary>
        /// 範例: 執行了無窮迴圈的Parallel 
        /// 結果: 會輸出 => 永遠當機
        /// </summary>
        public void InfiniteLoopForParallel()
        {
            Parallel.For(0, 1, (i) =>
            {
                while(true)
                {

                }
            });
            
            Console.WriteLine("Parallel 結束");

        }
    }
}
