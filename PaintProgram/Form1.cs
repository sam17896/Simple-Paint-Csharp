using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintProgram
{
    public partial class Form1 : Form
    {
        bool canPaint = false;
        bool DrawRectangle = false;
        bool DrawSquare = false;
        Graphics g;
        int? prevX;
        int? prevY;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            prevX = null;
            prevY = null;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canPaint = true;
            if (DrawSquare)
            {
                SolidBrush sb = new SolidBrush(toolStripButton1.ForeColor);
                g.FillRectangle(sb, e.X, e.Y, Convert.ToInt32(toolStripTextBox2.Text), Convert.ToInt32(toolStripTextBox2.Text));
                canPaint = false;
                DrawSquare = false;
            }
            if (DrawRectangle)
            {
                SolidBrush sb = new SolidBrush(toolStripButton1.ForeColor);
                g.FillRectangle(sb, e.X, e.Y, Convert.ToInt32(toolStripTextBox2.Text) * 2, Convert.ToInt32(toolStripTextBox2.Text));
                canPaint = false;
                DrawRectangle = false;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canPaint = false;
            prevX = null;
            prevX = null;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canPaint && toolStripTextBox1.Text!= "")
            {
                Pen p = new Pen(toolStripButton1.ForeColor, float.Parse(toolStripTextBox1.Text));
                g.DrawLine(p, new Point(prevX ?? e.X, prevY ?? e.Y), new Point(e.X, e.Y));

                prevX = e.X;
                prevY = e.Y;

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                toolStripButton1.ForeColor = cd.Color;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                toolStripButton2.ForeColor = cd.Color;
                panel1.BackColor = cd.Color;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            g.Clear(panel1.BackColor);
        }

        private void drawRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawRectangle = true;
        }

        private void drawSquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawSquare = true;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] imagePath = (string[]) e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in imagePath)
            {
                g.DrawImage(Image.FromFile(path), new Point(0, 0));
            }
        }
    }
}
