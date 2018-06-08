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
    public partial class Game_Begin : Form
    {
        public Game_Begin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 用来设置游戏难度
        /// </summary>
        public static int time_interval = 100;

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 form = new Form1();
            if (radioButton1.Checked)
            {
                time_interval = 200;
            }
            else if (radioButton3.Checked)
            {
                time_interval = 50;
            }
            else if (radioButton4.Checked)
            {
                time_interval = 20;
            }
            else
            {
                time_interval = 100;
            }
            form.Show();

        }
    }
}
