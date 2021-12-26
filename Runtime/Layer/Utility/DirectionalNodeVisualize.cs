using System.Drawing;

namespace DirectionalPathingLayers
{
    class DirectionalNodeVisualize
    {
        Color backgroundColor;

        Color[] directionalNodeColors;

        Bitmap bitmap;

        int tileSize;
        public DirectionalNodeVisualize(int tileSize)
        {
            this.tileSize = tileSize;
        }

        /// <summary>
        /// Creates a nefwf visual representation of nodes.  
        /// </summary>
        /// <param name="nodes"></param>
        public void ApplyNodes(DirectionalNode[,] nodes)
        {
            this.bitmap = this.GetBlankBitmap(nodes.GetLength(0), nodes.GetLength(1));

            for (int x = 0; x < nodes.GetLength(0); ++x)
            {
                for (int y = 0; y < nodes.GetLength(1); ++y)
                {
                    Color[,] nodeColors = this.GetNodeColors(nodes[x, y]);

                    //Set returned pixels in bitmap
                    for (int x_node = 0; x_node < nodeColors.GetLength(0); ++x_node)
                    {
                        for (int y_node = 0; y_node < nodeColors.GetLength(1); ++y_node)
                        {
                            this.bitmap.SetPixel(x * this.tileSize + x_node, y * this.tileSize + y_node, nodeColors[x_node, y_node]);
                        } //y_node
                    } //x_node
                } //y
            } //x
        }

        private Bitmap GetBlankBitmap(int nodesWidth, int nodesHeight)
        {
            Bitmap bitmap = new Bitmap(nodesWidth * this.tileSize, nodesHeight * this.tileSize);
            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    this.bitmap.SetPixel(x, y, this.backgroundColor);
                }
            }
            return bitmap;
        }

        /// <summary> Returns a 2D color image of an individual node. </summary>
        Color[,] GetNodeColors(DirectionalNode node)
        {
            Color[,] nodeColors = new Color[this.tileSize, this.tileSize];

            //Set background
            for (int x = 0; x < nodeColors.GetLength(0); ++x)
            {
                for (int y = 0; y < nodeColors.GetLength(1); ++y)
                {
                    nodeColors[x, y] = this.backgroundColor;
                }
            }

            //Create small lines indicating direction.
            int widthTop = nodeColors.GetLength(0);
            int widthCenter = widthTop / 2;

            int heightTop = nodeColors.GetLength(1);
            int heightCenter = heightTop / 2;

            //Forward
            if (node.directions[0] != 0)
            {
                int x = widthCenter;
                for (int y = heightCenter; y < heightTop; ++y)
                {
                    nodeColors[x, y] = this.directionalNodeColors[0];
                }

            }

            //Back
            if (node.directions[1] != 0)
            {
                int x = widthCenter;
                for (int y = 0; y < heightCenter; ++y)
                {
                    nodeColors[x, y] = this.directionalNodeColors[1];
                }

            }

            //Left
            if (node.directions[2] != 0)
            {
                int y = heightCenter;
                for (int x = 0; x < widthCenter; ++x)
                {
                    nodeColors[x, y] = this.directionalNodeColors[2];
                }

            }

            //Right
            if (node.directions[3] != 0)
            {
                int y = heightCenter;
                for (int x = widthCenter; x < widthTop; ++x)
                {
                    nodeColors[x, y] = this.directionalNodeColors[3];
                }

            }

            //Up
            if (node.directions[4] != 0)
            {
                for (int x = widthCenter; x < widthTop; ++x)
                {
                    for (int y = heightCenter; y < heightTop; ++y)
                    {
                        nodeColors[x, y] = this.directionalNodeColors[4];
                    }
                }

            }

            //Down
            if (node.directions[5] != 0)
            {
                for (int x = widthCenter; x < widthTop; ++x)
                {
                    for (int y = heightTop; y >= 0; ++y)
                    {
                        nodeColors[x, y] = this.directionalNodeColors[5];
                    }
                }

            }

            return nodeColors;

        }
    }
}
