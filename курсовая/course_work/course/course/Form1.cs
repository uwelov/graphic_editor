using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;




namespace course
{
    public partial class Form1 : Form
    {
        bool drawing, rectangleClick; GraphicsPath newPath; Point oldlocation;
        Pen newPen = new Pen(Color.Black); string selectColor = "";
        Rectangle rectangle = new Rectangle(10, 10, 200, 100);
        int rectangleX = 0, rectangleY = 0;


        public Form1()
        {
            InitializeComponent();
            drawing = false;
            //Pen newPen = new Pen(Color.Black);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap pic;
            pic = new Bitmap(picBox.Width, picBox.Height);
            picBox.Image = pic;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (picBox.Image == null)
            {
                MessageBox.Show("Создайте новый файл!");
            }
            if (e.Button == MouseButtons.Left)
            {
                //if (selectColor == "Red")
                //{
                //    new Pen(Color.Red);
                //}
                drawing = true;
                oldlocation = e.Location;
                newPath = new GraphicsPath();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog windSave = new SaveFileDialog();
            windSave.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png";
            windSave.Title = "Сохранить файл";
            windSave.FilterIndex = 2;
            windSave.ShowDialog();
            if (windSave.FileName != "")
            { 
                System.IO.FileStream fileType = (System.IO.FileStream)windSave.OpenFile();
                switch (windSave.FilterIndex)
                {
                    case 1:
                        picBox.Image.Save(fileType, ImageFormat.Jpeg);
                        break;
                    case 2:
                        picBox.Image.Save(fileType, ImageFormat.Png);
                        break;
                }
                fileType.Close();
            }
            if (picBox.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(sender, e);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog(); 
            fileOpen.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png";
            fileOpen.Title = "Открыть файл";
            fileOpen.FilterIndex = 1;
            if (fileOpen.ShowDialog()!= DialogResult.Cancel)
            {
                picBox.Load(fileOpen.FileName);
            }
            picBox.AutoSize = true;
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            try 
            {
                newPath.Dispose();
            }
            catch 
            { 

            };
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing == true)
            {
                Graphics g = Graphics.FromImage(picBox.Image); 
                newPath.AddLine(oldlocation, e.Location); 
                g.DrawPath(newPen, newPath); 
                oldlocation = e.Location; 
                g.Dispose(); 
                picBox.Invalidate();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
             //selectColor = "Red";
        }

        private void toolStripButton2_Click(object sender, //Mouse
                                                           EventArgs e)
        {
            //if ((e.X < rectangle.X + rectangle.Width) && (e.X > rectangle.X))
            //{


            //    if ((e.Y < rectangle.Y + rectangle.Height) && (e.Y > rectangle.Y))
            //    {


            //        rectangleClick = true;
            //        rectangleX = e.X - rectangle.X;
            //        rectangleY = e.Y - rectangle.Y;
            //    }
            //}
        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, rectangle);

        }
    }
}
