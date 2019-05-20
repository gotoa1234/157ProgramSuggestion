using System;
using System.Windows.Forms;

namespace _105PrivateStructUseForSingleton
{
    /// <summary>
    /// 105: 使用私有構造函數強化單例
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public sealed class Singleton
        {
            static Singleton _instance = null;
            static readonly object _padlock = new object();

            /// <summary>
            /// 限制實例在外部被創建
            /// </summary>
            Singleton()
            {

            }

            public static Singleton Instance
            {
                get {
                    if (_instance == null)
                    {
                        lock (_padlock)
                        {
                            if (_instance == null)
                            {
                                _instance = new Singleton();
                            }
                        }
                    }
                    return _instance;
                }
            }

            public void SampleMethod()
            {
            }

        }

        
    }
}
