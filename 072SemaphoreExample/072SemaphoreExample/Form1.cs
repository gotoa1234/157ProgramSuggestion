using System;
using System.Threading;
using System.Windows.Forms;

namespace _072SemaphoreExample
{
    /// <summary>
    /// 072. 在線程同步中使用信號量(理解AutoResetEvent 在執行緒中的用法，停止時可以使用,WaitOne()執行緒暫停，要繼續啟動時使用.Set())
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Working();
        }
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        
        /// <summary>
        /// 建立一個執行緒，並且使用WaitOne 等待信號(.Set()) 才繼續進行工作
        /// </summary>
        public void Working()
        {
            var tLable = this.Controls["label1"] as Label;

            //設定一個Thread 在5秒後 啟動Set
            new Thread(() =>
            {
                Thread.Sleep(5000);
                autoResetEvent.Set();
            }).Start();

            //01. Invoke 使用匿名委派Action 
            tLable.Invoke(new Action(() =>
            {
                tLable.Text = "線程啟動" + Environment.NewLine;
                tLable.Text += "做事 : " + Environment.NewLine;
                tLable.Text = "等待信號再進行工作(因為執行了  autoResetEvent.WaitOne() ) :" + Environment.NewLine;
                autoResetEvent.WaitOne();
                tLable.Text += "因為您執行了autoResetEvent.Set() 所以繼續進行工作";
            }));
            

            //02. 使用擴充方法進行Invoke 
            //tLable.InvokeIfRequired(() =>
            //{
            //    tLable.Text = "線程啟動" + Environment.NewLine;
            //    tLable.Text += "做事 : " + Environment.NewLine;
            //    tLable.Text = "等待信號再進行工作 :" + Environment.NewLine;
            //    autoResetEvent.WaitOne();
            //    tLable.Text += " 繼續進行工作，並且結束";
            //});
        }

    }

    /// <summary>
    /// 搭配02. 使用擴充方式，第55行可以呼叫 InvokeIfRequired()方法
    /// </summary>
    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
