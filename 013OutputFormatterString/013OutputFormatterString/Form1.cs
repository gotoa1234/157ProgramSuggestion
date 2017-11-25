using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _013OutputFormatterString
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //============ 1. 基本覆寫ToString() 格式化 + IFormattable 格式化(可帶參數的字串格式化)
            classA objA = new classA();
            classPerson objB = new classPerson() { IDCode = "H333456789", FirstName = "王", LastName = "小明" };

            //Class資訊(類別資訊)
            var resultA = objA.ToString();
            //王 小明
            var resultB = objB.ToString("Ch", null);
            //小明 王
            var resullC = objB.ToString("Eg", null);
            //王 小明
            var resullD = objB.ToString();


            //============= 2. 使用格式化器的方法

            classPerson objC = new classPerson() { FirstName = "王", LastName = "大明", IDCode = "H333456790" };
            classPersonFormatter cpFormatter = new classPersonFormatter();
            //王 大明
            var result_objC_A = objC.ToString();
            //王 大明
            var result_objC_B = cpFormatter.Format("Ch", objC, null);
            //大明 王
            var resull_objC_C = cpFormatter.Format("Eg", objC, null);
            //王 大明 : H333456790
            var resull_objC_D = cpFormatter.Format("ChM", objC, null);

            //============  結合 1. 2.的方法
            classPersonCombination objD =
                new classPersonCombination() { FirstName = "王", LastName = "超明", IDCode = "H333456791" };

            classPersonFormatter cp2Formatter = new classPersonFormatter();
            //王 超明
            var result_objD_A = objD.ToString();
            //王 超明
            var result_objD_B = objD.ToString("Ch", cp2Formatter);
            //超明 王
            var resull_objD_C = objD.ToString("Eg", cp2Formatter);
            //王 超明 : H333456791
            var resull_objD_D = objD.ToString("ChM", cp2Formatter);
        }

        /// <summary>
        /// A 類別沒有覆寫.ToString() 方法
        /// </summary>
        public class classA
        {
        }

        /// <summary>
        /// classPersonB 類別覆寫 .ToString() 方法
        /// </summary>
        public class classPerson : IFormattable
        {
            /// <summary>
            /// 身分證
            /// </summary>
            public string IDCode { get; set; }

            /// <summary>
            /// 姓
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// 名
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// 覆寫classPerson在內部呼叫ToString()的方法
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                //東方名字  姓 + 名
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }

            /// <summary>
            /// 覆寫classPerson於外部呼叫ToString()的方法
            /// </summary>
            /// <param name="format"></param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                switch (format)
                {
                    case "Ch":
                        //東方名字 姓 + 名 ※已於ClassB內部覆寫
                        return this.ToString();
                    case "Eg":
                        //西方名字 名 + 姓
                        return string.Format("{0} {1}", this.LastName, this.FirstName);
                    default:
                        //(預設)東方名字 姓 + 名 ※已於classPerson內部覆寫
                        return this.ToString();
                }
            }
        }

        /// <summary>
        /// 實作"格式化器" - 自訂義套用的格式化字串方法
        /// </summary>
        public class classPersonFormatter : IFormatProvider, ICustomFormatter
        {
            /// <summary>
            /// 說明：IFormatProvider 實作
            /// 目的：傳進來的ICustomFormatter 需求物件，回傳自己
            ///       為了能餵進下方的Format(string format, object arg, IFormatProvider formatProvider) 
            ///       的formatProvider 參數中
            /// </summary>
            /// <param name="formatType"></param>
            /// <returns></returns>
            public object GetFormat(Type formatType)
            {
                if (formatType == typeof(ICustomFormatter))
                {
                    return this;
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// 說明：ICustomFormatter 實作
            /// 目的：可以傳進自己想要定義的格式化資料
            /// </summary>
            /// <param name="format"></param>
            /// <param name="arg"></param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                //這邊因為是範例所以簡化，如果有多個類別 可以先用IS判斷再轉到對應的型別，餵進對應的格式化
                classPerson person = arg as classPerson;
                if (person == null)
                {
                    return string.Empty;
                }

                switch (format)
                {
                    case "Ch":
                        return string.Format("{0} {1}", person.FirstName, person.LastName);
                    case "Eg":
                        return string.Format("{0} {1}", person.LastName, person.FirstName);
                    case "ChM":
                        return string.Format("{0} {1} : {2}", person.FirstName, person.LastName, person.IDCode);
                    default:
                        return string.Format("{0} {1}", person.LastName, person.FirstName);
                }

            }
        }

        /// <summary>
        /// 結合.Tostring() 與 格式化器
        /// </summary>ㄍ
        public class classPersonCombination : IFormattable
        {
            /// <summary>
            /// 身分證
            /// </summary>
            public string IDCode { get; set; }

            /// <summary>
            /// 姓
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// 名
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// 覆寫classPersonCombination 在內部呼叫ToString()的方法
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                //東方名字  姓 + 名
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }

            /// <summary>
            /// 覆寫classPersonCombination 於外部呼叫ToString()的方法
            /// </summary>
            /// <param name="format"></param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                switch (format)
                {
                    case "Ch":
                        //東方名字 姓 + 名 ※已於classPersonCombination內部覆寫
                        return this.ToString();
                    case "Eg":
                        //西方名字 名 + 姓
                        return string.Format("{0} {1}", this.LastName, this.FirstName);
                    case "ChM":
                        //完整資訊
                        return string.Format("{0} {1} : {2}", this.FirstName, this.LastName, IDCode);
                    default:
                        //善用 IFormatProvider 格式化器
                        classPersonFormatter myCustomFormatter = formatProvider as classPersonFormatter;

                        //如果沒有格式化器，則回傳我們覆寫的ToString()
                        if (myCustomFormatter == null)
                            return this.ToString();
                        else
                        {
                            return myCustomFormatter.Format(format, this, null);
                        }
                }
            }
        }
    }
}
