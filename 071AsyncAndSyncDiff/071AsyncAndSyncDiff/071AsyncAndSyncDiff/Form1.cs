using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _071AsyncAndSyncDiff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string url = "http://www.dotblogs.com.tw/";
        HttpWebRequest _request = null;
        HttpWebResponse _response = null;
        Uri _uri = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            this._uri = new Uri(url);
        }

        private void buttonGetPage_Click(object sender, EventArgs e)
        {
            
            //Thread t = new Thread(() => {

            //    HttpWebRequest request = HttpWebRequest.Create("http://yahoo.com.tw") as HttpWebRequest;
            //    var response = request.GetResponse();
            //    var stream = response.GetResponseStream();
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        var content = reader.ReadLine();
            //        textBox1.Text = content;
            //    }
            //});

            //t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            this._request = (HttpWebRequest)WebRequest.Create(url);
            this._request.Method = WebRequestMethods.Http.Get;//預設為GET
            this.listBox1.Items.Add("用戶實際回應要求的URI:" + this._request.Address.ToString());
            this.listBox1.Items.Add("是否允許重新導向回應:" + this._request.AllowAutoRedirect.ToString());
            this.listBox1.Items.Add("是否允許緩衝傳送資料:" + this._request.AllowWriteStreamBuffering.ToString());
            this.listBox1.Items.Add("用戶端安全性憑證:" + this._request.ClientCertificates.ToString());
            if (this._request.Connection != null)
                this.listBox1.Items.Add("與伺服端保持持續性的連結至下達close參數為止:" + this._request.Connection.ToString());
            if (this._request.ConnectionGroupName != null)
                this.listBox1.Items.Add("連結群組名稱:" + this._request.ConnectionGroupName.ToString());
            if (this._request.ContentLength != -1)
                this.listBox1.Items.Add("傳送資料內容的大小:" + this._request.ContentLength.ToString());
            if (this._request.ContentType != null)
                this.listBox1.Items.Add("傳送資料內容的MIME格式:" + this._request.ContentType.ToString());
            this.listBox1.Items.Add("是否已接收HTTP伺服端的回應:" + this._request.HaveResponse.ToString());
            this.listBox1.Items.Add("是否已接收HTTP在HTTP請求完成之後，是否關閉與HTTP伺服端之連結:" + this._request.KeepAlive.ToString());
            this.listBox1.Items.Add("媒體類型:" + this._request.MaximumAutomaticRedirections.ToString());

            if (this._request.MediaType != null)
                this.listBox1.Items.Add("媒體類型:" + this._request.MediaType.ToString());
            this.listBox1.Items.Add("通訊協定方法:" + this._request.Method.ToString());
            this.listBox1.Items.Add("是否要求預先驗證:" + this._request.PreAuthenticate.ToString());
            this.listBox1.Items.Add("HTTP通訊協定的版本:" + this._request.ProtocolVersion.ToString());

            //var request = HttpWebRequest.Create("https://tw.yahoo.com/");
            //request.BeginGetResponse(this.AsyncCallbackImpl, request);


        }

        public void AsyncCallbackImpl(IAsyncResult asyncResult)
        {
            var request = asyncResult as WebRequest;
            var response = request.EndGetResponse(asyncResult);
            var stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                var content = reader.ReadLine();
                //textBox1.Text = content;
                    
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._request = (HttpWebRequest)WebRequest.Create(url);
            this._request.Method = WebRequestMethods.Http.Get;//預設為GET

            this.listBox2.Items.Clear();

            this._response = (HttpWebResponse)this._request.GetResponse();
            HttpStatusCode code = this._response.StatusCode;
            int idNumber = (int)code;
            this.listBox2.Items.Add("回應的字元編碼格式:" + this._response.CharacterSet.ToString());
            this.listBox2.Items.Add("回應的壓縮及編碼格式:" + this._response.CharacterSet.ToString());
            this.listBox2.Items.Add("回應資料內容的大小:" + this._response.ContentLength.ToString());
            this.listBox2.Items.Add("回應資料內容的MIME格式:" + this._response.ContentType.ToString());
            this.listBox2.Items.Add("最近修改回應內容的日期時間:" + this._response.LastModified.ToString());
            this.listBox2.Items.Add("回應通訊協定的版本:" + this._response.ProtocolVersion.ToString());
            this.listBox2.Items.Add("伺服端所回應的URI:" + this._response.ResponseUri.ToString());
            this.listBox2.Items.Add("傳送回應的伺服器名稱:" + this._response.Server.ToString());
            this.listBox2.Items.Add("回應訊息狀態的編碼編號:" + idNumber.ToString());
            this.listBox2.Items.Add("回應訊息狀態的編碼狀態:" + this._response.StatusCode.ToString());
            this.listBox2.Items.Add("回應訊息狀態的描述:" + this._response.StatusDescription.ToString());
            this._response.Close();
        }
    }
}
