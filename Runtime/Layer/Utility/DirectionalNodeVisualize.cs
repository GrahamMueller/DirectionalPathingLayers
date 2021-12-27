using System;
using System.Drawing;
//Add 'multiply block'

//Set background base per tile (this tile is red, this tile is blue)
//  Color background!
//  Set background tile!

namespace DirectionalPathingLayers
{
    class DirectionalNodeVisualize
    {

        Color[] directionalNodeColors;

        //Bitmap bitmap;

        int tileSize;

        Color[,] backgroundTileColor;
        Color[,] foregroundColors;
        /// <summary>
        /// 
        /// </summary>
        public void SetBackgroundTileColor(int indexX, int indexY, Color setColor)
        {
            this.backgroundTileColor[indexX, indexY] = setColor;
        }

        //public DirectionalNodeVisualize(DirectionalNodes[,] nodes, int tileSize)
        //{
        //    this.tileSize = tileSize;

        //    this.directionalNodeColors = new Color[6] { Color.Red, Color.Red, Color.Green, Color.Green, Color.Blue, Color.Blue };

        //    this.backgroundTileColor = new Color[tileSize, tileSize];
        //    for (int x = 0; x < this.backgroundTileColor.GetLength(0); ++x)
        //    {
        //        for (int y = 0; y < this.backgroundTileColor.GetLength(1); ++y)
        //        {
        //            this.SetBackgroundTileColor(x, y, Color.White);
        //        }
        //    }
        //}

        public DirectionalNodeVisualize(DirectionalNode[,] nodes, int tileSize, Color backgroundColor, Color[] directionColors)
        {
            this.tileSize = tileSize;

            this.directionalNodeColors = directionColors;

            this.backgroundTileColor = new Color[nodes.GetLength(0), nodes.GetLength(1)];
            for (int x = 0; x < this.backgroundTileColor.GetLength(0); ++x)
            {
                for (int y = 0; y < this.backgroundTileColor.GetLength(1); ++y)
                {
                    this.SetBackgroundTileColor(x, y, backgroundColor);
                }
            }

            this.foregroundColors = new Color[nodes.GetLength(0) * tileSize, nodes.GetLength(1) * tileSize];
            for (int x = 0; x < this.foregroundColors.GetLength(0); ++x)
            {
                for (int y = 0; y < this.foregroundColors.GetLength(1); ++y)
                {
                    this.foregroundColors[x, y] = Color.Empty;
                }
            }
            this.ApplyNodes(nodes);

        }

        public DirectionalNodeVisualize(DirectionalNode[,] nodes, int tileSize)
        {
            this.tileSize = tileSize;

            this.directionalNodeColors = new Color[6] { Color.Red, Color.Red, Color.Green, Color.Green, Color.Blue, Color.Blue };

            this.backgroundTileColor = new Color[nodes.GetLength(0), nodes.GetLength(1)];
            for (int x = 0; x < this.backgroundTileColor.GetLength(0); ++x)
            {
                for (int y = 0; y < this.backgroundTileColor.GetLength(1); ++y)
                {
                    this.SetBackgroundTileColor(x, y, Color.White);
                }
            }

            this.foregroundColors = new Color[nodes.GetLength(0) * tileSize, nodes.GetLength(1) * tileSize];
            for (int x = 0; x < this.foregroundColors.GetLength(0); ++x)
            {
                for (int y = 0; y < this.foregroundColors.GetLength(1); ++y)
                {
                    this.foregroundColors[x, y] = Color.Empty;
                }
            }
            this.ApplyNodes(nodes);
        }

        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(this.foregroundColors.GetLength(0), this.foregroundColors.GetLength(1));

            //Set background colors
            for (int x = 0; x < this.backgroundTileColor.GetLength(0); ++x)
            {
                for (int y = 0; y < this.backgroundTileColor.GetLength(1); ++y)
                {
                    for (int x_block = 0; x_block < this.tileSize; ++x_block)
                    {
                        for (int y_block = 0; y_block < this.tileSize; ++y_block)
                        {
                            bitmap.SetPixel(x * this.tileSize + x_block, y * this.tileSize + y_block, this.backgroundTileColor[x, y]);
                        }
                    }
                }
            }

            for (int x = 0; x < this.foregroundColors.GetLength(0); ++x)
            {
                for (int y = 0; y < this.foregroundColors.GetLength(1); ++y)
                {
                    if (this.foregroundColors[x, y] != Color.Empty)
                    {
                        bitmap.SetPixel(x, this.foregroundColors.GetLength(1) - 1 - y, this.foregroundColors[x, y]);
                    }
                }
            }

            return bitmap;
        }

        public void SaveAsPng(string fileName)
        {
            Bitmap bitmap = this.GetBitmap();

            bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// Creates a nefwf visual representation of nodes.  
        /// </summary>
        /// <param name="nodes"></param>
        void ApplyNodes(DirectionalNode[,] nodes)
        {
            for (int x = 0; x < nodes.GetLength(0); ++x)
            {
                for (int y = 0; y < nodes.GetLength(1); ++y)
                {
                    this.SetNodeColors(nodes[x, y], x, y);
                } //y
            } //x
        }

        /// <summary> Returns a 2D color image of an individual node. </summary>
        void SetNodeColors(DirectionalNode node, int indexX, int indexY)
        {
            int baseOffsetX = indexX * this.tileSize;
            int baseOffsetY = indexY * this.tileSize;

            //Handle creating small lines 
            if (node.Forward != 0)
            {
                this.DrawLine(baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2,
                              baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize,
                              this.directionalNodeColors[0]);
            }

            if (node.Backward != 0)
            {
                this.DrawLine(baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2,
                              baseOffsetX + this.tileSize / 2, baseOffsetY,
                              this.directionalNodeColors[1]);
            }

            if (node.Left != 0)
            {
                this.DrawLine(baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2,
                              baseOffsetX, baseOffsetY + this.tileSize / 2,
                              this.directionalNodeColors[2]);

            }

            if (node.Right != 0)
            {
                this.DrawLine(baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2,
                              baseOffsetX + this.tileSize, baseOffsetY + this.tileSize / 2,
                              this.directionalNodeColors[3]);

            }

            if (node.Up != 0)
            {
                this.DrawLine(baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2,
                              baseOffsetX + (int)(this.tileSize), baseOffsetY + (int)(this.tileSize),
                              this.directionalNodeColors[4]);

            }

            if (node.Down != 0)
            {
                this.DrawLine(baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2,
                              baseOffsetX + (int)(this.tileSize * 0.75f), baseOffsetY + (int)(this.tileSize * 0.25f),
                              this.directionalNodeColors[4]);
            }

            this.foregroundColors[baseOffsetX + this.tileSize / 2, baseOffsetY + this.tileSize / 2] = Color.Black;
        }

        void DrawLine(int startX, int startY, int endX, int endY, Color lineColor)
        {
            int dirWidth = endX - startX;
            int dirHeight = endY - startY;

            //Iterate number of points needed, which will be the longer width.
            int pointIterations = (Math.Abs(dirWidth) > Math.Abs(dirHeight)) ? Math.Abs(dirWidth) : Math.Abs(dirHeight);
            for (int i = 0; i < pointIterations; ++i)
            {
                int currentX = startX + (int)(dirWidth * i / pointIterations);
                int currentY = startY + (int)(dirHeight * i / pointIterations);
                if (currentX >= this.foregroundColors.GetLength(0))
                {
                    ;
                }
                if (currentY >= this.foregroundColors.GetLength(1))
                {
                    ;
                }
                this.foregroundColors[currentX, currentY] = lineColor;
            }
        }
    }
}
