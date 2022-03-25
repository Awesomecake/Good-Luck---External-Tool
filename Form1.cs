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
        int tileMapWidth = 10;
        int tileMapHeight = 10;
        List<PictureBox> tiles = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < tileMapHeight; i++)
            {
                for (int j = 0; j < tileMapWidth; j++)
                {
                    PictureBoxGenerator(new Point(500+50*j, 50+50*i));
                }
            }

            currentTileSelected.Name = "53";
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
            currentTileSelected.Name = refBox.Name.Substring(refBox.Name.Length-2);
        }

        //Changes PictureBox Image on MouseEnter
        private void TileColorerMouseEnter(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                PictureBox tileBox = (PictureBox)sender;
                tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                tileBox.Name = currentTileSelected.Name;
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
                tileBox.Name = currentTileSelected.Name;
            }
        }

        private void PictureBoxGenerator(Point location)
        {
            PictureBox box = new PictureBox();
            tiles.Add(box);
            box.BackgroundImage = currentTileSelected.BackgroundImage;

            //box.Click += TileColorer;
            box.MouseEnter += TileColorerMouseEnter;
            box.MouseDown += MouseDownOnPictureBox;
            box.Name = "53";
            
            box.BackgroundImageLayout = ImageLayout.Stretch;
            box.Size = new Size(50, 50);
            box.Location = location;

            Controls.Add(box);
        }

        private Bitmap MergeImagesHorizontal(Image image1, Image image2)
        {
            Bitmap bitmap = new Bitmap(image1.Width + image2.Width, Math.Max(image1.Height, image2.Height));
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(image1, 0, 0);
                g.DrawImage(image2, image1.Width, 0);
            }

            return bitmap;
        }

        private Bitmap MergeImagesVertical(Image image1, Image image2)
        {
            Bitmap bitmap = new Bitmap(Math.Max(image1.Width, image2.Width),image1.Height + image2.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(image1, 0, 0);
                g.DrawImage(image2, 0, image1.Height);
            }

            return bitmap;
        }

        private void ButtonExport(object sender, EventArgs e)
        {
            List<Bitmap> columns = new List<Bitmap>();

            string output = "";

            if (tileMapHeight > 0 && tileMapWidth > 0)
            {
                Bitmap currentImage = null;

                for (int j = 0; j < tileMapWidth; j++)
                {
                    currentImage = new Bitmap(tiles[j*tileMapWidth].BackgroundImage);

                    output += tiles[j * tileMapWidth].Name;

                    for (int i = 1; i < tileMapHeight; i++)
                    {
                        currentImage = MergeImagesHorizontal(currentImage, tiles[i + j * tileMapWidth].BackgroundImage);
                        
                        output += tiles[i + j * tileMapWidth].Name;
                    }

                    columns.Add(currentImage);

                    output += "\n";
                }

                currentImage = columns[0];
                for (int i = 1; i < tileMapWidth; i++)
                {
                    currentImage = MergeImagesVertical(currentImage, columns[i]);
                }

                System.Diagnostics.Debug.WriteLine(output);
                currentImage.Save("../../../BackgroundImageFinal.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
