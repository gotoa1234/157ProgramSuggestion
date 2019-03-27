using System;
using System.Windows.Forms;

namespace _065AlwaysCatchNotFoundError
{
    /// <summary>
    /// 065 總是處理未補獲的異常 (在Global.cs 程式生命週期初期，就實作UnhandledException 事件內容)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            //註冊補獲全域錯誤事件 - 通常就是指Bug事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CatchNotFoundError);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 捕捉到未處理的例外Event時的函式
        /// </summary>
        /// <param name="sender">叫用者</param>
        /// <param name="e">未處理的例外狀況時引發的事件資料</param>
        static void CatchNotFoundError(object sender , UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Console.WriteLine($@"MyHandler caught : {ex.Message}");

        }
    }
}
