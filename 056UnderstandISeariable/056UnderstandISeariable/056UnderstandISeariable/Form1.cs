using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace _056UnderstandISeariable
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 056. 使用繼承ISerializable 接口更靈活地控制序列化過程 (序列化、反序列化的過程都可以建立正確的資料)
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化資料 - 沒有組合的ChineseName 欄位
            Person louis = new Person("Lin","Louis");
            IFormatter formatter = new BinaryFormatter();
            //建立一個檔案Stream - 並且序列化放入檔案.txt中
            //此時存入的只有 Louis , Lin 兩個欄位的資料
            Stream stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, louis);
            stream.Close();

            //接著讀取剛剛的檔案，並且用Deserialize 反序列化
            stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Open, FileAccess.Read);
            Person objnew = (Person)formatter.Deserialize(stream);

            Console.WriteLine(objnew.LastName);
            Console.WriteLine(objnew.FirstName);
            Console.WriteLine(objnew.ChineseName);//可以發現反序列化自動執行 protected Person(SerializationInfo info, StreamingContext context) 內部實作
        }

        [Serializable]
        public class Person : ISerializable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ChineseName { get; set; }

            /// <summary>
            /// 建構式
            /// </summary>
            public Person(string firstName , string lastName)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
            }

            #region 反序列化 Deserialize 的執行順序

            #endregion
            /// <summary>
            /// Step1 . 先將反序列化的檔案寫入 info 中
            /// </summary>
            /// <param name="info"></param>
            /// <param name="context"></param>
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("FirstName", FirstName);
                info.AddValue("LastName", LastName);
            }

            /// <summary>
            /// 在將資料轉型為Person類時，做對應的存入，並且將ChineseName 寫入到資料中
            /// </summary>
            /// <param name="info"></param>
            /// <param name="context"></param>
            protected Person(SerializationInfo info, StreamingContext context)
            {
                FirstName = info.GetString("FirstName");
                LastName = info.GetString("LastName");
                ChineseName = $@"{FirstName} {LastName}";
            }

            
        }
    }
}
