using System;

namespace DirectionalPathingLayers
{
    /// <summary>
    /// Individual node that contains directions as a boolean value.
    /// </summary>
    public struct DirectionalNode
    {

        /// <summary> Array of directions, in a forward, backward, left, right, up, down format. </summary>
        
        public SByte[] Directions { get => this.directions; }
        SByte[] directions;

        public enum directionEnum : int
        {
            forward = 0,
            backward = 1,
            left = 2,
            right = 3,
            up = 4,
            down = 5
        };
        public SByte Forward { get => this.directions[(int)directionEnum.forward]; }
        public SByte Backward { get => this.directions[(int)directionEnum.backward]; }
        public SByte Left { get => this.directions[(int)directionEnum.left]; }
        public SByte Right { get => this.directions[(int)directionEnum.right]; }
        public SByte Up { get => this.directions[(int)directionEnum.up]; }
        public SByte Down { get => this.directions[(int)directionEnum.down]; }

        public DirectionalNode(int initialValue)
        {
            this.directions = new SByte[6];
            this.Set(initialValue);
        }


        public DirectionalNode(DirectionalNode initialNode)
        {
            this.directions = new SByte[6];
            for (int i = 0; i < this.directions.Length; ++i) { this.directions[i] = initialNode.directions[i]; }
        }


        public DirectionalNode(int forward, int backward, int left, int right, int up, int down)
        {
            this.directions = new SByte[6];
            this.directions[(int)directionEnum.forward] = (SByte)forward;
            this.directions[(int)directionEnum.backward] = (SByte)backward;
            this.directions[(int)directionEnum.left] = (SByte)left;
            this.directions[(int)directionEnum.right] = (SByte)right;
            this.directions[(int)directionEnum.up] = (SByte)up;
            this.directions[(int)directionEnum.down] = (SByte)down;
        }

        void Set(int setValue)
        {
            Array.Fill<SByte>(this.directions, (SByte)setValue);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < left.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)((left.directions[i] != 0 && right.directions[i] != 0) ? 1 : 0);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < left.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)((left.directions[i] != 0 || right.directions[i] != 0) ? 1 : 0);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < left.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)(left.directions[i] + right.directions[i]);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < left.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)(left.directions[i] - right.directions[i]);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < left.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)(left.directions[i] * right.directions[i]);
            }
            return newNode;
        }

        public static DirectionalNode operator *(int left, DirectionalNode right)
        {
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < right.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)(left * right.directions[i]);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < left.directions.Length; ++i)
            {
                //If left is 0 but not right, OR left is not 0 but right is, it's XOR
                newNode.directions[i] = (SByte)(((left.directions[i] == 0 && right.directions[i] != 0) ||
                                         (left.directions[i] != 0 && right.directions[i] == 0)) ? 1 : 0);
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
            DirectionalNode newNode = new DirectionalNode(0);
            for (int i = 0; i < node.directions.Length; ++i)
            {
                newNode.directions[i] = (SByte)(node.directions[i] == 0 ? 1 : 0);
            }
            return newNode;
        }

        public override string ToString()
        {
            return "[" + string.Join(",", this.directions) + "]";
        }

        public override bool Equals(object obj)
        {
            return this.Equals((DirectionalNode)obj);
        }

        public bool Equals(DirectionalNode other)
        {
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
}