using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace _054NotUseMarkDisSerialize
{
    /// <summary>
    /// 054 為無用字段標註不可序列化 (好處有2點 , 理解[field: NonSerialized] 的使用情境)
    /// 序列化 : 物件轉成Stream (反序列化 : Stream 轉成物件 )※Stream 就是記憶體
    /// 通常會禁止反序列化的原因:
    /// 1. 節省空間
    /// 2. 情境、需求不需要
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 可以善用   [field: NonSerialized] 設定不可Clone() 的資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Person Temp = new Person() { Name = "Louis", Password = "123", Sex = "男" };
            Person Temp2 = new Person() { Name = "Candy", Password = "123456", Sex = "女" };

            PackageData MyDataA = new PackageData();
            MyDataA.info = new Person() { Name = "Louis", Password = "123", Sex = "男" };

            PackageData MyDataB = new PackageData();
            MyDataB.info = new Person() { Name = "Candy", Password = "123456", Sex = "女" };

            MyDataA.info.Name += " Lin ";
            var temp = MyDataA.Clone() as PackageData;
            Console.WriteLine($@"Name : {temp.info.Name}  , Password : {temp.info.Password} , Sex : {temp.info.Sex} ");
            //輸出=> Name : Louis Lin   , Password :  , Sex : 男 
        

            MyDataB.info.Name += " Bo ";
            var temp2 = MyDataB.Clone() as PackageData;
            Console.WriteLine($@"Name : {temp2.info.Name}  , Password : {temp2.info.Password} , Sex : {temp2.info.Sex} ");
            //輸出=> Name: Candy Bo, Password :  , Sex: 女
        }


        /// <summary>
        /// 改變名字的Event Function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Louis_NameChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Name Changed!");
        }

        #region 序列化範例

        [Serializable]
        public class SerializeClass
        {
            private string _name;
            public int ID;
            public string Name
            {
                get {
                    return Name;
                }
                set
                {
                    _name = value;
                }
            }

            public event EventHandler NameChanged;

        }

        #endregion

        #region 序列化共用物件
        /// <summary>
        /// 包裝類別
        /// </summary>
        [Serializable]
        public class PackageData
        {
            /// <summary>
            /// 個人資訊
            /// </summary>
            public Person info { get; set; }

            /// <summary>
            /// 深層複製的實作
            /// </summary>
            /// <returns></returns>
            public object Clone()
            {

                using (Stream objectStream = new MemoryStream())
                {
                    //序列化物件格式
                    IFormatter formatter = new BinaryFormatter();
                    //將自己所有資料序列化
                    formatter.Serialize(objectStream, this);
                    //複寫資料流位置，返回最前端
                    objectStream.Seek(0, SeekOrigin.Begin);
                    //再將objectStream反序列化回去 
                    return formatter.Deserialize(objectStream) as PackageData;
                }


            }
        }


        /// <summary>
        /// 人的資料類別
        /// </summary>
        [Serializable]
        public class Person
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 密碼
            /// </summary>
            [field: NonSerialized]
            public string Password { get; set;}

            /// <summary>
            /// 性別
            /// </summary>
            public string Sex { get; set;}
        }
        #endregion
    }
}
