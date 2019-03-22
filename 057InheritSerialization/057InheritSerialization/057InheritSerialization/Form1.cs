using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _057InheritSerialization
{
    /// <summary>
    /// 057. 實現ISerializable 的子類型應負責父類的序列化 (繼承的子類必須考量是否需要序列化父類的欄位) 
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //子類無法反序列化父類的欄位(Name)
            //new ExampleA();
            //繼成的子類 實作父類序列化的欄位
            //new ExampleB();
            //
            new ExampleC();
        }

        #region 001 => 父類: Fathre 子類 : Child  其中 Child 不會繼成父類Name 的序列化

        public class ExampleA
        {
            public ExampleA()
            {
                Child louis = new Child() { Name = "Louis", Salary = 1000 };
                IFormatter formatter = new BinaryFormatter();
                //建立一個檔案Stream - 並且序列化放入檔案.txt中
                Stream stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, louis);
                stream.Close();

                //接著讀取剛剛的檔案，並且用Deserialize 反序列化
                stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Open, FileAccess.Read);
                Child objnew = (Child)formatter.Deserialize(stream);

                Console.WriteLine(objnew.Name);// <=  不會輸出
                Console.WriteLine(objnew.Salary);// <= 會輸出 1000

            }


            public class Father
            {
                public string Name { get; set; }

            }

            [Serializable]
            public class Child : Father, ISerializable
            {
                public int Salary { get; set; }

                public Child()
                {
                }

                protected Child(SerializationInfo info, StreamingContext context)
                {
                    Salary = info.GetInt32("Salary");
                }

                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("Salary", Salary);
                }
            }

        }


        #endregion

        #region 002 => 父類: Fathre 子類 : Child  其中 Child完成父類的Name 的序列化

        public class ExampleB
        {
            public ExampleB()
            {
                Child louis = new Child() { Name = "Louis", Salary = 1000 };
                IFormatter formatter = new BinaryFormatter();
                //建立一個檔案Stream - 並且序列化放入檔案.txt中
                Stream stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, louis);
                stream.Close();

                //接著讀取剛剛的檔案，並且用Deserialize 反序列化
                stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Open, FileAccess.Read);
                Child objnew = (Child)formatter.Deserialize(stream);

                Console.WriteLine(objnew.Name);// <=  可以輸出了
                Console.WriteLine(objnew.Salary);// <= 會輸出 1000

            }


            public class Father
            {
                public string Name { get; set; }

            }

            [Serializable]
            public class Child : Father, ISerializable
            {
                public int Salary { get; set; }

                public Child()
                {
                }

                protected Child(SerializationInfo info, StreamingContext context)
                {
                    Name = info.GetString("Name");//===== 增加該行 完成父類欄位實作
                    Salary = info.GetInt32("Salary");
                }

                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("Name", Name); //====== 增加該行 完成父類欄位實作
                    info.AddValue("Salary", Salary);
                }
            }

        }
        #endregion

        #region 003 => 父類: Fathre 子類 : Child  其中 父類完成序列化ISerializable , 子類必須Override

        public class ExampleC
        {
            public ExampleC()
            {
                Child louis = new Child() { Name = "Louis", Salary = 1000 };
                IFormatter formatter = new BinaryFormatter();
                //建立一個檔案Stream - 並且序列化放入檔案.txt中
                Stream stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, louis);
                stream.Close();

                //接著讀取剛剛的檔案，並且用Deserialize 反序列化
                stream = new FileStream(@"D:\TEMP\ExampleNew.txt", FileMode.Open, FileAccess.Read);
                Child objnew = (Child)formatter.Deserialize(stream);

                Console.WriteLine(objnew.Name);
                Console.WriteLine(objnew.Salary);

            }

            /// <summary>
            /// 完成父類的 序列化，並且宣告 Attribute [Serializable]
            /// </summary>
            [Serializable]
            public class Father : ISerializable
            {
                public string Name { get; set; }

                public Father()
                { 
                }

                #region 完成序/反序 列化的工作
                protected Father(SerializationInfo info, StreamingContext context)
                {
                    Name = info.GetString("Name");
                }

                public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("Name", Name);
                }
                #endregion

            }

            [Serializable]
            public class Child : Father , ISerializable
            {
                public int Salary { get; set; }

                public Child()
                {
                }

                /// <summary>
                /// 必須 base 父類的欄位序列化工作，才可以進行 父+子 序列化工作
                /// </summary>
                /// <param name="info"></param>
                /// <param name="context"></param>
                protected Child(SerializationInfo info, StreamingContext context) : base(info,context)
                {
                    Salary = info.GetInt32("Salary");
                }

                public override void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    base.GetObjectData(info, context);
                    info.AddValue("Salary", Salary);
                }
            }

        }


        #endregion

    }
}
