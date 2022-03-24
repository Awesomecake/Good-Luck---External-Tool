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

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PictureBoxGenerator(new Point(500+50*i, 50+50*j));
                }
            }
        }

        private void TileSelector(object sender, EventArgs e)
        {
            PictureBox refBox = (PictureBox)sender;
            currentTileSelected.BackgroundImage = refBox.BackgroundImage;
        }

        private void TileColorer(object sender, EventArgs e)
        {
            PictureBox tileBox = (PictureBox)sender;
            tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
        }

        private void PictureBoxGenerator(Point location)
        {
            PictureBox box = new PictureBox();
            box.BackgroundImage = currentTileSelected.BackgroundImage;
            box.Click += TileColorer;
            box.BackgroundImageLayout = ImageLayout.Stretch;
            box.Size = new Size(50, 50);
            box.Location = location;

            Controls.Add(box);
        }
    }
}
