using System;
using System.Windows.Forms;

namespace _025ConsiderCollectionWriteOperation
{
    /// <summary>
    /// 025. 謹慎集合屬性的可寫操作 (每個可具有寫入的屬性，在非同步的情況下)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //錯誤的搶資源 - 會執行錯誤
            new ErrorThreadWork();

            //正確的修正 - 可以正確運行
            new ErrorFixThreadWork();
        }
    }
}
