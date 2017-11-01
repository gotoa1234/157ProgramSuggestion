using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _003asAndisAndexplicit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //as 是顯示轉型(強制)當失敗時會為Null 型態
            object a = new cat();
            dog b = new dog();
            var temp = a as dog;//結果: null 因為a的型別是cat
            //is 是判斷型別
            bool isKindDog = a is dog;// 結果:false
            bool isKindCat = a is cat;// 結果:true
            //顯示型別轉型(強制)
            var temp3 = (dog)a;//出現Exception，造成程式錯誤，必須用Try Catch避免程式異常
            //※Try Catch 基本上一定會消耗記憶體資源，包覆愈多層，效能耗費就愈多

           

        }

        /// <summary>
        /// 定義貓的型別
        /// </summary>
        public class cat
        {

        }

        /// <summary>
        /// 定義狗的型別
        /// </summary>
        public class dog {

        } 
            
    }
}
