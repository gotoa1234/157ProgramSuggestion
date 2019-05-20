using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _104PolymorphismReplaceCon
{
    /// <summary>
    /// 104 : 用多態代替條件語句
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void NormalCondition()
        {
            
        }

        private void PolymorphismReplaceExample()
        {
            //啟動
            Commander commander = new StartCommander();
            Driver(commander);

            //停止
            commander = new StopCommander();
            Driver(commander);
        }

        private void Driver(Commander com)
        {
            com.Execute();
        }


        abstract class Commander
        {
            public abstract void Execute();
        }

        class StartCommander : Commander
        {
            public override void Execute()
            {
                //啟動
            }
        }

        class StopCommander : Commander
        {
            public override void Execute()
            {
                //停止
            }
 
        }
    }
}
