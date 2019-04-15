using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace _084UsePLINQ
{
    /// <summary>
    /// 084. 使用PLINQ (學會 AsParallel 的用法 ，在一個集合中查找某個值並行並非最快，最快的應為集合的ElementAt)
    ///      分兩個篇章，第二部分時間比較可以發現，即使排序耗費的效能也是非常小
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Step1: 基本範例
            PLINQExample();
            //Step2: 時間比較
            SpeedCompare();
        }

        /// <summary>
        /// Step1: PLINQ 範例 (理解 AsParallel 、AsOrderd 用法)
        /// </summary>
        public void PLINQExample()
        {
            List<int> intList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var query = from p in intList select p;
            Console.WriteLine("LINQ輸出順序:");
            foreach (int item in query)
            {
                Console.WriteLine(item.ToString());
            }

            //並行但不會排序
            var queryParallel = from p in intList.AsParallel() select p;
            Console.WriteLine("PLINQ輸出 順序不規則:");
            foreach (int item in queryParallel)
            {
                Console.WriteLine(item.ToString());
            }

            ///下面這段與上面的 foreach相同
            Console.WriteLine("同 PLINQ輸出 順序不規則:");
            queryParallel.ForAll((item) =>
            {
                Console.WriteLine(item.ToString());
            });

            //下面這段則是學會使用 AsOrderd() 排序
            Console.WriteLine("PLINQ輸出 順序仍不規則 (※ForAll會破壞順序 ):");
            var queryParallelNotOrder = from p in intList.AsParallel().AsOrdered() select p;
            queryParallelNotOrder.ForAll((item) =>
            {
                Console.WriteLine(item.ToString());
            });

            //下面這段則是學會使用 AsOrderd() 排序 + Foreach 顯示
            Console.WriteLine("PLINQ輸出 有順序 (※使用Foreach遍歷 ):");
            var queryParallelOrder = from p in intList.AsParallel().AsOrdered() select p;
            foreach (var item in queryParallelOrder)
            {
                Console.WriteLine(item.ToString());
            }

        }

        /// <summary>
        /// Step2: 比較速度 (20萬筆資料下，有無排序影響無大)
        /// <para>
        ///  輸出結果如下：
        ///  AsParallel - 耗費時間 : 0.0009
        ///  AsParallel + ForAll - 耗費時間 : 0 複製資料數 : 95757
        ///  AsParallel + Foreach - 耗費時間 : 0 複製資料數 : 200000
        ///  AsParallel + AsOrdered - 耗費時間 : 0
        ///  AsParallel + AsOrdered + ForAll 耗費時間 : 0 複製資料數 : 102091
        ///  AsParallel + AsOrdered + Foreach- 耗費時間 : 0 複製資料數 : 200000
        /// </para>
        /// </summary>
        public void SpeedCompare()
        {
            //產生亂數資料
            Random rm = new Random((int)DateTime.Now.Ticks);
            List<int> sourceData = new List<int>();
            List<int> targetData = new List<int>();
            for (int i =0;i<200000;i++)
            {
                sourceData.Add(rm.Next(0, 10000));
            }

            Stopwatch sp = new Stopwatch();
            sp.Start();

            //並行但不會排序
            var queryParallel = sourceData.AsParallel();

            sp.Stop();
            targetData.Clear();
            Console.WriteLine($@" AsParallel - 耗費時間 : {sp.Elapsed.TotalMilliseconds}");            

            sp.Reset();
            queryParallel.ForAll((item) =>
            {
                targetData.Add(item);
            });
            sp.Stop();
            Console.WriteLine($@" AsParallel + ForAll - 耗費時間 : {sp.Elapsed.TotalMilliseconds} 複製資料數 : {targetData.Count}");
            targetData.Clear();

            sp.Reset();
            foreach (var item in sourceData)
            {
                targetData.Add(item);
            }
            sp.Stop();     
            Console.WriteLine($@" AsParallel + Foreach - 耗費時間 : {sp.Elapsed.TotalMilliseconds} 複製資料數 : {targetData.Count}");
            targetData.Clear();


            sp.Reset();
            var queryParallelOrder = sourceData.AsParallel().AsOrdered();
            sp.Stop();
            Console.WriteLine($@" AsParallel + AsOrdered - 耗費時間 : {sp.Elapsed.TotalMilliseconds}");

            sp.Reset();
            queryParallelOrder.ForAll((item) =>
            {
                targetData.Add(item);
            });
            sp.Stop();
            Console.WriteLine($@" AsParallel + AsOrdered + ForAll 耗費時間 : {sp.Elapsed.TotalMilliseconds} 複製資料數 : {targetData.Count}");
            targetData.Clear();

            sp.Reset();
            foreach (var item in queryParallelOrder)
            {
                targetData.Add(item);
            }
            sp.Stop();
            Console.WriteLine($@" AsParallel + AsOrdered + Foreach- 耗費時間 : {sp.Elapsed.TotalMilliseconds} 複製資料數 : {targetData.Count}");
            targetData.Clear();
        }
    }
}
