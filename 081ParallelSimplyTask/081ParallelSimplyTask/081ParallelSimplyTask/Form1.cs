using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _081ParallelSimplyTask
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 081: 使用Parallel 簡化同步狀態下Task 的使用 (理解 for , foreach , Invoke 的用法)
        /// ※使用Parallel的前提： 如果需要同步 & 順序 不該使用Parallel 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ParallelForExample();

            ParallelForeachExample();

            ParallelInvoke();
        }

        /// <summary>
        /// Parallel For的範例(For)
        /// </summary>
        public void ParallelForExample()
        {
            int[] nums = new int[] { 1, 2, 3, 4 };
            Parallel.For(0, nums.Length, (i) =>
            {
                Console.WriteLine($@" 針對數組索引{i} 對應的元素內容{nums[i]} 的一些工作代碼......");

            });
            
        }

        /// <summary>
        /// Parallel Foreach的範例(Foreach)
        /// </summary>
        public void ParallelForeachExample()
        {
            int[] nums = new int[] { 1, 2, 3, 4 };
            Parallel.ForEach(nums , (item) =>
            {
                Console.WriteLine($@" 對應的元素內容{item} 的一些工作代碼......");

            });

        }

        /// <summary>
        /// Parallel Invoke的範例(多個Task)
        /// </summary>
        public void ParallelInvoke()
        {
            Parallel.Invoke(
                //Work 1
                () => { Console.WriteLine("工作1"); },
                //Work 2
                () => { Console.WriteLine("工作2"); },
                //Work 3
                () => { Console.WriteLine("工作3"); }
            );
        }
    }
}
