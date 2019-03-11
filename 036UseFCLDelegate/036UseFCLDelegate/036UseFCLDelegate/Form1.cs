using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _036UseFCLDelegate
{
    /// <summary>
    /// 036 使用FCL 中的委託聲明 (理解3種委託聲明 1.Action 2.Func 3.Predicate 並且取代傳統的Delegate做法)
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 1. Action    可接受參數，但不會回傳值
        /// 2. Func      可接受參數，但會回傳值
        /// 3. Predicate 回傳Boolean
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //傳統委派做法
            OrigineDelegate();
            //改成Action + Func + Predicate
            ReplactDelegate();
        }


        #region 1. 傳統的委派做法
        delegate int AddHandler(int i, int j);//帶有回傳值
        delegate void PrintConsole(string msg);//不帶回傳值
        delegate bool CompareReult(TempClass data);//回傳值
        /// <summary>
        /// 傳統的委派，並執行函式
        /// </summary>
        public void OrigineDelegate()
        {
            AddHandler addInt = AddInt;
            PrintConsole print = OutputString;
            CompareReult intCompare = IsEqualInt;
            print(addInt(10, 25).ToString());
            print(IsEqualInt(new TempClass() {  X = 20 ,Y = 20 }).ToString());
            print(IsEqualInt(new TempClass() { X = 20, Y = 15 }).ToString());
            //輸出結果 35 , true , false
        }

        public class TempClass
        {
            public int X { get; set; }

            public int Y { get; set; }
        }

        /// <summary>
        /// 整數相加
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int AddInt(int i, int j)
        {
            return i + j;
        }

        public void OutputString(string msg)
        {
            Console.WriteLine(msg);
        }

        public bool IsEqualInt(TempClass data)
        {
            if (data.X == data.Y)
            {
                return true;
            }

            return false;
        }
        #endregion

        /// <summary>
        /// 使用Func<T> + Action<T> 的做法
        /// </summary>
        public void ReplactDelegate()
        {
            Func<int, int, int> funcAdd = AddInt;//帶有回傳值 用Func
            Action<string> actionPrint = OutputString;//不帶有回傳 用Action
            Predicate<TempClass> perdicateBool = IsEqualInt;
            actionPrint(funcAdd(10, 25).ToString());
            actionPrint(perdicateBool(new TempClass { X = 20, Y = 20 }).ToString());
            actionPrint(perdicateBool(new TempClass { X = 20, Y = 15 }).ToString());
            //輸出結果 35 , true , false
        }
    }
}
