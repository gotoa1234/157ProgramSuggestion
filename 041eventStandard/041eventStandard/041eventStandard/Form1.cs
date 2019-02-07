using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _041eventStandard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 041 : 實現標準的事件模型 (基於040 的改良)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }



        class SubClass
        {
            public void Execute()
            {
                FileUploader fileUp = new FileUploader();
                fileUp.FileUploaded += Progress;
                fileUp.Upload();
            }

            /// <summary>
            /// 類別 : 事件類別約束
            /// </summary>
            public class FileUploadedEventArgs : EventArgs
            {
                public int FileProgress { get; set; }
            }

            class FileUploader
            {
                public event EventHandler<FileUploadedEventArgs> FileUploaded;
                
                public void Upload()
                {
                    FileUploadedEventArgs e = new FileUploadedEventArgs() { FileProgress = 100 };

                    while(e.FileProgress > 0)
                    {
                        e.FileProgress--;
                        if (FileUploaded != null)
                        {
                            FileUploaded(this, e);
                        }
                    }
                }
            }

            public void Progress(object sender, FileUploadedEventArgs e)
            {
                Console.WriteLine($@"File 1 {e.FileProgress}");
            }

            /// <summary>
            /// 第二種檔案
            /// </summary>
            public void ProgressAnother(object sender,FileUploadedEventArgs e)
            {
                Console.WriteLine($@"File 2 {e.FileProgress}");
            }
        }
    }
}
