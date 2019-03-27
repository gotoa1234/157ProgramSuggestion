using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace _064EachUsingTester_Doer
{
    /// <summary>
    /// 064.為循環增加Tester-Doer 模式而不是將Try-catch 放置循環內 (不該在迴圈內放入Try catch會突增效能浪費，每個catch都是一次處理)
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
            int TIMES = 1000;//執行次數
            sw.Start();//開始計時
            int divisor = 0;//除數為0
            //001. 錯誤做法，迴圈內用Try Catch包覆錯誤
            for (int dividend = 0; dividend < TIMES; dividend++)
            {
                try
                {
                    int temp = dividend / divisor;
                }
                catch
                {
                    
                }
            }
            Console.WriteLine($@"耗費時間 { sw.Elapsed.TotalMilliseconds}");

            sw.Reset();

            //002. 正確作法，對已知的錯誤邏輯應跳出該次情境
            for (int dividend = 0; dividend < TIMES; dividend++)
            {
                if (divisor == 0)
                    continue;
                int j = dividend / divisor;
            }
       
            Console.WriteLine($@"耗費時間 { sw.Elapsed.TotalMilliseconds}");
        }

      
    }
}
