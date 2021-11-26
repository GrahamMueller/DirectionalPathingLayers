using System;

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

        //OR creates a layer that contains the greatest of both axis from left and right.
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