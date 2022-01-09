using System;
using System.Numerics;
namespace DirectionalPathingLayers
{
    /// <summary>
    /// Individual node that contains directions as a boolean value.
    /// </summary>
    public struct DirectionalNode
    {

        /// <summary> Array of directions, in a forward, backward, left, right, up, down format. </summary>

        public Matrix3x2 Directions { get => this.directions; }
        Matrix3x2 directions;

        public enum DirectionEnum
        {
            forward = 0, //M1,1
            backward = 1,//M1,2
            left = 2, //M2,1
            right = 3, //M2,2
            up = 4, //M3,1
            down = 5 //M,2
        };

        //Faster operations to get a single retrieved.
        public Single sForward { get => this.directions.M11; }
        public Single sBackward { get => this.directions.M12; }
        public Single sLeft { get => this.directions.M21; }
        public Single sRight { get => this.directions.M22; }
        public Single sUp { get => this.directions.M31; }
        public Single sDown { get => this.directions.M32; }

        public int Forward { get => (int)this.sForward; }
        public int Backward { get => (int)this.sBackward; }
        public int Left { get => (int)this.sLeft; }
        public int Right { get => (int)this.sRight; }
        public int Up { get => (int)this.sUp; }
        public int Down { get => (int)this.sDown; }

        public int DirectionByIndex(int index)
        {
            switch (index)
            {
                case (int)DirectionEnum.forward:
                    {
                        return this.Forward;
                    }
                case (int)DirectionEnum.backward:
                    {
                        return this.Backward;
                    }
                case (int)DirectionEnum.left:
                    {
                        return this.Left;
                    }
                case (int)DirectionEnum.right:
                    {
                        return this.Right;
                    }
                case (int)DirectionEnum.up:
                    {
                        return this.Up;
                    }
                case (int)DirectionEnum.down:
                    {
                        return this.Down;
                    }

                default:
                    {
                        throw new System.ArgumentException("Invalid index used");
                        return 0;
                    }
            }
        }

        public DirectionalNode(int initialValue)
        {

            //this.directions = new Matrix3x2();
            this.directions.M11 = initialValue;
            this.directions.M12 = initialValue;
            this.directions.M21 = initialValue;
            this.directions.M22 = initialValue;
            this.directions.M31 = initialValue;
            this.directions.M32 = initialValue;
        }


        public DirectionalNode(in DirectionalNode initialNode)
        {
            this.directions = initialNode.directions;
            
            //for (int i = 0; i < 6; ++i) { this.directions[i] = initialNode.directions[i]; }
        }


        public DirectionalNode(int forward, int backward, int left, int right, int up, int down)
        {
            this.directions.M11 = forward;
            this.directions.M12 = backward;
            this.directions.M21 = left;
            this.directions.M22 = right;
            this.directions.M31 = up;
            this.directions.M32 = down;

            //this.directions = new Matrix3x2(forward, backward, left, right, up, down);
        }

        DirectionalNode(Single forward, Single backward, Single left, Single right, Single up, Single down)
        {
            this.directions.M11 = forward;
            this.directions.M12 = backward;
            this.directions.M21 = left;
            this.directions.M22 = right;
            this.directions.M31 = up;
            this.directions.M32 = down;
        }

        DirectionalNode(Matrix3x2 directionsMatrix)
        {
            this.directions = directionsMatrix;
        }

        //void Set(int setValue)
        //{
        //    this.directions = new Matrix3x2(setValue, setValue, setValue, setValue, setValue, setValue);
        //}


        /// <summary>
        /// Logical AND on each direction.
        /// This returns output as '1' or '0'. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DirectionalNode operator &(DirectionalNode left, DirectionalNode right)
        {
            Single And(int leftValue, int rightValue)
            {
                return (leftValue != 0) && (rightValue != 0)? 1.0f : 0.0f;
            }
            return new DirectionalNode(And(left.Forward, right.Forward),
                                       And(left.Backward, right.Backward),
                                       And(left.Left, right.Left),
                                       And(left.Right, right.Right),
                                       And(left.Up, right.Up),
                                       And(left.Down, right.Down)
                                       );
            //for (int i = 0; i < 6; ++i)
            //{
            //    newNode.directions[i] = (SByte)((left.directions[i] != 0 && right.directions[i] != 0) ? 1 : 0);
            //}
            //return newNode;
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
            Single Or(int leftValue, int rightValue)
            {
                return (leftValue != 0) || (rightValue != 0) ? 1.0f : 0.0f;
            }
            return new DirectionalNode(Or(left.Forward, right.Forward),
                                       Or(left.Backward, right.Backward),
                                       Or(left.Left, right.Left),
                                       Or(left.Right, right.Right),
                                       Or(left.Up, right.Up),
                                       Or(left.Down, right.Down)
                                       );

            //DirectionalNode newNode = new DirectionalNode(0);
            //for (int i = 0; i < left.directions.Length; ++i)
            //{
            //    newNode.directions[i] = (SByte)((left.directions[i] != 0 || right.directions[i] != 0) ? 1 : 0);
            //}
            //return newNode;
        }


        /// <summary>
        /// integer AND on each element.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DirectionalNode operator +(DirectionalNode left, DirectionalNode right)
        {
            return new DirectionalNode(left.directions + right.directions);
        }


        /// <summary>
        /// integer SUB on each element.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DirectionalNode operator -(DirectionalNode left, DirectionalNode right)
        {
            return new DirectionalNode(left.directions - right.directions);
        }


        /// <summary>
        /// integer MULT on each element.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DirectionalNode operator *(in DirectionalNode left, in DirectionalNode right)
        {
            //DirectionalNode newNode;
            //newNode.directions = left.directions * right.directions;
            //return newNode;
            return new DirectionalNode(left.directions * right.directions);
        }

        public static DirectionalNode operator *(int left, DirectionalNode right)
        {
            Single singleLeft = (Single)left;
            return new DirectionalNode(singleLeft * right.sForward,
                                       singleLeft * right.sBackward,
                                       singleLeft * right.sLeft,
                                       singleLeft * right.sRight,
                                       singleLeft * right.sUp,
                                       singleLeft * right.sDown
                                       );
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
            Single XOr(int leftValue, int rightValue)
            {
                return ((leftValue == 0) && (rightValue != 0) ||
                        (leftValue != 0) && (rightValue == 0)) ? 1.0f : 0.0f;
            }
            return new DirectionalNode(XOr(left.Forward, right.Forward),
                                       XOr(left.Backward, right.Backward),
                                       XOr(left.Left, right.Left),
                                       XOr(left.Right, right.Right),
                                       XOr(left.Up, right.Up),
                                       XOr(left.Down, right.Down)
                                       );
        }


        /// <summary>
        /// Logical invert on each direction.
        /// This returns output as '1' or '0'.  
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static DirectionalNode operator ~(DirectionalNode node)
        {
            Single Invert(int value)
            {
                return (value == 0 ) ? 1.0f : 0.0f;
            }
            return new DirectionalNode(Invert(node.Forward),
                                       Invert(node.Backward),
                                       Invert(node.Left),
                                       Invert(node.Right),
                                       Invert(node.Up),
                                       Invert(node.Down)
                                       );
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
            return this.directions.Equals(other.directions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.directions);
        }
    }
}