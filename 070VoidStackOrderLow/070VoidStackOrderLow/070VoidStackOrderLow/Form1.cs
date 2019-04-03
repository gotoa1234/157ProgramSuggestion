using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _070VoidStackOrderLow
{
    /// <summary>
    /// 070. 引用避免再堆疊棧順序較低的位置紀錄異常 (在代碼中包覆的例外避免巢狀重複)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MethodHigh();
        }

        internal void MethodLow()
        {
            try
            {
                int a = 0;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"MethodLow: {ex.Message}");
                throw;
            }
        }

        internal void MethodHigh()
        {
            try
            {
                MethodLow();
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"MethodHigh: {ex.Message}");
            }
        }
    }
}
