using System;


/// <summary>
/// Individual node that contains directions as a boolean value.
/// </summary>
public class DirectionalNode : ICloneable, IEquatable<DirectionalNode>
{
    public int[] directions;


    public DirectionalNode()
    {
        this.directions = new int[2 + 2 + 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = 0; }
    }


    public DirectionalNode(int initialValue)
    {
        this.directions = new int[2 + 2 + 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = initialValue; }
    }

    public DirectionalNode(bool initialValue)
    {
        this.directions = new int[2 + 2 + 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = initialValue ? 1 : 0; }
    }

    public DirectionalNode(DirectionalNode initialNode)
    {
        this.directions = new int[2 + 2 + 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = initialNode.directions[i]; }
    }


    public DirectionalNode(int[] initialNodes)
    {
        this.directions = new int[6] { initialNodes[0], initialNodes[1], initialNodes[2], initialNodes[3], initialNodes[4], initialNodes[5] };
    }


    public void Set(bool setValue)
    {
        for (int i = 0; i < this.directions.Length; ++i)
        {
            this.directions[i] = setValue ? 1 : 0;
        }
    }

    public void Set(int setValue)
    {
        for (int i = 0; i < this.directions.Length; ++i)
        {
            this.directions[i] = setValue;
        }
    }


    /// <summary>
    /// Logical AND on each direction.
    /// This returns output as '1' or '0'. 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator &(DirectionalNode left, DirectionalNode right)
    {
        if (left is null || right is null) { return null; }

        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = (left.directions[i] != 0 && right.directions[i] != 0) ? 1 : 0;
        }
        return newNode;
    }


    /// <summary>
    /// Logical OR on each direction.
    /// This returns output as '1' or '0'. 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator |(DirectionalNode left, DirectionalNode right)
    {
        if (left is null || right is null) { return null; }

        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = (left.directions[i] != 0 || right.directions[i] != 0) ? 1 : 0;
        }
        return newNode;
    }


    /// <summary>
    /// integer AND on each element.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator +(DirectionalNode left, DirectionalNode right)
    {
        if (left is null || right is null) { return null; }

        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = left.directions[i] + right.directions[i];
        }
        return newNode;
    }


    /// <summary>
    /// integer SUB on each element.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator -(DirectionalNode left, DirectionalNode right)
    {
        if (left is null || right is null) { return null; }

        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = left.directions[i] - right.directions[i];
        }
        return newNode;
    }


    /// <summary>
    /// integer MULT on each element.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator *(DirectionalNode left, DirectionalNode right)
    {
        if (left is null || right is null) { return null; }
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = left.directions[i] * right.directions[i];
        }
        return newNode;
    }

    public static DirectionalNode operator *(int left, DirectionalNode right)
    {
        if (right is null) { return null; }
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < right.directions.Length; ++i)
        {
            newNode.directions[i] = left * right.directions[i];
        }
        return newNode;
    }

    /// <summary>
    /// Logical XOR on each direction.
    /// This returns output as '1' or '0'.  
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator ^(DirectionalNode left, DirectionalNode right)
    {
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            //If left is 0 but not right, OR left is not 0 but right is, it's XOR
            newNode.directions[i] = ((left.directions[i] == 0 && right.directions[i] != 0) ||
                                     (left.directions[i] != 0 && right.directions[i] == 0)) ? 1 : 0;
        }
        return newNode;
    }


    /// <summary>
    /// Logical invert on each direction.
    /// This returns output as '1' or '0'.  
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static DirectionalNode operator ~(DirectionalNode node)
    {
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < node.directions.Length; ++i)
        {
            newNode.directions[i] = node.directions[i] == 0 ? 1 : 0;
        }
        return newNode;
    }


    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public override string ToString()
    {
        return "[" + string.Join(",", this.directions) + "]";
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as DirectionalNode);
    }

    public bool Equals(DirectionalNode other)
    {
        if (other is null) { return false; }

        for (int i = 0; i < this.directions.Length; ++i)
        {
            if (this.directions[i] != other.directions[i]) { return false; }
        }
        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.directions);
    }
}
