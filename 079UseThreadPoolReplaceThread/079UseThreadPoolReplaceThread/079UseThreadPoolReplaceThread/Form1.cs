using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _079UseThreadPoolReplaceThread
{
    /// <summary>
    /// 079. 使用ThreadPool 或 BackgroundWorker 代替Thread ()
    /// Thread 使用空間來源 (共)
    /// 1. Thread 占用 700 Byte  相當於0.0007 mb 又相當於 0.7 kb
    /// 2. Thread Enviroment Block (TEB) 直行環境區塊，記錄每個執行緒的運行記錄 約4Kb
    /// 3. Use Mode Stack (UMS) 用戶模式堆疊，記錄使用者於執行緒叫用的參數，記錄使用 約1024KB
    /// 4. Kernel Mode Stack (KMS) 核心模式堆疊，將UMS重要資料複製至此 約12KB
    /// 
    /// Thread 的生命週期 :
    /// step1 : 進入內核模式
    /// step2 : CPU資訊複製到內核中
    /// step3 : 系統使用SpinLock(只有一個Thread可以進行資源的使用),並確定下個執行的Thread
    /// step4 : step3中如果下個Thread沒有與當前Thread在同一個進程(Process) 則會進行虛擬地址交換
    /// step5 : Thread 進入上下文模式(同步、單執行緒工作)
    /// step6 : 離開內核模式
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

        /// <summary>
        /// 001. ThreadPool 的範例 (讓ThreadPool管理資源分配，而非使用Thread自行管理)
        /// </summary>
        private void ThreadPoolExample()
        {
            ThreadPool.QueueUserWorkItem((objState) => {
                
            },null);
        }

        #region 002. WPF , Winform 建議使用BackgroundWorker 

        private BackgroundWorker worker;

        private void StartAsyncButton_Click(System.Object sender , System.EventArgs e)
        {
            worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i < 10; i++)
            {
                worker.ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        private void Worker_ProgressChanged(object sender , ProgressChangedEventArgs e)
        {
            this.Text = e.ProgressPercentage.ToString();

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            worker.RunWorkerAsync();
        }
    }
}
