using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _046UseIDisposable
{
    /// <summary>
    /// 046 047. 顯示釋放資源需繼承接口 IDisposable (釋放資源 + 非託管情況下，學習如何使用IDisposable、Using的IDisposable，以及理解為何要用解構式)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //1. 使用Using 程式會自動提供 Dispose()方法，在結束時會自動釋放資源
            using (MyClass c1 = new MyClass())
            {

            }

            // 這段代碼相當於上述代碼
            MyClass myC = new MyClass();
            try {
                myC = new MyClass();
                //......執行一連串操作
            }
            finally {
                myC.Dispose();
            }
        }

        #region 2. sIDisposable 用法

        public class MyClass : IDisposable
        {
            private IntPtr nativeResource;

            private bool _disposed = false;

            public void Dispose()
            {
                _disposed = true;
                //通知GC 回收這個Class ，表示之後不使用了 ※釋放資源
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// 關閉這個Close 時，必需釋放資源
            /// </summary>
            public void Close()
            {
                this.Dispose();
            }

            /// <summary>
            /// 3. 如果使用IDispose()，請務必帶入解構式，釋放資源
            /// 為何 : 不是所有使用者都會主動叫用 Dispose()方法 釋放資源，
            ///        此時還有 GC會幫我們自動偵測是否該資源已經不需要了。
            ///        解構式，就是讓GC幫我們管理，第二層回收。
            /// </summary>
            ~MyClass()
            {
                this.Dispose();
            }

            /// <summary>
            /// 自行釋放資源
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
            }

            /// <summary>
            /// 發生錯誤時
            /// </summary>
            public void SamplePublicMethod()
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("SampleClass", "Error");
                }
            }
        }

        #endregion
    }
}
