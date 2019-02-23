using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace picturebox複数サンプル
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.PictureBox[] pictureBoxes;
        public Form1()
        {
            InitializeComponent();

            int num = 2;
            this.pictureBoxes = new System.Windows.Forms.PictureBox[num];
            this.SuspendLayout();

            this.pictureBoxes[0] = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[0])).BeginInit();
            this.pictureBoxes[0].Name = "pictureBoxes[0]";
            this.pictureBoxes[0].Parent = this.pictureBox1;
            this.pictureBoxes[0].BackColor = Color.Transparent;
            //this.pictureBoxes[0].BackColor = Color.Red;
            this.pictureBoxes[0].Location = new Point(0, 0);
            this.pictureBoxes[0].Size = new Size(pictureBox1.Width, pictureBox1.Height);
            this.pictureBoxes[0].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBoxes[0].ImageLocation = @"test.png";
            this.Controls.AddRange(this.pictureBoxes);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[0])).EndInit();
            //
            this.pictureBoxes[1] = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[1])).BeginInit();
            this.pictureBoxes[1].Name = $"pictureBoxes{1}";
            this.pictureBoxes[1].Parent = this.pictureBoxes[0];
            this.pictureBoxes[1].BackColor = Color.Transparent;
            this.pictureBoxes[1].Location = new Point(0, 0);
            this.pictureBoxes[1].Size = new Size(pictureBox1.Width, pictureBox1.Height);
            this.pictureBoxes[1].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBoxes[1].ImageLocation = @"test.png";
            this.Controls.AddRange(this.pictureBoxes);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[1])).EndInit();



            this.ResumeLayout(false);






            this.pictureBoxes[0].SendToBack();
            this.pictureBox1.SendToBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pictureBoxes[1].Invalidate();
            this.pictureBoxes[1].Update();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            MessageBox.Show(((System.Windows.Forms.PictureBox)sender).Name);
            switch (((System.Windows.Forms.PictureBox)sender).Name)
            {
                case "pictureBoxes[0]":
                    break;
                case "pictureBoxes[1]":
                    break;
                default:
                    break;
            }
        }
    }
}
