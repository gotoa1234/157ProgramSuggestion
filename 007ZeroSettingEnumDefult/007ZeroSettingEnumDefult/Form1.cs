using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _007ZeroSettingEnumDefult
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static MyWeek week;

        private void Form1_Load(object sender, EventArgs e)
        {
            //建議:將 0 設為列舉的預設值
            //理由: 1.軟體工程界中的一致性，大都默認為0為起始值 ※除非工作團隊名定義規則
            //理由: 2.程式編譯器皆自動從0設定

            //編譯後執行 - 執行結果: 0  ==> 這是因為編譯器自動幫我們帶預設值0
            int getValue = (int)week;
        }
        /// <summary>
        /// 一周的Enum 但由 1 開始 ※反語法，請別從1開始設
        /// </summary>
        private enum MyWeek
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }
    }
}
