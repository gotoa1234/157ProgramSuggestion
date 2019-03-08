using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _026AnoyTypeAccessLINQQuery
{
    /// <summary>
    /// 026. 使用匿名類型存儲LINQ查詢結果 (目的是為了減少實體的.cs Class檔案)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void NumberChanger(int n);

        /// <summary>
        /// 本篇分三個部分
        /// 1. 匿名型別5大特性
        /// 2. LinQ 的.ToString() Override輸出
        /// 3. LinQ 比較 Lambda 差異
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //1. 匿名型別5個特性
            Anonymous_Characteristic();

            //2. LinQ 的.ToString() Override輸出
            LinQ_Outout_Method();

            //3. LinQ 比較 Lambda 差異
            LinQ_VS_Lambda();

            //結論:
            //1. 使用匿名型別 -目的是為了減少實體的.cs Class檔案
            //2. LinQ 的.ToString() 已經Override成 Json 輸出
            //3. LinQ 、Lambda相同，只是語法不同
        }


        /// <summary>
        /// 匿名類型5個特性:
        /// 1. 必須有初始值，以New初始化
        /// 2. 當成立匿名型別資料後，只能唯讀
        /// 3. 可以在Foreach中使用 (循環)
        /// 4. 支持感知智能 (強型別可以用.叫出屬性)
        /// 5. 可以擁有方法 (Method)
        /// </summary>
        public void Anonymous_Characteristic()
        {
            //1. 必須有初始值，以New初始化
            var newData = new { Name = "Louis", ID = 1234567 };

            // 2. 當成立匿名型別資料後，只能唯讀

            //newData.ID = 3345678; //這行會出錯 , 不可以設定值

            // 3. 可以在Foreach中使用 (循環)
            var newDataList = new[] { Name = "A", Name = "B" };

            foreach (var item in newDataList)
            {
                Console.WriteLine(item);
            }

            // 4. 支持感知智能 (強型別可以用.叫出屬性)
            var temp = newData.Name;

            // 5. 可以擁有方法 (Method) 
            NumberChanger nc = delegate (int x)
            {
                Console.WriteLine("Anonymous Method: {0}", x);
            };
        }

        /// <summary>
        /// 輸出後的結果
        /// </summary>
        public void LinQ_Outout_Method()
        {
            //公司類別
            List<Company> companyList = new List<Company>() {

                new Company(){ CompanyID =0 ,Name ="Micro"},
                new Company(){ CompanyID =1 ,Name ="Sun"},
            };

            //人類別
            List<Person> personList = new List<Person>() {

                new Person(){ CompanyID =1 ,Name ="Mike"},
                new Person(){ CompanyID =0 ,Name ="Rose"},
                new Person(){ CompanyID =1 ,Name ="Steve"}
            };


            Console.WriteLine($@"LINQ : 轉換輸出前 : {personList[0].ToString()}");
            //使用LinQ 將 人與公司 Join 使育 CompanyId
            var personWithCompanyList = (from person in personList
                                        join company in companyList on person.CompanyID equals company.CompanyID
                                        select new { PersonName = person.Name, CompanyName = company.Name }).ToList();

            Console.WriteLine($@"LINQ : 轉換輸出後 : {personWithCompanyList[0].ToString()}");
            //可以發現 Linq 將.ToString() 做Override 輸出成 => 【 new Person(){ CompanyID =1 ,Name ="Mike"} 】 的格式
        }

        /// <summary>
        /// LinQ 比較 Lambda 差異
        /// </summary>
        public void LinQ_VS_Lambda()
        {
            //公司類別
            List<Company> companyList = new List<Company>() {

                new Company(){ CompanyID =0 ,Name ="Micro"},
                new Company(){ CompanyID =1 ,Name ="Sun"},
            };

            //人類別
            List<Person> personList = new List<Person>() {

                new Person(){ CompanyID =1 ,Name ="Mike"},
                new Person(){ CompanyID =0 ,Name ="Rose"},
                new Person(){ CompanyID =1 ,Name ="Steve"}
            };

            Console.WriteLine($@"LINQ : 轉換輸出前 : {personList[0].ToString()}");
            //使用LinQ 將 人與公司 Join 使育 CompanyId
            var personWithCompanyList = (from person in personList
                                         join company in companyList on person.CompanyID equals company.CompanyID
                                         select new { PersonName = person.Name, CompanyName = company.Name }).ToList();

            Console.WriteLine($@"LINQ : 轉換輸出後 : {personWithCompanyList[0].ToString()}");
            //可以發現 Linq 將.ToString() 做Override 輸出成 => 【 new Person(){ CompanyID =1 ,Name ="Mike"} 】 的格式


            Console.WriteLine($@"Lambda : 轉換輸出前 : {personList[0].ToString()}");
            var lambdaTemp = personList.Join(companyList, x => x.Name, y => y.Name, (x, y) => new
            {
                PersonName = x.Name,
                CompanyName = y.Name,
            }).ToList();

            Console.WriteLine($@"Lambda : 轉換輸出後 : {lambdaTemp[0].ToString()}");
            //可以發現 LinQ 與 Lambda 是相同的結果
            //LinQ 與 Lambda哪個好用呢? 1.優先以團隊開發風格、規範為優先 2.個人偏好
        }


        private class Company
        {
            public int  CompanyID { get; set;}

            public string Name { get; set;}
        }

        private class Person
        {
            public int CompanyID { get; set; }

            public string Name { get; set; }
        }
    }
}
