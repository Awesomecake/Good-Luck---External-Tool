using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Good_Luck___External_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    PictureBoxGenerator(new Point(500+50*i, 50+50*j));
                }
            }
        }

        //Changes PictureBox Image on Click
        private void TileColorer(object sender, EventArgs e)
        {
            PictureBox tileBox = (PictureBox)sender;
            tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
        }

        //Changes held image
        private void TileSelector(object sender, EventArgs e)
        {
            PictureBox refBox = (PictureBox)sender;
            currentTileSelected.BackgroundImage = refBox.BackgroundImage;
        }

        //Changes PictureBox Image on MouseEnter
        private void TileColorerMouseEnter(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                PictureBox tileBox = (PictureBox)sender;
                tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
            }
        }

        //Changes PictureBox when Mouse is clicked inside Box
        private void MouseDownOnPictureBox(object sender, EventArgs e)
        {
            PictureBox tileBox = (PictureBox)sender;
            tileBox.Capture = false;

            if (MouseButtons == MouseButtons.Left)
            {
                tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
            }
        }

        private void PictureBoxGenerator(Point location)
        {
            PictureBox box = new PictureBox();
            box.BackColor = Color.Blue;

            //box.Click += TileColorer;
            box.MouseEnter += TileColorerMouseEnter;
            box.MouseDown += MouseDownOnPictureBox;
            
            box.BackgroundImageLayout = ImageLayout.Stretch;
            box.Size = new Size(50, 50);
            box.Location = location;

            Controls.Add(box);
        }
    }
}
