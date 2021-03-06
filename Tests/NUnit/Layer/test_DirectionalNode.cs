using NUnit.Framework;
using System.Numerics;
using System;
namespace DirectionalPathingLayers.Tests
{
    public class test_DirectionalNode
    {
        [Test]
        public void Test_HardwareSupport()
        {
            Assert.IsTrue(Vector.IsHardwareAccelerated);
        }

        [Test]
        public void Test_Single()
        {
            Single single = 0.0f;

            for (double i = 0.0f; i < 100000000.0f; )
            {
                ++i;
                ++single;
                if (i != (double)single)
                {
                    Console.WriteLine($"Mismatch.  i={i}  single={single} ");
                    return;
                }
                //Assert.Equals(i, (double)single);
            }
        }

        [Test]
        public void Test_Invert()
        {
            DirectionalNode node_all_1 = new DirectionalNode(1,1,1,1,1,1);

            DirectionalNode node_all_0 = new DirectionalNode(0, 0, 0, 0, 0, 0);

            //Test ~
            Assert.IsTrue(node_all_0.Equals(~node_all_1));
            Assert.IsTrue(node_all_1.Equals(~node_all_0));
            Assert.IsTrue(node_all_1.Equals(~~node_all_1));
        }


        [Test]
        public void Test_Or()
        {
            DirectionalNode node_all_1 = new DirectionalNode(1,1,1,1,1,1);

            DirectionalNode node_all_0 = new DirectionalNode(0,0,0,0,0,0);

            DirectionalNode node_all_alt10 = new DirectionalNode(1, 0, 1, 0, 1, 0);
            DirectionalNode node_all_alt01 = new DirectionalNode(0, 1, 0, 1, 0, 1 );

            //Test |
            Assert.IsTrue(node_all_1.Equals(node_all_1 | node_all_1));
            Assert.IsTrue(node_all_1.Equals(node_all_1 | node_all_0));
            Assert.IsTrue(node_all_1.Equals(node_all_0 | node_all_1));
            Assert.IsTrue(node_all_0.Equals(node_all_0 | node_all_0));
            Assert.IsTrue(node_all_1.Equals(node_all_alt10 | node_all_alt01));
        }


        [Test]
        public void Test_XOr()
        {
            DirectionalNode node_all_1 = new DirectionalNode(1,1,1,1,1,1);

            DirectionalNode node_all_0 = new DirectionalNode(0,0,0,0,0,0);

            DirectionalNode node_all_alt10 = new DirectionalNode(1, 0, 1, 0, 1, 0);
            DirectionalNode node_all_alt01 = new DirectionalNode(0, 1, 0, 1, 0, 1 );

            //Test ^
            Assert.IsTrue(node_all_1.Equals(node_all_1 ^ node_all_0));
            Assert.IsTrue(node_all_1.Equals(node_all_0 ^ node_all_1));
            Assert.IsTrue(node_all_0.Equals(node_all_1 ^ node_all_1));
            Assert.IsTrue(node_all_0.Equals(node_all_0 ^ node_all_0));
            Assert.IsTrue(node_all_1.Equals(node_all_alt10 ^ node_all_alt01));
        }


        [Test]
        public void Test_And()
        {
            DirectionalNode node_all_1 = new DirectionalNode(1,1,1,1,1,1);

            DirectionalNode node_all_0 = new DirectionalNode(0,0,0,0,0,0);

            DirectionalNode node_all_alt10 = new DirectionalNode(1, 0, 1, 0, 1, 0);
            DirectionalNode node_all_alt01 = new DirectionalNode(0, 1, 0, 1, 0, 1 );

            //Test &
            Assert.IsTrue(node_all_1.Equals(node_all_1 & node_all_1));
            Assert.IsFalse(node_all_1.Equals(node_all_1 & node_all_0));
            Assert.IsTrue(node_all_0.Equals(node_all_alt10 & node_all_alt01));
        }


        [Test]
        public void Test_Equality()
        {
            DirectionalNode node_all_1 = new DirectionalNode(1,1,1,1,1,1);
            DirectionalNode node_all_1_same = node_all_1;
            DirectionalNode node_all_1_alt = new DirectionalNode(1,1,1,1,1,1);
            DirectionalNode node_all_0 = new DirectionalNode(0,0,0,0,0,0);

            ////Test ==
            //Assert.IsTrue(node_all_1 == node_all_1_same);
            //Assert.IsFalse(node_all_1 == node_all_1_alt);
            //Assert.IsFalse(node_all_1 == node_all_0);

            ////Test !=
            //Assert.IsFalse(node_all_1 != node_all_1_same);
            //Assert.IsTrue(node_all_1 != node_all_1_alt);
            //Assert.IsTrue(node_all_1 != node_all_0);

            //Test Equals
            Assert.IsTrue(node_all_1.Equals(node_all_1_same));
            Assert.IsTrue(node_all_1.Equals(node_all_1_alt));
            Assert.IsFalse(node_all_1.Equals(node_all_0));
        }

        [Test]
        public void Test_Add_Sub()
        {
            DirectionalNode node_alt05 = new DirectionalNode(0, 5, 0, 5, 0, 5);
            DirectionalNode node_alt50 = new DirectionalNode(5, 0, 5, 0, 5, 0);
            DirectionalNode node_alt55 = new DirectionalNode(5, 5, 5, 5, 5, 5);
            DirectionalNode node_alt1010 = new DirectionalNode(10, 10, 10, 10, 10, 10);

            DirectionalNode testNode;

            testNode = node_alt05 + node_alt50;
            Assert.IsTrue(node_alt55.Equals(testNode));

            testNode += node_alt55;
            Assert.IsTrue(node_alt1010.Equals(testNode));

            //Now subtract back to 0
            testNode -= node_alt55;
            Assert.IsTrue(node_alt55.Equals(testNode));

            testNode -= node_alt55;
            Assert.IsTrue(new DirectionalNode(0).Equals(testNode));
        }

        [Test]
        public void Test_DirectionNames()
        {
            int[] expectedValues = new int[6] { 1, 2, 3, 4, 5, 6 };
            DirectionalNode testNode = new DirectionalNode(expectedValues[0], expectedValues[1], expectedValues[2], expectedValues[3], expectedValues[4], expectedValues[5]);

            Assert.AreEqual(testNode.Forward, expectedValues[0]);
            Assert.AreEqual(testNode.Backward, expectedValues[1]);
            Assert.AreEqual(testNode.Left, expectedValues[2]);
            Assert.AreEqual(testNode.Right, expectedValues[3]);
            Assert.AreEqual(testNode.Up, expectedValues[4]);
            Assert.AreEqual(testNode.Down, expectedValues[5]);
        }
    }
}