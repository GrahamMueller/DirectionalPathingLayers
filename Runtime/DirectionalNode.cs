using System;

/// <summary>
/// Individual node that contains 
/// </summary>
public class DirectionalNode : ICloneable, IEquatable<DirectionalNode>
{
    public bool[] directions;


    public DirectionalNode()
    {
        this.directions = new bool[2 * 2 * 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = false; }
    }


    public DirectionalNode(bool initialValue)
    {
        this.directions = new bool[2 * 2 * 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = initialValue; }
    }


    public DirectionalNode(DirectionalNode initialNode)
    {
        this.directions = new bool[2 * 2 * 2];
        for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = initialNode.directions[i]; }
    }


    public DirectionalNode(bool[] initialNodes)
    {
        this.directions = initialNodes[0..6];
    }


    public void Set(bool setValue)
    {
        for (int i = 0; i < this.directions.Length; ++i)
        {
            this.directions[i] = setValue;
        }
    }

    /// <summary>
    /// Logical AND on each direction.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator &(DirectionalNode left, DirectionalNode right)
    {
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = left.directions[i] & right.directions[i];
        }
        return newNode;
    }


    /// <summary>
    /// Logical OR on each direction.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static DirectionalNode operator |(DirectionalNode left, DirectionalNode right)
    {
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < left.directions.Length; ++i)
        {
            newNode.directions[i] = left.directions[i] | right.directions[i];
        }
        return newNode;
    }



    /// <summary>
    /// Logical invert on each direction.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static DirectionalNode operator ~(DirectionalNode node)
    {
        DirectionalNode newNode = new DirectionalNode();
        for (int i = 0; i < node.directions.Length; ++i)
        {
            newNode.directions[i] = !node.directions[i];
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