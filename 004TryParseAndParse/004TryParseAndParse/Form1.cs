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

namespace _004TryParseAndParse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Parse 效能
            textBox_Perfomance.AppendText(this.parseSuccessful());
            textBox_Perfomance.AppendText(this.parseFailure());

            //TryParse 效能
            textBox_Perfomance.AppendText(this.tryParseSuccessful());
            textBox_Perfomance.AppendText(this.tryParseFailure());

        }

        /// <summary>
        /// .Parse() 跑1000次，且成功
        /// </summary>
        /// <returns></returns>
        private string parseSuccessful()
        {
            //計時開始
            Stopwatch sw = Stopwatch.StartNew();
            double tempValue = 0;
            for (int i = 1000; i > 0; i--)
            {
                try
                {
                    tempValue = double.Parse("123");
                }
                catch (Exception ex)
                {
                    tempValue = 0;
                }
            }
            //計時結束
            sw.Stop();
            return string.Format("parseSuccessful: {0}{1}", sw.Elapsed.ToString(), "\r\n");
        }

        /// <summary>
        /// .Parse() 跑1000次，且失敗
        /// </summary>
        /// <returns></returns>
        private string parseFailure()
        {
            //計時開始
            Stopwatch sw = Stopwatch.StartNew();
            double tempValue = 0;
            for (int i = 1000; i > 0; i--)
            {
                try
                {
                    tempValue = double.Parse(null);
                }
                catch (Exception ex)
                {
                    tempValue = 0;
                }
            }
            //計時結束
            sw.Stop();
            return string.Format("parseFailure : {0}{1}", sw.Elapsed.ToString(), "\r\n");
        }


        /// <summary>
        /// .TryParse() 跑1000次，且成功
        /// </summary>
        /// <returns></returns>
        private string tryParseSuccessful()
        {
            //計時開始
            Stopwatch sw = Stopwatch.StartNew();
            double tempValue = 0;
            for (int i = 1000; i > 0; i--)
            {
                if (double.TryParse("123", out tempValue) == false)
                {
                    tempValue = 0;
                }
            }
            //計時結束
            sw.Stop();
            return string.Format("tryParseSuccessful: {0}{1}", sw.Elapsed.ToString(), "\r\n");
        }

        /// <summary>
        /// .TryParse() 跑1000次，且失敗
        /// </summary>
        /// <returns></returns>
        private string tryParseFailure()
        {
            //計時開始
            Stopwatch sw = Stopwatch.StartNew();
            double tempValue = 0;
            for (int i = 1000; i > 0; i--)
            {
                if (double.TryParse(null, out tempValue) == false)
                {
                    tempValue = 0;
                }
            }
            //計時結束
            sw.Stop();
            return string.Format("tryParseFailure: {0}{1}", sw.Elapsed.ToString(), "\r\n");
        }
    }
}
