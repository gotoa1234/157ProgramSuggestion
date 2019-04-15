using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _080UseTaskReplaceThreadPool
{
    /// <summary>
    /// 080. 用Task代替 ThreadPool
    /// ThreadPool 缺點
    /// 1. 無法與內部Thread 進行交互工作(通知、取消、完成)
    /// 2. 不支持Thread 執行的順序
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TaskExample();
        }

        public void TaskExample()
        {
            Task t = new Task(() =>
            {
                Console.WriteLine("任務開始");
            });

            t.Start();
            t.ContinueWith((task) =>
            {
                Console.WriteLine("任務完成");
                Console.WriteLine($@"IsCanceled = {task.IsCanceled} IsComplated = {task.IsCompleted} tIsFaulted={task.IsFaulted}");
            });
        }



    }
}
