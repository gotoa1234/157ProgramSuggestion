using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _069UseFinally
{
    /// <summary>
    /// 069.應使用finally 避免資源洩漏 (finally 進行釋放資源)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //該方法可以看到輸出為 : Method2 釋放資源 然後 Method1釋放資源
            Method1();


            //該方法可以看到輸出為 : Method4 捕獲異常 然後 釋放資源 然後 Method3捕獲異常 然後 釋放資源
            //可以得知發生例外時 catch > finally ，先進行捕捉再進入最後
            //因此在Try Catch 中如果使用到的資源，進入到Catch後應考量是否要 ~Dispose() 該資源，避免占用空間 (※GC 多半會回收，但非即時)
            Method3();
        }

        public class ClassShouldDisposeBase : IDisposable
        {
            string _methodName;

            public ClassShouldDisposeBase(string methodName)
            {
                _methodName = methodName;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                Console.WriteLine($@"在方法 : {_methodName} 中被釋放");

            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    //Clean Code
                }
            }

            ~ClassShouldDisposeBase()
            {
                this.Dispose(false);
            }
        }

        static void Method1()
        {
            ClassShouldDisposeBase c = null;
            try
            {
                c = new ClassShouldDisposeBase("Method1");
                Method2();
            }
            finally
            {
                c.Dispose();
            }
        }
        static void Method2()
        {
            ClassShouldDisposeBase c = null;
            try
            {
                c = new ClassShouldDisposeBase("Method2");
            }
            finally
            {
                c.Dispose();
            }
        }

        static void Method3()
        {
            ClassShouldDisposeBase c = null;
            try
            {
                c = new ClassShouldDisposeBase("Method3");
                Method4();
            }
            catch
            {
                Console.WriteLine("在Mehod3 中捕獲異常");
            }
            finally
            {
                c.Dispose();
            }
        }

        static void Method4()
        {
            ClassShouldDisposeBase c = null;
            try
            {
                c = new ClassShouldDisposeBase("Method4");
                throw new Exception();
            }
            catch
            {
                Console.WriteLine("在Mehod4 中捕獲異常");
                throw;
            }
            finally
            {
                c.Dispose();
            }
        }
    }
}
