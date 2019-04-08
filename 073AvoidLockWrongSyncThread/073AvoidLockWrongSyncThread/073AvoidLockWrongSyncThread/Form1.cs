using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace _073AvoidLockWrongSyncThread
{
    /// <summary>
    /// 073.避免鎖定不恰當的同步對象 (1.Lock用法 2.靜態方法必須保證Thread安全(這邊以類為範例) )
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

        AutoResetEvent autoSet = new AutoResetEvent(false);
        List<string> tempList = new List<string>() { "初始_1", "初始_2", "初始_3" };

        /// <summary>
        /// 01. 執行緒中很一般的Lock() 用法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            object syncObj = new object();
            #region 執行的第一個執行緒 : 進行查詢 

            Thread t1 = new Thread(() => {
                autoSet.WaitOne();//等待中
                lock (syncObj)
                {
                    //每個項目遍例，並且等待1秒
                    foreach (var item in tempList)
                    {
                        Thread.Sleep(1000);
                    }
                }

            });
            t1.IsBackground = true;
            t1.Start();
            #endregion

            #region 執行的第二個執行緒 : 進行移除

            Thread t2 = new Thread(() => {
                autoSet.Set();//解儲等待繼續執行
                Thread.Sleep(1000);//等待一秒讓 t1的執行緒有正確執行到 lock

                //此時T1 應該遍例到 第二個項目(第2s) ，如果此時沒有Lock，將會異常
                lock (syncObj)
                {
                    tempList.RemoveAt(1);
                }

            });
            t2.IsBackground = true;
            t2.Start();
            #endregion
        }

        /// <summary>
        /// 02. Class 類別中靜態資料進行的 Lock鎖
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            SampleClass sample1 = new SampleClass();
            SampleClass sample2 = new SampleClass();
            
            sample1.StartT1();//進行sample1的Lock
            sample2.StartT2();//進行sample2的RemoveAt(1)

        }

        /// <summary>
        /// 02. Class 類別中的Lock鎖
        /// </summary>
        class SampleClass
        {
            public static List<string> _tempList = new List<string>() { "初始_1", "初始_2", "初始_3" };
            static AutoResetEvent _autoSet = new AutoResetEvent(false);
            /// <summary>
            /// 正確寫法 ，當資料都具有 static 時，鎖也必須擁有Static
            /// </summary>
            static object _syncobj = new object();

            //以下試錯誤用法，在建立多個類別時，都會有獨立的Lock，等於各自用不同的Lock 進行相同物件的鎖定。
            //object _syncobj = new object();

            public void StartT1()
            {
                Thread t1 = new Thread(() =>
                {
                    _autoSet.WaitOne();
                    lock (_syncobj)
                    {
                        foreach (var item in _tempList)
                        {
                            Thread.Sleep(1000);
                        }
                    }
                });
                t1.IsBackground = true;
                t1.Start();
            }

            public void StartT2()
            {
                Thread t2 = new Thread(() =>
                {
                    _autoSet.Set();
                    Thread.Sleep(1000);
                    lock (_syncobj)
                    {
                        _tempList.RemoveAt(1);
                    }
                });
                t2.IsBackground = true;
                t2.Start();
            }


        }
    }

    
}
