using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Good_Luck___External_Tool
{
    public partial class Form1 : Form
    {
        int tileMapWidth = 10;
        int tileMapHeight = 6;
        int totalTiles;
        List<PictureBox> tiles = new List<PictureBox>();

        List<PictureBox> gridImages = new List<PictureBox>();

        PictureBox topDoor;
        PictureBox bottomDoor;
        PictureBox leftDoor;
        PictureBox rightDoor;

        public Form1()
        {
            InitializeComponent();

            //Sets width of form to fix with the width of the tile grid
            Width = 518 + 50*tileMapWidth + 30;

            //Sets export button location to be centered under tile grid
            exportButton.Left = 500 + (50*tileMapWidth - exportButton.Width)/2;

            totalTiles = tileMapWidth * tileMapHeight;

            //Creates the array of drawable pictureboxes
            for (int i = 0; i < tileMapHeight; i++)
            {
                for (int j = 0; j < tileMapWidth; j++)
                {
                    //Generates the pictureboxes
                    PictureBoxGenerator(new Point(500+50*j, 50+50*i));
                }
            }

            //Sets value of default tile to "05ff", the empty floor tile
            currentTileSelected.Name = "05ff";

            //Saving all TileSelector PictureBoxes to a list 
            //So I can read in files and know what tile is what
            //There hopefully is a more efficient way to do this
            #region 
            gridImages.Add(gameTile01e1);
            gridImages.Add(gameTile02e1);
            gridImages.Add(gameTile03e1);
            gridImages.Add(gameTile04e1);
            gridImages.Add(gameTile05e1);
            gridImages.Add(gameTile06e1);
            gridImages.Add(gameTile07e1);
            gridImages.Add(gameTile08e1);
            gridImages.Add(gameTile09e1);
            gridImages.Add(gameTile01e2);
            gridImages.Add(gameTile02e2);
            gridImages.Add(gameTile03e2);
            gridImages.Add(gameTile04e2);
            gridImages.Add(gameTile05e2);
            gridImages.Add(gameTile06e2);
            gridImages.Add(gameTile07e2);
            gridImages.Add(gameTile08e2);
            gridImages.Add(gameTile09e2);
            gridImages.Add(gameTile01c1);
            gridImages.Add(gameTile02c1);
            gridImages.Add(gameTile03c1);
            gridImages.Add(gameTile04c1);
            gridImages.Add(gameTile05c1);
            gridImages.Add(gameTile06c1);
            gridImages.Add(gameTile07c1);
            gridImages.Add(gameTile08c1);
            gridImages.Add(gameTile09c1);
            gridImages.Add(gameTile01c2);
            gridImages.Add(gameTile02c2);
            gridImages.Add(gameTile03c2);
            gridImages.Add(gameTile04c2);
            gridImages.Add(gameTile05c2);
            gridImages.Add(gameTile06c2);
            gridImages.Add(gameTile07c2);
            gridImages.Add(gameTile08c2);
            gridImages.Add(gameTile09c2);
            gridImages.Add(gameTile01ff);
            gridImages.Add(gameTile02ff);
            gridImages.Add(gameTile03ff);
            gridImages.Add(gameTile04ff);
            gridImages.Add(gameTile05ff);
            gridImages.Add(gameTile06ff);
            gridImages.Add(gameTile07ff);
            gridImages.Add(gameTile08ff);
            gridImages.Add(gameTile09ff);
            gridImages.Add(gameTile10dd);
            gridImages.Add(gameTile11dd);
            gridImages.Add(gameTile12dd);
            gridImages.Add(gameTile13dd);
            #endregion 
        }

        //Changes held image
        private void TileSelector(object sender, EventArgs e)
        {
            PictureBox refBox = (PictureBox)sender;
            currentTileSelected.BackgroundImage = refBox.BackgroundImage;
            currentTileSelected.Name = refBox.Name.Substring(refBox.Name.Length-4);
        }

        //Changes PictureBox Image on MouseEnter
        private void TileColorerMouseEnter(object sender, EventArgs e)
        {
            PictureBox tileBox = (PictureBox)sender;
            TilePlacerHelper(tileBox);
        }

        //Changes PictureBox when Mouse is clicked inside Box
        private void MouseDownOnPictureBox(object sender, EventArgs e)
        {
            PictureBox tileBox = (PictureBox)sender;
            tileBox.Capture = false;

            TilePlacerHelper(tileBox);
        }

        private void TilePlacerHelper(PictureBox tileBox)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                
                //If the current tile is a door tile
                if (currentTileSelected.Name.Substring(2) == "dd")
                {
                    string tileClass = currentTileSelected.Name.Substring(0, 2);
                    int tileLocation = tiles.IndexOf(tileBox);

                    //Can only draw if there is less than 3 doors
                    if (tileLocation > 0 && tileLocation < tileMapWidth - 1 && tileClass == "10")
                    {
                        if(topDoor != null)
                            topDoor.BackgroundImage = gameTile05ff.BackgroundImage;

                        topDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                    if (tileLocation > totalTiles - tileMapWidth && tileLocation < totalTiles - 1 && tileClass == "12")
                    {
                        if (bottomDoor != null)
                            bottomDoor.BackgroundImage = gameTile05ff.BackgroundImage;

                        bottomDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                    if (tileLocation%tileMapWidth == 0 && tileLocation != 0 && tileLocation != totalTiles - tileMapWidth && tileClass == "13")
                    {
                        if (leftDoor != null)
                            leftDoor.BackgroundImage = gameTile05ff.BackgroundImage;

                        leftDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                    if (tileLocation % tileMapWidth == tileMapWidth-1 && tileLocation != tileMapWidth-1 && tileLocation != totalTiles - 1 && tileClass == "11")
                    {
                        if (rightDoor != null)
                            rightDoor.BackgroundImage = gameTile05ff.BackgroundImage;

                        rightDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                }
                //Any other tile draws normally
                else
                {
                    tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                    tileBox.Name = currentTileSelected.Name;
                }
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
            box.Name = "05ff";
            
            box.BackgroundImageLayout = ImageLayout.Stretch;
            box.Size = new Size(50, 50);
            box.Location = location;

            Controls.Add(box);
        }

        private void TextExport(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();

            //If valid name is chosen
            if (result == DialogResult.OK)
            {
                string output = $"";

                if (tileMapHeight > 0 && tileMapWidth > 0)
                {
                    for (int j = 0; j < tileMapWidth; j++)
                    {
                        output += tiles[j * tileMapHeight].Name + ",";

                        for (int i = 1; i < tileMapHeight; i++)
                        {
                            output += tiles[i + j * tileMapHeight].Name;
                            if (i < tileMapHeight - 1)
                            {
                                output += ",";
                            }
                        }

                        output += "\n";
                    }

                    StreamWriter writer = null;

                    try
                    {
                        writer = new StreamWriter(saveFileDialog1.FileName);
                        writer.WriteLine(output);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File could not be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        saveFileDialog1.FileName = saveFileDialog1.FileName.Split(@"\")[^1];

                        if (writer != null)
                            writer.Close();
                    }

                    //Displays that file was saved properly
                    MessageBox.Show("File saved successfully", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
