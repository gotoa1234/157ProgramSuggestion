using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _040eventProtectDelegate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //執行範例
            new SubClass().method();
        }

        /// <summary>
        /// 子類別 : 040使用event 關鍵字為委派施加保護
        /// </summary>
        class SubClass
        {
            /// <summary>
            /// 範例method
            /// </summary>
            public void method()
            {
                FileUploader f1 = new FileUploader();
                f1.FileUploaded += Progress;//將檔案加入上傳
                f1.FileUploaded += ProgressAnother;//將檔案加入上傳
                                                   //1. 因為有event保護:禁止清空
                                                   //f1.FileUploaded = null;
                                                   //2. 因為有event保護:禁止改值
                                                   //f1.FileUploaded(10);

                f1.Upload();
            }

            /// <summary>
            /// 第一種檔案
            /// </summary>
            /// <param name="progress"></param>
            private void Progress(int progress)
            {
                Console.WriteLine($@"File 1 {progress}");
            }

            /// <summary>
            /// 第二種檔案
            /// </summary>
            /// <param name="progress"></param>
            private void ProgressAnother(int progress)
            {
                Console.WriteLine($@"File 2 {progress}");
            }

            /// <summary>
            /// 檔案上傳範例
            /// </summary>
            private class FileUploader
            {
                /// <summary>
                /// 委派方法宣告
                /// </summary>
                /// <param name="progress"></param>
                public delegate void FileUploadedHandler(int progress);
                /// <summary>
                /// 委派執行 : 加入event 保護委派方法
                /// </summary>
                public event FileUploadedHandler FileUploaded;

                /// <summary>
                /// 執行檔案上傳
                /// </summary>
                public void Upload()
                {
                    int fileProgress = 100;//從100% 開始，每次遞減
                    while (fileProgress > 0)
                    {
                        fileProgress--;
                        //有方法被指向，才進行方法內容
                        if (FileUploaded != null)
                        {
                            FileUploaded(fileProgress);
                        }
                    }

                }
            }
        }
    }
}
