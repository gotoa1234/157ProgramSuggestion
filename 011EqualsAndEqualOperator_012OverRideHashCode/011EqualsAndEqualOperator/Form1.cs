using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _011EqualsAndEqualOperator
{
    /// <summary>
    /// 11.區別對待 == 和 Equals 12.覆寫 Equals時也要覆寫GetHashCode
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Equal 用於【值】相等  、 == 運算子用於 【引用】 相等

            //== 範例- 引用
            object A = 1;
            object B = 1;
            bool result_TestOne = (A == B);//False A 跟 B 是獨立物件，引用不相等----引用
            bool result_TestThre = A.Equals(B);//  A 與 B 值都是1，所以相等---------值
            A = B;
            bool result_TestTwo = (A == B);//True A 引用了 B ，所以引用相等 --------引用
            bool result_TestFour = A.Equals(B);//  A 與 B 值都是1，所以相等 --------值


            //== 範例 何時覆寫 Equals
            Person person_A = new Person("123");
            Person person_B = new Person("123");

            //True 因為值相同
            bool isEqualAB = person_A.Equals(person_B);
            //False 因為記憶體位址不同
            bool isMemoryAB = (person_A == person_B);
            //※ 結論: 在自訂義行別，預設的情況下 Equal 與 ==  都是針對參考位址
            //         但在程式的基本概念上 值: Equal 記憶體位址: ==


            //==========  GetHash Code 到底是什麼 : 每個物件真正的代碼
            //靜態建立
            AddPerson();
            //再次建立123這個人
            Person person_123 = new Person("123");
            //理論上AddPerson()已經將資料加入靜態函示中所以要從字典找得到
            //但結果為 false 原因就是HashCode 不一樣
            //Dictionary 就是用HashCode 去比對
            bool get = PersonValues.ContainsKey(person_123);
        }

        static Dictionary<Person, PersonDetail> PersonValues = new Dictionary<Person, PersonDetail>();

        static void AddPerson()
        {
            //123這個人
            Person person_123 = new Person("123");
            //123的個人文件
            PersonDetail detail_123 = new PersonDetail() { FileName = "123文件" };
            //加入字典中
            PersonValues.Add(person_123, detail_123);
            //True 表示字典裡有這個人(Person 類別)的資料 
            bool get = PersonValues.ContainsKey(person_123);
        }

        /// <summary>
        /// 個人文件資料
        /// </summary>
        public class PersonDetail
        {
            public string FileName { get; set; }
        
        }
       

        /// <summary>
        /// 個人
        /// </summary>
        public class Person
        {
            /// <summary>
            /// 身分證
            /// </summary>
            public string IDCard { get; private set; }

            /// <summary>
            /// 出生時建立唯一身分證字號
            /// </summary>
            /// <param name="inputId"></param>
            public Person(string inputId)
            {
                this.IDCard = inputId;
            }

            /// <summary>
            /// Equals 用於比較值 ==用於比較參考(記憶體位址)
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                //比較身分證上的ID
                return this.IDCard == (obj as Person).IDCard;
            }

            /// <summary>
            /// 標準的Equal 必須覆寫Hash Code
            /// 沒有HashCode Dictionary 就會找不到對應的值
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                //覆寫IDCard 所以 GetHashCode 也要覆寫 IDCard.GetHashCode()
                return this.IDCard.GetHashCode();
            }
        }
    }
}
