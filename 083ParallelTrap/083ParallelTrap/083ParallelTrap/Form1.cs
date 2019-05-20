using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _083ParallelTrap
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 083: 小心Parallel 的陷阱  (理解Parallel 每次開的Thread是依照當時的CPU 狀況配置
        ///                            ※Parllel內部執行特性 1.內部順序執行不一定 2.使用的線程數量不固定)
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 大部分的情況下輸出 11 少數情況為 12~14
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = new int[] { 1, 2, 3, 4 };
            int total = 0;
            Parallel.For<int>(0, nums.Length, () =>
            {
                return 1;
            }, (i, loopstate, subtotal) =>
            {
                subtotal += nums[i];
                return subtotal;
            },
            (x) => Interlocked.Add(ref total, x)
            );

            Console.WriteLine(total);
        }

        /// <summary>
        /// (字串理解版) 可以理解Parallel的每個項目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string[] stringArr = new string[] { "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh" };

            string result = string.Empty;
            Parallel.For<string>(
                //起始索引 : 0
                0, 
                //結束索引 : 8 (因為有8個項目)
                stringArr.Length, 
                //初始化函式 (這邊返回字串)
                () => { return "[初始化字串]"; }, 
                //線程啟動時的工作
                (i, loopstate, subResult) => {
                return subResult += stringArr[i];
                },
                //線程結束時的工作
                (threadEndstring) =>  {
                result += threadEndstring;
                Console.WriteLine("Inner : " + threadEndstring);
            });

            //輸出可能 1:
            /*
                Inner : [初始化字串]aabbddffgghh
                Inner : [初始化字串]cc
                Inner : [初始化字串]ee
             */
            //輸出可能 2:
            /*
               Inner : [初始化字串]aabbddeeffgghh
               Inner : [初始化字串]cc
            */
            //輸出可能 3:
            /*
                Inner : [初始化字串]cc
                Inner : [初始化字串]gg
                Inner : [初始化字串]aabbddffhh
                Inner : [初始化字串]ee
             */
            //輸出可能 4:
            /*
                Inner : [初始化字串]aabbccddeeffgghh 
            */

        }
    }
}
