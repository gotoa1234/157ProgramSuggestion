using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _035GenericSettingDefalut
{
    /// <summary>
    /// 035 使用default 為泛型類型變數指定初始值 (使用default(T)做初始化設定)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var intGenericTemp = Func<int>();//        初始值 => 0
            var stringGenericTemp = Func<String>();//  初始值 => null
            var decimalGenericTemp = Func<decimal>();//初始值 => 0
            var charGenericTemp = Func<char>();//      初始值 => '\0'

        }

        public T Func<T>()
        {
            //錯誤作法:
            //無法將 null 轉換成類型參數 'T'，因為其可能是不可為 null 的實值類型。
            //T t = null;
            
            //正確做法: 
            T t = default(T);
            return t;
        }
    }
}
