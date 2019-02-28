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

namespace _017ForeachPriorityUse
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Foreach 比 For 差異
        /// 1. Foreach 具有自動將程式碼置入try-finally
        /// 2. Foreach 使用時，若型別內有繼承IDispose，會在迴圈結束後會自動使用Dispose方法
        /// 3. Foreach 不可以中途異動參考資料
        /// 4. Foreach 效能通常與For 差異不大
        /// 何時用Foreach ? 要確保不會影響原資料時，可以使用Foreach確保資料使用的安全性
        /// 如果要異動原始參考資料，仍需使用For
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //生成資料
            for (int i = 0; i < MAKE_COUNT; i++)
            {
                items.Add(i);
            }
            //執行遍歷差異
            ReadPerformance();
            //執行效能差異
            ExcutePerformance();
        }

        private int MAKE_COUNT = 3000000;
        //存放基本資料
        private static List<int> items = new List<int>();

        private void ReadPerformance()
        {
            //遍歷差異
            textBox1.Text += $@"生成Int資料筆數(使用.Add()加入資料) ： {MAKE_COUNT} 筆 == \r\n";
            
            List<int> temp = new List<int>();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //==以下為 foreach 遍歷
            foreach (var item in items)
            {
                temp.Add(item);
            }
            sw.Stop();
            //紀錄花費時間
            textBox1.AppendText($@"==> foreach 遍歷 ： 花費時間：");
            textBox1.AppendText($"{sw.Elapsed.TotalSeconds.ToString()} \r\n");
            temp.Clear();
            //==以下為 for 遍歷
           
            sw.Restart();
            for (int i = 0; i < items.Count; i++)
            {
                temp.Add(i);
            }
            sw.Stop();
            //紀錄花費時間
            textBox1.AppendText($@"==> for 遍歷 ： 花費時間：");
            textBox1.AppendText($"{sw.Elapsed.TotalSeconds.ToString()} \r\n");
        }

        private void ExcutePerformance()
        {
            textBox1.Text += ($@"不使用.Add()加入資料 ： \r\n");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //==以下為 foreach 遍歷
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            sw.Stop();
            //紀錄花費時間
            textBox1.AppendText($@"==> foreach 遍歷 ： 花費時間：");
            textBox1.AppendText($"{sw.Elapsed.TotalSeconds.ToString()} \r\n");

            //==以下為 for 遍歷
            sw.Restart();
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(i);
            }
            sw.Stop();
            //紀錄花費時間
            textBox1.AppendText($@"==> for 遍歷 ： 花費時間：");
            textBox1.AppendText($"{sw.Elapsed.TotalSeconds.ToString()} \r\n");
        }
    }
}
