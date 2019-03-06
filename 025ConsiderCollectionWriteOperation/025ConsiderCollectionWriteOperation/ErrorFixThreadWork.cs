using System;
using System.Collections.Generic;

namespace _025ConsiderCollectionWriteOperation
{
    /// <summary>
    /// 正確的Thread 操作，排錯方式為對類別加入【寫入限制】
    /// </summary>
    public class ErrorFixThreadWork
    {
        public static List<Student> _list1 = new List<Student>()
        {
             new Student(){Name ="Mike" , Age = 1},
             new Student(){Name ="Louis" , Age = 2},
        };
        public ErrorFixThreadWork()
        {
            StudentTeamX teamX = new StudentTeamX();

            //新增方法使用.Add() 、 .AddRange()
            teamX._students.Add(new Student() { Name = "Steve", Age = 3 });
            teamX._students.AddRange(_list1);

            Console.WriteLine(teamX._students.Count);

            StudentTeamX teamX2 = new StudentTeamX();
            Console.WriteLine(teamX2._students.Count);

            //※ => 以下仍為錯誤代碼 
            //結論： 即使變更為Private Set 在錯誤的使用參考物件，還是需思考該物件被參考的地方
            //       在多執行的物件下，更要考慮到是否有被誤用的問題。


            //中斷3 秒後確認Count() 數量
            //Thread t1 = new Thread(() => {
            //    teamA._students.AddRange(_list1);
            //    Thread.Sleep(3000);
            //    Console.WriteLine(_list1.Count);//將會發生錯誤，因為t2 Thread 將一起參考的 _list1 設為null
            //});

            //t1.Start();

            //Thread t2 = new Thread(() =>
            //{
            //    _list1 = null;
            //});

            //t2.Start();
        }

        public class Student
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        /// <summary>
        /// 將原本的StudentA 改良
        /// </summary>
        public class StudentTeamX
        {
            /// <summary>
            /// 變成只可以唯讀
            /// </summary>
            public List<Student> _students { get; }

            public StudentTeamX()
            {
                this._students = new List<Student>();
            }

        }
    }
}