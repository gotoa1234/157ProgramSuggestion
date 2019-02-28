using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
namespace _019_UseAnonymousType
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //C# 3.0 之後可以使用匿名型別 Anonymous;;; type 宣告變數，而不用事先宣告Class

            var person = new {
                ID = 9527,
                Department = "業務部",
            };

            //轉成Json
            string str_person = JsonConvert.SerializeObject(person);
            //解析結果
            // str_person  ==>   {"ID":9527,"DepartMent":"業務部"}
            
        }
    }
}
