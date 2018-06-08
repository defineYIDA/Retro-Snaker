using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Snake
{
    public partial class Form1 : Form
    {
        //定义成员变量
        /// <summary>
        /// 键盘状态，初始为   start
        /// </summary>
        string Key_Name = "start";
        /// <summary>
        /// 蛇身数组
        /// </summary>
        Label[] Snake_Boby = new Label[3000];
        /// <summary>
        /// 随机数,用于生成food
        /// </summary>
        Random R = new Random();
        /// <summary>
        /// 记录位置
        /// </summary>
        int Snake_Boby_content_x = 0, Snake_Boby_content_y = 0;
        /// <summary>
        /// 游戏分数
        /// </summary>
        int score;
        
        public Form1()
        {
            InitializeComponent();
        }
        //窗体初始化定义界面，初始化蛇身，生成随机food
        private void Form1_Load(object sender, EventArgs e)
        {
            

            //初始化一个label蛇体，长度为5个label，一个label height= width = 10
            for (int i = 0; i < 8; i++)
            {
                //蛇段
                Label Snake_Boby_content = new Label();
                Snake_Boby_content.Height = 10;
                Snake_Boby_content.Width = 10;
                //蛇段的位置
                Snake_Boby_content.Top = 400;
                Snake_Boby_content.Left = 400 - i * 10;

                Snake_Boby_content.BackColor = Color.Black;
                Snake_Boby_content.Text = "▉";
                //获取或设置包含有关控件的数据的对象。
                Snake_Boby_content.Tag = i;
                //加入蛇体
                Snake_Boby[i] = Snake_Boby_content;
                this.Controls.Add(Snake_Boby_content);
            }
            timer.Interval = Game_Begin.time_interval;
            //每隔一段时间发生一次右移
            timer.Tick += new EventHandler(Timer_Tick);
            //按键时发生的事件监控
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            Snake_food();    //food 生成
            timer.Start();    //Timer 开始计时
        }
        /// <summary>
        /// Food的生成事件
        /// </summary>
        public void Snake_food()
        {

            Label Food = new Label();
            Food.Width = 10;
            Food.Height = 10;
            //生成一个随机位置的food
            Food.Top = R.Next(1, 20) * 20;
            Food.Left = R.Next(1, 20) * 20;
            Food.Text = "❤";
            Food.Tag = "food";
            //Food.BackColor = Color.Gray;
            this.Controls.Add(Food);
        }
        /// <summary>
        /// 对键盘按键输入的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int x, y;
            x = Snake_Boby[0].Left;
            y = Snake_Boby[0].Top;
            //获取键盘代码
            Key_Name = e.KeyCode.ToString();
            if (Key_Name == "Right")   //向右
            {
                Snake_Boby[0].Left = x + 10;
                Snake_move(x, y);
                Snake_over();
            }
            if (Key_Name == "Up")    //向上
            {
                Snake_Boby[0].Top = y - 10;
                Snake_move(x, y);
                Snake_over();
            }
            if (Key_Name == "Down")     //向下
            {
                Snake_Boby[0].Top = y + 10;
                Snake_move(x, y);
                Snake_over();
            }
            if (Key_Name == "Left")    //向左
            {
                Snake_Boby[0].Left = x - 10;
                Snake_move(x, y);
                Snake_over();
            }
            //每按一次，判断是否与食物重合
            Eat_time();

        }
        /// <summary>
        /// snake的自动移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            //用来记录snake的head的xy坐标
            int x, y;
            x = Snake_Boby[0].Left;
            y = Snake_Boby[0].Top;
            if (Key_Name == "start")     //键盘状态处于初始状态
            {
                Snake_Boby[0].Left = x + 10;//Snake_Boby[0]右移10
                Snake_move(x, y);//调用
            }
            else if (Key_Name == "Right")   //键盘状态处于  向右 状态
            {
                Snake_Boby[0].Left = x + 10;
                Snake_move(x, y);
            }
            else if (Key_Name == "Up")     //键盘状态处于  向上  状态
            {
                Snake_Boby[0].Top = y - 10;
                Snake_move(x, y);
            }
            else if (Key_Name == "Down")   //键盘状态处于  向下  状态
            {
                Snake_Boby[0].Top = y + 10;
                Snake_move(x, y);
            }
            else if (Key_Name == "Left")    //键盘状态处于   向左 状态
            {
                Snake_Boby[0].Left = x - 10;
                Snake_move(x, y);
            }
            //  穿墙设置
            if (x > 500)
            {
                Snake_Boby[0].Left = 10; ;
            }
            if (x < 0)
            {
                Snake_Boby[0].Left = 490;
            }
            if (y > 500)
            {
                Snake_Boby[0].Top = 10;
            }
            if (y < 0)
            {
                Snake_Boby[0].Top = 490;
            }
            //每动一次，判断是否与食物重合
            Eat_time();

        }
        /// <summary>
        /// 蛇身移动事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Snake_move(int x, int y)
        {
            //记录xy的中间变量
            int temp_x = 0, temp_y = 0;

            //遍历蛇身进行移动
            for (int i = 1; Snake_Boby[i] != null; i++)
            {
                if(i >= 3)
                {
                    //将记录的前一个蛇段位置赋给中间变量
                    temp_x = Snake_Boby_content_x;
                    temp_y = Snake_Boby_content_y;
                }
                if (i == 1)
                {
                    //将记录蛇头的改变前的位置的x y赋给第一个蛇段，并记录蛇段的位置
                    temp_x  = Snake_Boby[i].Left;
                    temp_y = Snake_Boby[i].Top;
                    Snake_Boby[i].Left = x;
                    Snake_Boby[i].Top = y;
                }
                else
                {
                    //将记录前一个个蛇段的改变前的位置temp_赋给第二个蛇段,并记录改前位置
                    Snake_Boby_content_x = Snake_Boby[i].Left;
                    Snake_Boby_content_y = Snake_Boby[i].Top;
                    Snake_Boby[i].Left = temp_x;
                    Snake_Boby[i].Top = temp_y;
                }
            }
        }

        public void Eat_time()
        {
            double x1 = 20, y1 = 20, x2 = 20, y2 = 20;
                //遍历Controls中所有label
            foreach (Label lb in this.Controls)
            {
                //如果lb为food，将label的位置记录
                if (lb.Tag.ToString() == "food".ToString())
                {
                    x2 = lb.Left;
                    y2 = lb.Top;
                }
                //如果label为snake，将label的位置记录
                if (lb.Tag.ToString() == "0".ToString())
                {
                    x1 = lb.Left;  
                    y1 = lb.Top;  
                }
            }
            if (x2 == x1 && y2 == y1)
            {
                Snake_eat();
                //将食物移位
                foreach (Label lb in this.Controls)
                {
                    if (lb.Tag.ToString() == "food".ToString())
                    {
                        lb.Top = R.Next(1, 20) * 20;
                        lb.Left = R.Next(1, 20) * 20;
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int col = 50;
            int row = 50;
            int drawRow = 0;
            int drawCol = 0;
            Pen black = new Pen(Color.Gray, 1);
            //black.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            Graphics g = this.CreateGraphics();
            for (int i = 0; i <= row; i++)
            {
                g.DrawLine(black, 0, drawCol, 500, drawCol);
                drawCol += 10;
            }
            // 画垂直线
            for (int j = 0; j <= col; j++)
            {
                g.DrawLine(black, drawRow, 0, drawRow, 500);
                drawRow += 10;
            }
        }

        /// <summary>
        /// 蛇触碰到食物的事件
        /// </summary>
        private void Snake_eat()
        {
            int i = 0;
            //遍历到蛇尾
            for (; Snake_Boby[i] != null; i++) ;
            //蛇触碰到food蛇段加一，定义蛇段
            Label Snake_Boby_content = new Label();
            Snake_Boby_content.Width = 10;
            Snake_Boby_content.Height = 10;
            Snake_Boby_content.Top = Snake_Boby_content_y;
            Snake_Boby_content.Left = Snake_Boby_content_x;
            Snake_Boby_content.BackColor = Color.White;
            Snake_Boby_content.Text = "▉";
            Snake_Boby_content.Tag = i;
            Snake_Boby[i] = Snake_Boby_content;
            Snake_Boby_content.BackColor = Color.Black;
            this.Controls.Add(Snake_Boby_content);
        }
        public void Snake_over()
        {
            int x, y;
            //记录snake head位置
            x = Snake_Boby[0].Left;
            y = Snake_Boby[0].Top;
            //遍历看是否和snake body重合
            foreach (Label lb in this.Controls)
            {
                //将food排除
                if (lb.Tag.ToString() != "food".ToString())
                {
                    //出现重合
                    if ((lb.Left == x && lb.Top == y)&&lb.Tag.ToString()!="0")
                    {
                        this.Close();
                        MessageBox.Show("GAME OVER !", "提示！");
                        
                    }
                }
            }
        }

    }

}
