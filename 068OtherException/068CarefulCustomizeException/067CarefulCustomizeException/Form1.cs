using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace _067CarefulCustomizeException
{
    /// <summary>
    /// 068.從 System.Exception 或其他常見的基本異常中派生異常 (實作自定義的例外處理，要把序列化、匯出資訊放進實作中，才完整)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                throw new LouisException("Louis 好帥");
                throw new PaperEncryptException("加密報告出現異常", "時間: 2019/3/29");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [global:: System.Serializable]
        public class MyTestException : Exception
        {

        }

        public class MyTest2Exception : IOException
        {

        }
        #region 完整版的自定義 Exception

        /// <summary>
        /// 完整版的Exception ，考量以下幾點：
        /// 1. 繼承 ISerializable ，並且完成序/反序列化的功能
        /// 2. 覆寫Message 使其中可以Output 想表達的訊息
        /// </summary>
        public class PaperEncryptException : Exception, ISerializable
        {
            private readonly string _paperInfo;
            public PaperEncryptException() { }
            public PaperEncryptException(string message) : base(message) { }

            public PaperEncryptException(string message, Exception inner) :
                base(message, inner)
            { }

            public PaperEncryptException(string message, string paperInfo) : base(message)
            {
                _paperInfo = paperInfo;
            }

            protected PaperEncryptException(string message, string paperInfo, Exception inner) : base(message, inner)
            {
                _paperInfo = paperInfo;
            }

            protected PaperEncryptException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }

            public override string Message => $@"base.Message {_paperInfo}";

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Args", _paperInfo);
                base.GetObjectData(info, context);
            }

        }


        #endregion

        #region 簡易版的自定義 Exception

        public class LouisException : Exception
        {
            private readonly string message;

            public LouisException(string input)
            {
                this.message = input;
            }

            public override string Message => message;
        }

        #endregion



    }
}
