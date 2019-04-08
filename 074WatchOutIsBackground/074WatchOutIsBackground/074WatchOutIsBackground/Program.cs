using System;
using System.Threading;

namespace _074WatchOutIsBackground
{
    /// <summary>
    /// 074. 警惕線程的IsBackground (true:後台Thread 應用程式結束跟著關閉，false:前台Thread 應用程式結束仍存在，除非本身Thread執行完畢)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(() =>
            {
                Console.WriteLine("Thread Working");
                Console.ReadKey();
                Console.WriteLine("Thread Finish");

            });
            Console.WriteLine("主程式結束");

            t.IsBackground = false;
            t.Start();
        }
    }
}
