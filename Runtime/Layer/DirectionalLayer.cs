﻿using System;



/// <summary>
/// DirectionalLayer assumes the default node type is all blocked, or alternately can be treated as which directions differ from an undefined default node.
/// </summary>
class DirectionalLayer : IEquatable<DirectionalLayer>
{
    public DirectionalNode[,] directionalNodes;

    public DirectionalLayer(int perSideWidth, int perSideLength)
    {
        if (perSideWidth < 0) { perSideWidth = 0; }
        if (perSideLength < 0) { perSideLength = 0; }

        this.directionalNodes = new DirectionalNode[perSideWidth * 2 + 1, perSideLength * 2 + 1];
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y] = new DirectionalNode();
            }
        }
    }

    public void Set(bool setValue)
    {
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y] = new DirectionalNode(setValue);
            }
        }
    }

    public void Set(int setValue)
    {
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y] = new DirectionalNode(setValue);
            }
        }
    }

    public void Set(DirectionalNode setNode)
    {
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y] = setNode;
            }
        }
    }

    public void Set(DirectionalNode[,] setNodes)
    {
        if (setNodes.GetLength(0) != this.directionalNodes.GetLength(0) ||
            setNodes.GetLength(1) != this.directionalNodes.GetLength(1))
        {
            throw new ArgumentException("setNodes size invalid");
        }

        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y] = setNodes[x, y];
            }
        }
    }

    /// <summary>
    /// Sets all values in direction index to value set in setValue.
    /// This is used to set a single axis.
    /// </summary>
    /// <param name="directionIndex"></param>
    /// <param name="setValue"></param>
    public void Set(int directionIndex, int[,] setValue)
    {
        if (setValue.GetLength(0) != this.directionalNodes.GetLength(0) ||
            setValue.GetLength(1) != this.directionalNodes.GetLength(1))
        {
            throw new ArgumentException("setValue size invalid");
        }
        if (directionIndex < 0 || directionIndex >= this.directionalNodes[0, 0].directions.Length)
        {
            throw new ArgumentException("directionIndex index invalid");
        }

        //Set nodes
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y].directions[directionIndex] = setValue[x, y];
            }
        }
    }

    public void Set(int directionIndex, bool[,] setValue)
    {
        if (setValue.GetLength(0) != this.directionalNodes.GetLength(0) ||
            setValue.GetLength(1) != this.directionalNodes.GetLength(1))
        {
            throw new ArgumentException("setValue size invalid");
        }
        if (directionIndex < 0 || directionIndex >= this.directionalNodes[0, 0].directions.Length)
        {
            throw new ArgumentException("directionIndex index invalid");
        }

        //Set nodes
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                this.directionalNodes[x, y].directions[directionIndex] = setValue[x, y] ? 1 : 0;
            }
        }
    }

    public bool[,] Get_bools(int directionIndex)
    {
        if (directionIndex < 0 || directionIndex >= this.directionalNodes[0, 0].directions.Length)
        {
            throw new ArgumentException("directionIndex index invalid");
        }
        bool[,] boolArr = new bool[this.directionalNodes.GetLength(0), this.directionalNodes.GetLength(1)];
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                boolArr[x, y] = this.directionalNodes[x, y].directions[directionIndex] != 0 ? true : false;
            }
        }
        return boolArr;
    }

    public int[,] Get_ints(int directionIndex)
    {
        if (directionIndex < 0 || directionIndex >= this.directionalNodes[0, 0].directions.Length)
        {
            throw new ArgumentException("directionIndex index invalid");
        }
        int[,] intArr = new int[this.directionalNodes.GetLength(0), this.directionalNodes.GetLength(1)];
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                intArr[x, y] = this.directionalNodes[x, y].directions[directionIndex];
            }
        }
        return intArr;
    }
    /// <summary>
    /// Gets number of nodes to the left or right of the center node.  
    /// </summary>
    /// <returns></returns>
    public int GetSideWidth()
    {
        return (this.directionalNodes.GetLength(0) - 1) / 2;
    }

    /// <summary>
    /// Gets number of nodes to the forward or back of center node.
    /// </summary>
    /// <returns></returns>
    public int GetSideLength()
    {
        return (this.directionalNodes.GetLength(1) - 1) / 2;
    }


    public void Resize(int newWidth, int newLength)
    {
        DirectionalLayer resizedLayer = new DirectionalLayer(newWidth, newLength);

        resizedLayer |= this;
        this.directionalNodes = resizedLayer.directionalNodes;
    }

    /// <summary>
    /// Returns the nodes from a slice of this layer.  
    /// 
    /// If any point to slice were to fall outside the area already defined by this layer, 'none' is returned.
    /// 
    /// </summary>
    /// <param name="offsetFromCenterX">Offset from center of map to collect nodes.</param>
    /// <param name="offsetFromCenterY">Offset from center of map to collect nodes.</param>
    /// <param name="sliceSideWidth">Number of points to the left and right to collect from offset point.</param>
    /// <param name="sliceSideLength">Number of points to the forward and back to collect from offset point.</param>
    /// <returns></returns>
    public DirectionalLayer GetSlice(int offsetFromCenterX, int offsetFromCenterY, int sliceSideWidth, int sliceSideLength)
    {
        if (sliceSideWidth < 0) { sliceSideWidth = 0; }
        if (sliceSideLength < 0) { sliceSideLength = 0; }

        //Calculate index center.
        int indexCenterX = this.GetSideWidth() + 1;
        int indexCenterY = this.GetSideLength() + 1;

        int indexMinX = indexCenterX + offsetFromCenterX - sliceSideWidth - 1;
        int indexMaxX = indexCenterX + offsetFromCenterX + sliceSideWidth;

        int indexMinY = indexCenterY + offsetFromCenterY - sliceSideLength - 1;
        int indexMaxY = indexCenterY + offsetFromCenterY + sliceSideLength;

        //Ensure all points are in map
        if (indexMinX < 0 ||
            indexMaxX > this.directionalNodes.GetLength(0) ||
            indexMinY < 0 ||
            indexMaxY > this.directionalNodes.GetLength(1))
        {
            return null; //No support for members outside
        }


        DirectionalLayer returnLayer = new DirectionalLayer(sliceSideWidth, sliceSideLength);
        for (int x = indexMinX; x < indexMaxX; ++x)
        {
            for (int y = indexMinY; y < indexMaxY; ++y)
            {
                returnLayer.directionalNodes[x - indexMinX, y - indexMinY] = this.directionalNodes[x, y];
            }
        }
        return returnLayer;
    }


    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


    public override string ToString()
    {
        return null;
    }


    public override bool Equals(object obj)
    {
        return this.Equals(obj as DirectionalLayer);
    }


    public bool Equals(DirectionalLayer other)
    {
        if (other is null) { return false; }

        if (this.directionalNodes.GetLength(0) != other.directionalNodes.GetLength(0) |
            this.directionalNodes.GetLength(1) != other.directionalNodes.GetLength(1))
        {
            return false;
        }

        //No easy exit, check every node.
        for (int x = 0; x < this.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < this.directionalNodes.GetLength(1); ++y)
            {
                if (!this.directionalNodes[x, y].Equals(other.directionalNodes[x, y]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    //--Overloads
    /// <summary>
    /// Logical AND.  Returns a DirectionalLayer that is made up of the smallest dimensions of left and right.
    /// Acts as if both left and right are centered on each other.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalLayer operator &(DirectionalLayer left, DirectionalLayer right)
    {
        if (left is null || right is null) { return null; }

        //AND creates a layer that contains the smallest of both axis from left and right.
        int min_width = (left.GetSideWidth() <= right.GetSideWidth()) ? left.GetSideWidth() : right.GetSideWidth();
        int min_length = (left.GetSideLength() <= right.GetSideLength()) ? left.GetSideLength() : right.GetSideLength();

        DirectionalLayer newLayer = new DirectionalLayer(min_width, min_length);

        //From -width to +width, including 0
        for (int x = -newLayer.GetSideWidth(); x <= newLayer.GetSideWidth(); ++x)
        {
            for (int y = -newLayer.GetSideLength(); y <= newLayer.GetSideLength(); ++y)
            {

                int new_index_x = x + newLayer.GetSideWidth();
                int new_index_y = y + newLayer.GetSideLength();

                int left_index_x = x + left.GetSideWidth();
                int left_index_y = y + left.GetSideLength();

                int right_index_x = x + right.GetSideWidth();
                int right_index_y = y + right.GetSideLength();

                newLayer.directionalNodes[new_index_x, new_index_y] = left.directionalNodes[left_index_x, left_index_y] & right.directionalNodes[right_index_x, right_index_y];
            }
        }

        return newLayer;
    }


    /// <summary>
    /// Logical XOR.  Returns a DirectionalLayer that is made up of the smallest dimensions of left and right.
    /// Acts as if both left and right are centered on each other.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalLayer operator ^(DirectionalLayer left, DirectionalLayer right)
    {
        if (left is null || right is null) { return null; }

        //XOR creates a layer that contains the smallest of both axis from left and right.
        int min_width = (left.GetSideWidth() <= right.GetSideWidth()) ? left.GetSideWidth() : right.GetSideWidth();
        int min_length = (left.GetSideLength() <= right.GetSideLength()) ? left.GetSideLength() : right.GetSideLength();

        DirectionalLayer newLayer = new DirectionalLayer(min_width, min_length);

        //From -width to +width, including 0
        for (int x = -newLayer.GetSideWidth(); x <= newLayer.GetSideWidth(); ++x)
        {
            for (int y = -newLayer.GetSideLength(); y <= newLayer.GetSideLength(); ++y)
            {

                int new_index_x = x + newLayer.GetSideWidth();
                int new_index_y = y + newLayer.GetSideLength();

                int left_index_x = x + left.GetSideWidth();
                int left_index_y = y + left.GetSideLength();

                int right_index_x = x + right.GetSideWidth();
                int right_index_y = y + right.GetSideLength();

                newLayer.directionalNodes[new_index_x, new_index_y] = left.directionalNodes[left_index_x, left_index_y] ^ right.directionalNodes[right_index_x, right_index_y];
            }
        }

        return newLayer;
    }


    /// <summary>
    /// Logical OR on each node that exists in both 'left' and 'right' if centered.  
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns> Directional layer that contains both 'left' and 'right' OR'd together. Takes on greatest dimensions. </returns>
    public static DirectionalLayer operator |(DirectionalLayer left, DirectionalLayer right)
    {
        if (left is null || right is null) { return null; }

        //OR creates a layer that contains the greatest of both axis from left and right.
        int max_width = (left.GetSideWidth() > right.GetSideWidth()) ? left.GetSideWidth() : right.GetSideWidth();
        int max_length = (left.GetSideLength() > right.GetSideLength()) ? left.GetSideLength() : right.GetSideLength();

        DirectionalLayer newLayer = new DirectionalLayer(max_width, max_length);

        //From -width to +width, including 0
        for (int x = -newLayer.GetSideWidth(); x <= newLayer.GetSideWidth(); ++x)
        {
            for (int y = -newLayer.GetSideLength(); y <= newLayer.GetSideLength(); ++y)
            {
                bool left_in_bounds = (x >= -left.GetSideWidth() && x <= left.GetSideWidth()) && (y >= -left.GetSideLength() && y <= left.GetSideLength());
                bool right_in_bounds = (x >= -right.GetSideWidth() && x <= right.GetSideWidth()) && (y >= -right.GetSideLength() && y <= right.GetSideLength());

                //If both left and right are in bounds
                if (left_in_bounds && right_in_bounds)
                {
                    int new_index_x = x + newLayer.GetSideWidth();
                    int new_index_y = y + newLayer.GetSideLength();

                    int left_index_x = x + left.GetSideWidth();
                    int left_index_y = y + left.GetSideLength();

                    int right_index_x = x + right.GetSideWidth();
                    int right_index_y = y + right.GetSideLength();

                    newLayer.directionalNodes[new_index_x, new_index_y] = left.directionalNodes[left_index_x, left_index_y] | right.directionalNodes[right_index_x, right_index_y];
                }
                //If only left is in bounds
                else if (left_in_bounds)
                {
                    int new_index_x = x + newLayer.GetSideWidth();
                    int new_index_y = y + newLayer.GetSideLength();

                    int left_index_x = x + left.GetSideWidth();
                    int left_index_y = y + left.GetSideLength();

                    newLayer.directionalNodes[new_index_x, new_index_y] = left.directionalNodes[left_index_x, left_index_y];
                }
                //If only right is in bounds
                else if (right_in_bounds)
                {
                    int new_index_x = x + newLayer.GetSideWidth();
                    int new_index_y = y + newLayer.GetSideLength();

                    int right_index_x = x + right.GetSideWidth();
                    int right_index_y = y + right.GetSideLength();

                    newLayer.directionalNodes[new_index_x, new_index_y] = right.directionalNodes[right_index_x, right_index_y];
                }
                //Neither left nor right are in bounds
                else
                {
                    ;//Do nothing
                }

            }
        }

        return newLayer;
    }


    /// <summary>
    /// Product of left and right.  Returns a DirectionalLayer with the smallest dimensions of X and Y.
    /// Acts as if both left and right are centered on each other.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalLayer operator *(DirectionalLayer left, DirectionalLayer right)
    {
        if (left is null || right is null) { return null; }

        //AND creates a layer that contains the smallest of both axis from left and right.
        int min_width = (left.GetSideWidth() <= right.GetSideWidth()) ? left.GetSideWidth() : right.GetSideWidth();
        int min_length = (left.GetSideLength() <= right.GetSideLength()) ? left.GetSideLength() : right.GetSideLength();

        DirectionalLayer newLayer = new DirectionalLayer(min_width, min_length);

        //From -width to +width, including 0
        for (int x = -newLayer.GetSideWidth(); x <= newLayer.GetSideWidth(); ++x)
        {
            for (int y = -newLayer.GetSideLength(); y <= newLayer.GetSideLength(); ++y)
            {

                int new_index_x = x + newLayer.GetSideWidth();
                int new_index_y = y + newLayer.GetSideLength();

                int left_index_x = x + left.GetSideWidth();
                int left_index_y = y + left.GetSideLength();

                int right_index_x = x + right.GetSideWidth();
                int right_index_y = y + right.GetSideLength();

                newLayer.directionalNodes[new_index_x, new_index_y] = left.directionalNodes[left_index_x, left_index_y] * right.directionalNodes[right_index_x, right_index_y];
            }
        }

        return newLayer;
    }

    public static DirectionalLayer operator *(int left, DirectionalLayer right)
    {
        if (right is null) { return null; }

        //AND creates a layer that contains the smallest of both axis from left and right.
        int min_width = right.GetSideWidth();
        int min_length = right.GetSideLength();

        DirectionalLayer newLayer = new DirectionalLayer(min_width, min_length);

        //From -width to +width, including 0
        for (int x = -newLayer.GetSideWidth(); x <= newLayer.GetSideWidth(); ++x)
        {
            for (int y = -newLayer.GetSideLength(); y <= newLayer.GetSideLength(); ++y)
            {

                int new_index_x = x + newLayer.GetSideWidth();
                int new_index_y = y + newLayer.GetSideLength();

                int right_index_x = x + right.GetSideWidth();
                int right_index_y = y + right.GetSideLength();

                newLayer.directionalNodes[new_index_x, new_index_y] = left * right.directionalNodes[right_index_x, right_index_y];
            }
        }

        return newLayer;
    }


    public static DirectionalLayer operator ~(DirectionalLayer node)
    {
        if (node is null) { return null; }

        DirectionalLayer newMap = new DirectionalLayer(node.GetSideWidth(), node.GetSideLength());
        for (int x = 0; x < newMap.directionalNodes.GetLength(0); ++x)
        {
            for (int y = 0; y < newMap.directionalNodes.GetLength(1); ++y)
            {
                newMap.directionalNodes[x, y] = ~node.directionalNodes[x, y];
            }
        }
        return newMap;
    }
}