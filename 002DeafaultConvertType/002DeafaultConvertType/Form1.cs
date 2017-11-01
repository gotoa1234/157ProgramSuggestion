using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _002DeafaultConvertType
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 002. 使用預設轉型方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
                轉型方法分以下4種
                1. 使用型別的轉換運算子
                2. 型別內建的Parse , TryParse, ToString , ToDouble 等
                3. 幫助類提供的方法
                4. CLR (Common Language Runtime 通用語言運行庫)支援的轉型
             */

            //======1. 使用型別的轉換運算子======

            //1.1 型別轉換運算子
            int i = 0;
            float j = 0;
            j = i;     //隱式轉型 - 轉為浮點數;
            i = (int)j;//顯示轉型 - 強制轉為整數

            //1.2 重載運算子
            IPAddress ipv4 = new IPAddress(16885952);//192.168.1.1  = 16777216 + 65536 + 43008 +192
            Ip ipv4_IpClass = "192.168.1.1";
            string callResult = ipv4_IpClass.ToString();


            //======2. 型別內建的Parse , TryParse, ToString , ToDouble 等
            int a = int.Parse("91");
            double b = double.Parse("1.23");

            //======3. 幫助類提供的方法
            Ip2 test = new Ip2();
            string result =  test.ToString();

            //======4. CLR (Common Language Runtime 通用語言運行庫)支援的轉型
            animal main = new animal();
            dog Samoyed = new dog();
            main = Samoyed;
            Samoyed = (dog)main;

        }

        /// <summary>
        /// 建立一個IP類別
        /// </summary>
        public class Ip
        {
            /// <summary>
            /// 屬性
            /// </summary>
            private IPAddress ipv4;

            /// <summary>
            /// 建構式 - 將傳進來的String IP 轉為 IPAddress
            /// </summary>
            /// <param name="inputIp"></param>
            public Ip(string inputIp)
            {
                this.ipv4 = IPAddress.Parse(inputIp);
            }
            /// <summary>
            /// 建立隱式轉型 - 可以接受String 型別
            /// </summary>
            /// <param name="inputIp"></param>
            public static implicit operator Ip(string inputIp)
            {
                Ip iptemp = new Ip(inputIp);
                return iptemp;
            }
            /// <summary>
            /// 重載運算子 - 複寫ToString()方法
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.ipv4.ToString()+ "12";
            }
        }


        public class Ip2 : IConvertible
        {
            public TypeCode GetTypeCode()
            {
                throw new NotImplementedException();
            }

            public bool ToBoolean(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public byte ToByte(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public char ToChar(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public DateTime ToDateTime(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public decimal ToDecimal(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public double ToDouble(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public short ToInt16(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public int ToInt32(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public long ToInt64(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public sbyte ToSByte(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public float ToSingle(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public string ToString(IFormatProvider provider)
            {
                return "123";
                //throw new NotImplementedException();
            }

            public object ToType(Type conversionType, IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public ushort ToUInt16(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public uint ToUInt32(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public ulong ToUInt64(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }
        }

        #region 繼承父類別， CLR 會自動幫我們建立 (static implicit operator 隱式轉換的功能) 

        /// <summary>
        /// 動物類別 - 這邊範例做為父類別
        /// </summary>
        public class animal
        {

        }

        /// <summary>
        /// 狗類別(子) - 這邊繼承animal類別
        /// </summary>
        public class dog : animal
        {

        }

        /// <summary>
        /// 貓類別(子) - 這邊繼承animal類別
        /// </summary>
        public class cat : animal
        {

        }
        #endregion

    }
}
