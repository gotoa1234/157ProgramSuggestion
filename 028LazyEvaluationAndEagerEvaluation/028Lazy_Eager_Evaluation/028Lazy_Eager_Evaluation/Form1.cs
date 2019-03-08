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

namespace _028Lazy_Eager_Evaluation
{
    /// <summary>
    /// 028. 理解延遲求值與主動求值得的區別 (以延遲求值為優先考量，避免主動求值)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Example();

            ComparePerformance();

            //結論: 盡量以延遲求值，沒有必要避免使用.ToList()語句。
        }

        /// <summary>
        /// 延遲求值與主動求值的範例
        /// </summary>
        public void Example()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            var lazyTempList = list.Where(o => o > 5);
            var eagerTempList = list.Where(o => o > 5).ToList();

            list[0] = 9527;

            foreach (var item in lazyTempList)
            {
                Console.WriteLine(item.ToString());
            }
            //輸出為 : 9527 , 6, 7, 8

            foreach (var item in eagerTempList)
            {
                Console.WriteLine(item.ToString());
            }
            //輸出為 : 6, 7, 8
        }

        /// <summary>
        /// 比較效能 
        /// 1萬筆資料 兩者差距約 290 倍的效能差異
        /// 主動求值 ~= 29秒  延遲求值 ~=0.1秒 
        /// </summary>
        public void ComparePerformance()
        {
            int MAXCOUNT = 10000;//1萬筆資料
            List<int> list = new List<int>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < MAXCOUNT; i++)
            {
                list.Add(i);
                list.ToList();//每次都Eager Evaluation
            }
            sw.Stop();
            Console.WriteLine($@"耗費時間(ms) : {sw.Elapsed.TotalMilliseconds}");

           
            sw.Restart();

            for (int i = 0; i < MAXCOUNT; i++)
            {
                list.Add(i);
            }
            list.ToList();//最後進行Lazy Evaluation
            sw.Stop();
            Console.WriteLine($@"耗費時間(ms) : {sw.Elapsed.TotalMilliseconds}");

        }
    }
}
