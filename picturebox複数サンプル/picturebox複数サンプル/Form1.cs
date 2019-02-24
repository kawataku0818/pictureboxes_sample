using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace picturebox複数サンプル
{
    public partial class Form1 : Form
    {
        private Worker[] workers;
        private System.Windows.Forms.PictureBox[] pictureBoxes;
        public Form1()
        {
            InitializeComponent();

            int num = 20;
            // Worker
            //var bitmap = new Bitmap(@"test.png");
            var point = new Point(0, 0);
            workers = new Worker[num];
            for (int i = 0; i < workers.Length; i++)
            {
                var bitmap = new Bitmap($"worker_image/workers ({(i+1).ToString()}).png");
                workers[i] = new Worker(i,bitmap, point);
            }

            // 背景
            this.pictureBox1.Image = new Bitmap(@"test.png");

            // pictureBox
            this.pictureBoxes = new System.Windows.Forms.PictureBox[num];
            this.SuspendLayout();

            this.pictureBoxes[0] = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[0])).BeginInit();
            this.pictureBoxes[0].Name = "pictureBoxes0";
            //this.pictureBoxes[0].Parent = this.pictureBox1;
            this.pictureBoxes[0].BackColor = Color.Transparent;
            this.pictureBoxes[0].Location = new Point(0, 0);
            this.pictureBoxes[0].Size = new Size(pictureBox1.Width, pictureBox1.Height);
            this.pictureBoxes[0].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBoxes[0].Image = workers[0].Bitmap;
            this.Controls.AddRange(this.pictureBoxes);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[0])).EndInit();
            //
            for (int i = 1; i < pictureBoxes.Length; i++)
            {
                this.pictureBoxes[i] = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[i])).BeginInit();
                this.pictureBoxes[i].Name = $"pictureBoxes{i}";
                //this.pictureBoxes[i].Parent = this.pictureBoxes[i - 1];
                this.pictureBoxes[i].BackColor = Color.Transparent;
                this.pictureBoxes[i].Location = new Point(0, 0);
                this.pictureBoxes[i].Size = new Size(pictureBox1.Width, pictureBox1.Height);
                this.pictureBoxes[i].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
                this.pictureBoxes[i].Image = workers[i].Bitmap;
                this.Controls.AddRange(this.pictureBoxes);
                ((System.ComponentModel.ISupportInitialize)(this.pictureBoxes[i])).EndInit();
            }

            this.ResumeLayout(false);


            // ピクチャーボックスの順番を変更する
            for (int i = pictureBoxes.Length - 1; i >= 0; i--)
            {
                this.pictureBoxes[i].SendToBack();
            }
            this.pictureBox1.SendToBack();

            // 親を設定する。
            this.pictureBoxes[0].Parent = this.pictureBox1;
            for (int i = 1; i < pictureBoxes.Length; i++)
            {
                this.pictureBoxes[i].Parent = this.pictureBoxes[i-1];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pictureBoxes[3].Invalidate();
            //this.pictureBoxes[1].Update();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!Regex.IsMatch(((System.Windows.Forms.PictureBox)sender).Name, "pictureBoxes"))
            {
                return;
            }
            Regex regex = new Regex("pictureBoxes");
            string result = regex.Replace(((System.Windows.Forms.PictureBox)sender).Name, "");
            int index = Convert.ToInt32(result);
            e.Graphics.DrawImage(pictureBoxes[index].Image, workers[index].Point);
        }

        Random random = new Random();
        Stopwatch sw = new Stopwatch();
        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while(true)
                {
                    sw.Reset();
                    sw.Start();
                    for (int i = 0; i < workers.Length; i++)
                    {
                        workers[i].Point = new Point(random.Next(0, pictureBox1.Width - 1), random.Next(0, pictureBox1.Height - 1));
                    }
                    System.Threading.Thread.Sleep(100);
                    sw.Stop();
                    Debug.WriteLine(sw.ElapsedMilliseconds);
                }
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while(true)
                {
                    for (int i = 0; i < pictureBoxes.Length; i++)
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new Action<int>(update), i);
                        }
                        else
                        {
                            update(i);
                        }
                    }
                }
            });
        }

        void update(int i)
        {
            pictureBoxes[i].Invalidate();
            pictureBoxes[i].Update();
        }
    }

    class Worker
    {
        public int Id { get; private set; }
        public Bitmap Bitmap { get;  private set; }
        public Point Point { get; set; }

        public Worker(int id, Bitmap bitmap, Point point)
        {
            this.Id = id;
            this.Bitmap = bitmap;
            this.Point = point;
        }
    }
}
