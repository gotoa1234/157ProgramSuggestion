using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _094NewAndOverrideDiff
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 094. 區別Override 和New (理解兩者差別)
        /// new: 覆寫基底類別
        /// override : 覆寫Virtual 類別
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //第一個
            Shape circleA = new Circle();
            circleA.MethodVirtual();
            circleA.Method();
            
            Circle circleB = new Circle();
            circleA.MethodVirtual();
            circleA.Method();

            //circleA 、 B 都是以下輸出
            //Circle Override MethodVirtual 成功
            //Base Method Call

            Shape triangleA = new Circle();
            triangleA.MethodVirtual();
            triangleA.Method();

            Triangle triangleB = new Triangle();
            triangleB.MethodVirtual();
            triangleB.Method();

            //triangleA 輸出以下:
            //Circle Override MethodVirtual 成功
            //Base Method Call

            //triangleB 輸出以下: (完全覆蓋過去)
            //triangle new MethodVirtual
            //triangle new Method

            Shape diamondA = new Diamond();
            diamondA.MethodVirtual();
            diamondA.Method();

            Diamond diamondB = new Diamond();
            diamondB.MethodVirtual();
            diamondB.Method();
            //diamond A:
            //base MethodVirtual call
            //Base Method Call

            //diamond B:
            //Diamond default MethodVirtual
            //Diamond default Method

        }

        public class Shape {
            public virtual void MethodVirtual()
            {
                Console.WriteLine("base MethodVirtual call");
            }

            public void Method()
            {
                Console.WriteLine("Base Method Call");
            }
        }

        class Circle : Shape
        {
            public override void MethodVirtual()
            {
                Console.WriteLine("Circle Override MethodVirtual 成功");
            }
        }

        class Triangle : Shape
        {
            public new void MethodVirtual()
            {
                Console.WriteLine("triangle new MethodVirtual");
            }

            public new void Method()
            {
                Console.WriteLine("triangle new Method");
            }
        }

        class Diamond : Shape {
            public void MethodVirtual()
            {
                Console.WriteLine("Diamond default MethodVirtual");
            }

            public void Method()
            {
                Console.WriteLine("Diamond default Method");
            }
        }
    }

}
