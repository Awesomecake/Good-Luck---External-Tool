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
            loadButton.Left = 500 + (50 * tileMapWidth - loadButton.Width) / 2;

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
            gridImages.Add(gameTile14ff);
            gridImages.Add(gameTile15ff);
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
                        {
                            topDoor.BackgroundImage = gameTile05ff.BackgroundImage;
                            topDoor.Name = "05ff";
                        }

                        topDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                    if (tileLocation > totalTiles - tileMapWidth && tileLocation < totalTiles - 1 && tileClass == "12")
                    {
                        if (bottomDoor != null)
                        {
                            bottomDoor.BackgroundImage = gameTile05ff.BackgroundImage;
                            bottomDoor.Name = "05ff";
                        }

                        bottomDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                    if (tileLocation%tileMapWidth == 0 && tileLocation != 0 && tileLocation != totalTiles - tileMapWidth && tileClass == "13")
                    {
                        if (leftDoor != null)
                        {
                            leftDoor.BackgroundImage = gameTile05ff.BackgroundImage;
                            leftDoor.Name = "05ff";
                        }

                        leftDoor = tileBox;
                        tileBox.BackgroundImage = currentTileSelected.BackgroundImage;
                        tileBox.Name = currentTileSelected.Name;
                    }
                    if (tileLocation % tileMapWidth == tileMapWidth-1 && tileLocation != tileMapWidth-1 && tileLocation != totalTiles - 1 && tileClass == "11")
                    {
                        if (rightDoor != null)
                        {
                            rightDoor.BackgroundImage = gameTile05ff.BackgroundImage;
                            rightDoor.Name = "05ff";
                        }

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

        private void PictureBoxLoadGenerator(Point location, string image)
        {
            PictureBox box = new PictureBox();
            tiles.Add(box);

            for (int i = 0; i < gridImages.Count; i++)
            {
                if(gridImages[i].Name.Substring(8) == image)
                {
                    box.BackgroundImage = gridImages[i].BackgroundImage;
                    break;
                }
            }


            //box.Click += TileColorer;
            box.MouseEnter += TileColorerMouseEnter;
            box.MouseDown += MouseDownOnPictureBox;
            box.Name = image;

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


                output += $"{tiles.IndexOf(topDoor)}\n";

                output += $"{tiles.IndexOf(leftDoor)}\n";

                output += $"{tiles.IndexOf(rightDoor)}\n";

                output += $"{tiles.IndexOf(bottomDoor)}\n";

                if (tileMapHeight > 0 && tileMapWidth > 0)
                {
                    for (int j = 0; j < tileMapHeight; j++)
                    {
                        for (int i = 0; i < tileMapWidth; i++)
                        {
                            output += tiles[i + j*tileMapWidth].Name;
                            if (i != tileMapWidth-1)
                                output += ",";
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

        private void LoadData(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if(result == DialogResult.OK)
            {
                StreamReader reader = null;
                try
                {
                    //Reads from file location
                    reader = new StreamReader(openFileDialog1.FileName);

                    //Splits line on | character
                    int topDoorLoc = int.Parse(reader.ReadLine());
                    int leftDoorLoc = int.Parse(reader.ReadLine());
                    int rightDoorLoc = int.Parse(reader.ReadLine());
                    int bottomDoorLoc = int.Parse(reader.ReadLine());

                    string line = null;

                    //Clears all PictureBoxes that existed
                    if (tiles != null)
                    {
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            tiles[i].Dispose();
                        }
                    }

                    //Creates a new array with accurate width and height values
                    tiles = new List<PictureBox>();

                    //Loops through array and fills it with PictureBoxes

                    int x = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] split = line.Split(",");

                        for (int i = 0; i < split.Length; i++)
                        {
                            PictureBoxLoadGenerator(new Point(500 + 50 * i, 50 + 50 * x), split[i]);
                        }

                        x++;
                    }

                    //Sets width of form to fix with the width of the tile grid
                    Width = 518 + 50 * tileMapWidth + 30;

                    //Sets export button location to be centered under tile grid
                    exportButton.Left = 500 + (50 * tileMapWidth - exportButton.Width) / 2;
                    loadButton.Left = 500 + (50 * tileMapWidth - loadButton.Width) / 2;

                    totalTiles = tileMapWidth * tileMapHeight;

                    if (topDoorLoc == -1)
                        topDoor = null;
                    else
                    {
                        topDoor = tiles[topDoorLoc];
                    }

                    if (leftDoorLoc == -1)
                        leftDoor = null;
                    else
                    {
                        leftDoor = tiles[leftDoorLoc];
                    }

                    if (rightDoorLoc == -1)
                        rightDoor = null;
                    else
                    {
                        rightDoor = tiles[rightDoorLoc];
                    }

                    if (bottomDoorLoc == -1)
                        bottomDoor = null;
                    else
                    {
                        bottomDoor = tiles[bottomDoorLoc];
                    }
                }
                catch (Exception)
                {
                    //Displays that an error occurs and disposes the invalid Level Designer
                    MessageBox.Show("File could not be loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Dispose();
                    return;
                }
                finally
                {
                    //Sets FileDialog's default name to be the file chosen
                    openFileDialog1.FileName = openFileDialog1.FileName.Split("\\")[^1];

                    //Closes StreamReader
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }

                //Displays that the file was loaded successfully
                MessageBox.Show("File loaded successfully", "File loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
