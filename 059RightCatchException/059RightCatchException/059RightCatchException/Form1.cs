using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace _059RightCatchException
{
    /// <summary>
    /// 059. 不要在不恰當的場合下引發異常 (本篇可理解 => 1. 例外處理Tester-Doer 驗證 2.程序員正確的例外處理原則 )
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Tester-Doer 檢察方式
            new TesterDoerExample();
            //正確例外處理方式
            new RightException();
        }

        /// <summary>
        /// 001. 正常的業務邏輯判斷，不應視為Exception ，故使用TesterDoer 的方式將不合理的值紀錄
        /// </summary>
        public class TesterDoerExample
        {
            /// <summary>
            /// 叫用方式
            /// </summary>
            public TesterDoerExample()
            {
                string resultMessage = string.Empty;
                if (this.CheckValue(168, 75 ,ref resultMessage))
                {
                    Console.WriteLine("您好，您是一名正常人");
                }

                if (false == string.IsNullOrEmpty(resultMessage))
                {
                    Console.WriteLine($@"請確認您的填寫資料是否正確 {resultMessage}");
                    
                }
            }

            /// <summary>
            /// 檢查值
            /// </summary>
            /// <param name="height">身高</param>
            /// <param name="weight">體重</param>
            /// <returns></returns>
            private bool CheckValue(int height, int weight, ref string returnMessage)
            {
                if (height < 0 || weight < 0)
                {
                    returnMessage = "體重、身高不可為負值";
                    return false;
                }
                else if (weight > height)
                {
                    returnMessage = "體重大於身高，請趕緊就醫，尋求解決管道";
                    return false;

                }
                return true;
            }

        }

        /// <summary>
        /// 002. 正確處理例外錯誤的原則 - 1. 如001 2.不輕易引發異常，而應該允許異常返回傳遞
        /// </summary>
        public class RightException
        {
            /// <summary>
            /// 建構式
            /// </summary>
            public RightException()
            {
                Working();

            }

            /// <summary>
            /// 執行工作
            /// </summary>
            public void Working()
            {
                string localLogMessage = string.Empty;
                try
                {
                    AccessData();
                }
                catch(Exception ex)
                {
                    //本機Log 應記錄詳細資訊
                    localLogMessage = ex.Message;
                    //回傳給User應 封裝過的資訊
                    Console.WriteLine("目前連線異常，請使用者自行聯絡網路服務電信商 XD");
                }
            }

            /// <summary>
            /// 與DB連線存取資料
            /// </summary>
            /// <returns>True : 正確  ; False :異常</returns>
            private bool AccessData()
            {
                try
                {
                    //必定連不到的連線
                    SqlConnection conn = new SqlConnection("server=127.0.01;port=3306;user id=xxxxx;password=xxxxx;database=mvctest;charset=utf8;");
                    conn.Open();
                }
                catch (Exception ex)
                {
                    //往上拋錯
                    throw ex;
                }
                return true;
                
            }

        }


    }
}
