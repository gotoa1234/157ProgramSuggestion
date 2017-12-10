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

namespace _015DynamicReflect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //==== A  FCL 1.0 沒有Dynamic的時代，必須透過反射取得Method ====

            DyamicSample origin = new DyamicSample();
            //取得類別 DyamicSample 裡面的Method 名稱叫 Add ※如果沒有就是Null
            var addMetohd = typeof(DyamicSample).GetMethod("Add");
            //取得到後，可以對該方法進行委派，取得結果
            int resulte = (int)addMetohd.Invoke(origin, new object[] { 1, 2 });


            //==== B  FCL 2.0 以後有了Dynamic 可以更快速、間單的取得Method ===
            dynamic origin2 = new DyamicSample();
            int result2 = origin2.Add(1, 2);

            //上面 A、B 進行 10000000 次的比較，可以展顯出兩者差異
            string rateResult = Compare(10000000);
            textBox1.Text = rateResult;

        }

        /// <summary>
        /// 比較反射與Dynamic的速度差異
        /// </summary>
        public string Compare(int times)
        {
            string resultMessage = string.Empty;

            //----------以下是反射
            //建立碼表計時器
            Stopwatch sw = new Stopwatch();
            DyamicSample origin = new DyamicSample();
            var addMetohd = typeof(DyamicSample).GetMethod("Add");

            sw.Restart();
            for (int i = times; i > 0; i--)
            {
                addMetohd.Invoke(origin, new object[] { 1, 2 });
            }
            resultMessage += string.Format("反射(Reflect)耗費：{0} 毫秒  \r\n", sw.ElapsedMilliseconds);
            sw.Stop();

            //----------以下是反射 優化
            DyamicSample originPeformance = new DyamicSample();
            var addMetohdPeformance = typeof(DyamicSample).GetMethod("Add");
            //優化部分-執行委派 FCL 3.0提供以下方法
            var delegateObj = (Func<DyamicSample, int, int, int>)Delegate.CreateDelegate(
                   typeof(Func<DyamicSample, int, int, int>),
                   addMetohdPeformance
                );

            sw.Restart();
            for (int i = times; i > 0; i--)
            {
                delegateObj(originPeformance, 1, 2);
            }
            resultMessage += string.Format("反射優化(Reflect)耗費：{0} 毫秒  \r\n", sw.ElapsedMilliseconds);
            sw.Stop();


            dynamic dynamicObj = new DyamicSample();
            //----------以下是Dynamic
            sw.Restart();
            for (int i = times; i > 0; i--)
            {
                dynamicObj.Add(1, 2);
            }
            resultMessage += string.Format("Dynamic 耗費：{0} 毫秒  \r\n", sw.ElapsedMilliseconds);
            sw.Stop();


            return resultMessage;
        }

        /// <summary>
        /// 範例類別
        /// </summary>
        public class DyamicSample
        {
            public string Name { get; set; }

            public int Add(int a, int b)
            {
                return a + b;
            }


        }
    }
}
