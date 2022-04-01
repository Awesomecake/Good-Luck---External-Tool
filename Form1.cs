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
        int tileMapHeight = 10;
        List<PictureBox> tiles = new List<PictureBox>();

        bool isPlayerPlaced = false;
        int numDoors = 0;

        public Form1()
        {
            InitializeComponent();

            //Sets width of form to fix with the width of the tile grid
            Width = 518 + 50*tileMapWidth + 30;

            //Sets export button location to be centered under tile grid
            exportButton.Left = 500 + (50*tileMapWidth - exportButton.Width)/2;

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
            if (MouseButtons == MouseButtons.Left)
            {
                PictureBox tileBox = (PictureBox)sender;

                //If the tile being drawn on was a player spawning tile, the tile is removed, so players = false
                if (tileBox.Name.Substring(2) == "pp")
                {
                    isPlayerPlaced = false;
                }

                //If the tile being drawn on was a door, tile is removed, so number of doors goes down
                if(tileBox.Name.Substring(2) == "dd")
                {
                    numDoors--;
                }

                //If the current tile is a player-spawning tile
                if (currentTileSelected.Name.Substring(2) == "pp")
                {
                    //It can only be drawn if there are no other player tiles
                    if (!isPlayerPlaced)
                    {
                        //Player tile is drawn, so true
                        isPlayerPlaced = true;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                }
                //If the current tile is a door tile
                else if (currentTileSelected.Name.Substring(2) == "dd")
                {
                    //Can only draw if there is less than 3 doors
                    if(numDoors < 3)
                    {
                        numDoors++;
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

        //Changes PictureBox when Mouse is clicked inside Box
        private void MouseDownOnPictureBox(object sender, EventArgs e)
        {
            PictureBox tileBox = (PictureBox)sender;
            tileBox.Capture = false;

            if (MouseButtons == MouseButtons.Left)
            {
                //If the tile being drawn on was a player spawning tile, the tile is removed, so players = false
                if (tileBox.Name.Substring(2) == "pp")
                {
                    isPlayerPlaced = false;
                }

                //If the tile being drawn on was a door, tile is removed, so number of doors goes down
                if (tileBox.Name.Substring(2) == "dd")
                {
                    numDoors--;
                }

                //If the current tile is a player-spawning tile
                if (currentTileSelected.Name.Substring(2) == "pp")
                {
                    //It can only be drawn if there are no other player tiles
                    if (!isPlayerPlaced)
                    {
                        //Player tile is drawn, so true
                        isPlayerPlaced = true;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                }
                //If the current tile is a door tile
                else if (currentTileSelected.Name.Substring(2) == "dd")
                {
                    //Can only draw if there is less than 3 doors
                    if (numDoors < 3)
                    {
                        numDoors++;
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
                        output += tiles[j * tileMapWidth].Name + ",";

                        for (int i = 1; i < tileMapHeight; i++)
                        {
                            output += tiles[i + j * tileMapWidth].Name;
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
