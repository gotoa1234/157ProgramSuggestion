using System;
using System.Collections.Generic;
using System.Threading;

namespace _025ConsiderCollectionWriteOperation
{
    /// <summary>
    /// 錯誤的Thread 操作，假設有兩位工程師同時操作同個物件(本範例為List)
    /// </summary>
    public class ErrorThreadWork
    {
        public static List<Student> _list1 = new List<Student>()
        {
             new Student(){Name ="Mike" , Age = 1},
             new Student(){Name ="Louis" , Age = 1},
        };

        public ErrorThreadWork()
        {
            StudentTeamA teamA = new StudentTeamA();

            //中斷3 秒後確認Count() 數量
            Thread t1 = new Thread(() => {
                teamA.Students = _list1;
                Thread.Sleep(3000);
                Console.WriteLine(_list1.Count);//將會發生錯誤，因為t2 Thread 將一起參考的 _list1 設為null
            });

            t1.Start();

            Thread t2 = new Thread(() =>
            {
                _list1 = null;
            });

            t2.Start();
        }


    }
    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

    }

    public class StudentTeamA
    {
        public List<Student> Students { get; set; }
    }
}