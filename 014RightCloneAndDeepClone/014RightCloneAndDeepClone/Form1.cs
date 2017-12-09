using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _014RightCloneAndDeepClone
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //淺層複製呼叫
            employee Louis = new employee()
            {
                age = 29,
                dept = new Department() { Name = "軟體開發部" },
                IDCode = "H333456789"
            };
            //=================   淺層複製  =====================
            employee Alise = Louis.Clone() as employee;
            //此時Alise資料如下:
            var alise_age = Alise.age;//-------------  29
            var alise_IDcode = Alise.IDCode;//-------  H333456789
            var alise_dept_Name = Alise.dept;//   軟體開發部

            //我們嘗試改變最源頭的Louis的個人資料
            Louis.age = 44;
            Louis.IDCode = "我沒身分證";
            Louis.dept.Name = "銷售部";

            //改變Louis資料後，再次觀察Alise的資料如下:
            var alise_age2 = Alise.age;//-------   29
            var alise_IDcode2 = Alise.IDCode;//-   H333456789
            var alise_dept_Name2 = Alise.dept;//   銷售部

            //age、IDCode 沒有連帶影響，但是dept有影響
            //可以得知 age、IDcode 是值 ， dept則是位址
            //換句話說 實作了MemberwiseClone 的複製方法
            //在實際使用 .Clone()時，實際上不是Clone記憶體位址，而是New新的記憶體位址
            //我們需要深層複製，在進行大數據連動資料時，節省我們工作的時間 & 正確性

            //===========  深層複製 ※把上面的測試再來一次，但使用我們的深層複製
            //※記得在所有相關的地方標上 [Serializable]
            employeeDeep Louis_2 = new employeeDeep()
            {
                age = 29,
                dept = new Department() { Name = "軟體開發部" },
                IDCode = "H333456789"
            };
            //=================   淺層複製  =====================
            employeeDeep CoCo = Louis_2.Clone() as employeeDeep;
            //此時Alise資料如下:
            var CoCo_age = CoCo.age;//-------------  29
            var CoCo_IDcode = CoCo.IDCode;//-------  H333456789
            var CoCo_dept_Name = CoCo.dept;//   軟體開發部

            //我們嘗試改變最源頭的Louis_2的個人資料
            Louis_2.age = 44;
            Louis_2.IDCode = "我沒身分證";
            Louis_2.dept.Name = "銷售部";

            //改變Louis資料後，再次觀察Alise的資料如下:
            var CoCo_age2 = CoCo.age;//-------   29
            var CoCo_IDcode2 = CoCo.IDCode;//-   H333456789
            var CoCo_dept_Name2 = CoCo.dept;//   軟體開發部 (沒有被Louis_2影響)


        }


        /// <summary>
        /// 淺層複製的實作
        /// </summary>
        public class employee : ICloneable
        {
            public string IDCode { get; set; }

            public int age { get; set; }

            public Department dept {get;set;}

            
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        /// <summary>
        /// 部門的類別
        /// </summary>
        [Serializable]
        public class Department
        {
            public string Name { get; set;}

            /// <summary>
            /// 覆寫此類別的引用ToString()
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.Name;
            }

        }


        /// <summary>
        /// 深層複製的實作
        /// </summary>
        [Serializable]
         public class employeeDeep 
        {
            public string IDCode { get; set; }

            public int age { get; set; }

            public Department dept { get; set; }

            
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
                    return formatter.Deserialize(objectStream) as employeeDeep;
                }


            }
        }


        //=====整合版
        /// <summary>
        /// 深層複製 + 淺層複製的實作
        /// </summary>
        [Serializable]
        public class employeeCombinate : ICloneable
        {
            public string IDCode { get; set; }

            public int age { get; set; }

            public Department dept { get; set; }

            /// <summary>
            /// 深層複製 - 深層要獨立出來
            /// </summary>
            /// <returns></returns>
            public employeeCombinate DeepClone()
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
                    return formatter.Deserialize(objectStream) as employeeCombinate;
                }


            }

            /// <summary>
            /// 淺層複製
            /// </summary>
            /// <returns></returns>
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }


    }
}
