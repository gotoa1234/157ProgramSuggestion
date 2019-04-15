using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _085TaskExceptionHandle
{
    /// <summary>
    /// 085. Task中的異常處理 ( Task 拋出例外最佳解 - 使用事件通知主執行緒)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TaskNotThrowToMainThreadExample();

            //TaskThrowToMainThreadButMainThreadBlockingExample();
            //Task 拋出例外最佳解 - 使用事件
            TaskBestThrowException();
        }


        /// <summary>
        /// 001. Task 發生例外的接收範例-例外資訊未拋到主線程中 (沒有將例外資訊，拋到主線程中，在tEnd 的Task內的線程中匯出)
        /// </summary>
        private void TaskNotThrowToMainThreadExample()
        {
            Console.WriteLine("001. Task 發生例外的接收範例-例外資訊未拋到主線程中");
            Task t = new Task(() =>
            {
                throw new Exception("Task 的Excetption");
            });
            t.Start();
            //當發生Task 的例外時(指定 TaskContinuationOptions.OnlyOnFaulted )，進行下列資訊的匯出
            Task tEnd = t.ContinueWith((task) =>
            {
                foreach(var item in task.Exception.InnerExceptions)
                {
                    Console.WriteLine($@"異常類型: {item.GetType()}");
                    Console.WriteLine($@"來自: {item.Source}");
                    Console.WriteLine($@"異常內容: {item.Message}");
                }
            } , TaskContinuationOptions.OnlyOnFaulted);

            Console.WriteLine("執行結束");
        }

        /// <summary>
        /// 002. Task 發生例外的接收範例-資訊拋到主線程中 (但.Wait()方法將會造成 Thread異常時，會阻塞主執行緒)
        /// </summary>
        private void TaskThrowToMainThreadButMainThreadBlockingExample()
        {
            Console.WriteLine("002. Task 發生例外的接收範例-資訊拋到主線程中");
            Task t = new Task(() =>
            {
                throw new InvalidOperationException("Task 拋出例外");
            });

            t.Start();
            Task tEnd = t.ContinueWith((task) =>
            {
                throw task.Exception;
            }, TaskContinuationOptions.OnlyOnFaulted);

            try
            {
                tEnd.Wait();
            }
            catch(AggregateException err)
            {
                foreach (var item in err.InnerExceptions)
                {
                    Console.WriteLine($@"異常類型: {item.InnerException.GetType()}");
                    Console.WriteLine($@"來自: {item.InnerException.Source}");
                    Console.WriteLine($@"異常內容: {item.InnerException.Message}");
                }
            }
            Console.WriteLine("執行結束");
        }

        #region 003.Task 發生例外的最佳範例-使用事件

        /// <summary>
        /// 合計(Aggregate)例外事件
        /// </summary>
        static event EventHandler<AggregateExceptionArgs> AggregateExceptionCatched;

        public class AggregateExceptionArgs : EventArgs
        {
            public AggregateException AggregateExceptio { get; set; }

        }

        private void TaskBestThrowException()
        {
            Console.WriteLine("003.Task 發生例外的最佳範例-使用事件");
            //註冊採集例外事件
            AggregateExceptionCatched += new EventHandler<AggregateExceptionArgs>(Program_AggregateExceptionChanged);
            Task t = new Task(() => {
                try
                {
                    throw new InvalidOperationException(" Task 拋出例外");
                }
                catch (Exception err)
                {
                    AggregateExceptionArgs errArgs = new AggregateExceptionArgs()
                    {
                        AggregateExceptio = new AggregateException(err)
                    };
                    AggregateExceptionCatched(null, errArgs);
                }

            });
            t.Start();

            Console.WriteLine("事件通知 Exception 結束");
        }

        /// <summary>
        /// 例外事件顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Program_AggregateExceptionChanged(object sender, AggregateExceptionArgs e)
        {
            foreach (var item in e.AggregateExceptio.InnerExceptions)
            {
                Console.WriteLine($@"異常類型: {item.GetType()}");
                Console.WriteLine($@"來自: {item.Source}");
                Console.WriteLine($@"異常內容: {item.Message}");
            }
        }

        #endregion



    }
}
