using NUnit.Framework;

namespace DirectionalPathingLayers.Tests
{
    public class test_DirectionalLayer
    {
        [Test]
        public void Test_Constructors()
        {
            DirectionalLayer layer = new DirectionalLayer(1, 2);
            Assert.IsTrue(layer.GetSideWidth() == 1);
            Assert.IsTrue(layer.GetSideLength() == 2);
        }


        [Test]
        public void Test_Resize()
        {
            DirectionalLayer centerPoint = new DirectionalLayer(0, 0);
            centerPoint.Set(1);

            centerPoint.Resize(1, 1);
            Assert.IsTrue(centerPoint.directionalNodes[0, 0].Equals(new DirectionalNode(0)));
            Assert.IsTrue(centerPoint.directionalNodes[1, 1].Equals(new DirectionalNode(1)));
            Assert.IsTrue(centerPoint.directionalNodes[2, 2].Equals(new DirectionalNode(0)));
        }

        [Test]
        public void Test_And()
        {
            DirectionalLayer allTrue = new DirectionalLayer(5, 5);
            allTrue.Set(1);
            DirectionalLayer allTrue_tall = new DirectionalLayer(5, 10);
            allTrue_tall.Set(1);
            DirectionalLayer allTrue_wide = new DirectionalLayer(10, 5);
            allTrue_wide.Set(1);

            DirectionalLayer allFalse = new DirectionalLayer(5, 5);


            Assert.IsTrue(allTrue.Equals(allTrue & allTrue));
            Assert.IsTrue(allFalse.Equals(allTrue & allFalse));

            Assert.IsTrue(allTrue.Equals(allTrue & allTrue_tall));
            Assert.IsTrue(allTrue.Equals(allTrue_tall & allTrue));
            Assert.IsTrue(allTrue.Equals(allTrue & allTrue_wide));
            Assert.IsTrue(allTrue.Equals(allTrue_tall & allTrue_wide));

            Assert.IsTrue(allFalse.Equals(allFalse & allFalse));
        }

        [Test]
        public void Test_Or()
        {
            DirectionalLayer allTrue = new DirectionalLayer(5, 5);
            allTrue.Set(1);
            DirectionalLayer allTrue_alt = new DirectionalLayer(5, 5);
            allTrue_alt.Set(1);
            DirectionalLayer allTrue_size = new DirectionalLayer(3, 3);
            allTrue_size.Set(1);
            DirectionalLayer allFalse = new DirectionalLayer(5, 5);

            //Same Size 
            Assert.IsTrue(allTrue.Equals(allTrue | allTrue));
            Assert.IsTrue(allTrue.Equals(allTrue | allFalse));
            Assert.IsTrue(allTrue.Equals(allFalse | allTrue));
            Assert.IsTrue(allFalse.Equals(allFalse | allFalse));
            Assert.IsFalse(allTrue.Equals(allFalse | allFalse));

        }

        //Very poor test.  Tests if OR returns bigger layers and appears correct at a couple points.
        [Test]
        public void Test_Or_Missized()
        {
            DirectionalLayer false_square = new DirectionalLayer(5, 5);
            DirectionalLayer true_square = new DirectionalLayer(5, 5);
            true_square.Set(1);

            DirectionalLayer true_tall = new DirectionalLayer(5, 10);
            true_tall.Set(1);

            DirectionalLayer false_wide = new DirectionalLayer(10, 5);
            DirectionalLayer true_wide = new DirectionalLayer(10, 5);
            true_wide.Set(1);

            DirectionalLayer false_big = new DirectionalLayer(10, 10);

            DirectionalLayer true_big = new DirectionalLayer(10, 10);
            true_big.Set(1);

            DirectionalLayer testLayer;

            //Test small to big and vice versa.
            testLayer = true_square | true_big;
            Assert.IsTrue(testLayer.Equals(true_big));
            testLayer = true_big | true_square;
            Assert.IsTrue(testLayer.Equals(true_big));

            //Test partial tall
            testLayer = false_wide | true_tall;
            Assert.IsTrue(testLayer.directionalNodes[0, 10].Equals(new DirectionalNode(0)));//Is center left node false?
            Assert.IsTrue(testLayer.directionalNodes[10, 10].Equals(new DirectionalNode(1)));//Is centernode true?
            Assert.IsTrue(testLayer.directionalNodes[20, 10].Equals(new DirectionalNode(0)));//Is center right node false?
            Assert.IsTrue(testLayer.directionalNodes[10, 20].Equals(new DirectionalNode(1)));//Is center top node true?

            //Test partial tall
            testLayer = true_tall | false_wide;
            Assert.IsTrue(testLayer.directionalNodes[0, 10].Equals(new DirectionalNode(0)));//Is center left node false?
            Assert.IsTrue(testLayer.directionalNodes[10, 10].Equals(new DirectionalNode(1)));//Is centernode true?
            Assert.IsTrue(testLayer.directionalNodes[20, 10].Equals(new DirectionalNode(0)));//Is center right node false?
            Assert.IsTrue(testLayer.directionalNodes[10, 20].Equals(new DirectionalNode(1)));//Is center top node true?
        }


        [Test]
        public void Test_Equals()
        {
            DirectionalLayer allTrue = new DirectionalLayer(5, 5);
            allTrue.Set(1);
            DirectionalLayer allTrue_alt = new DirectionalLayer(5, 5);
            allTrue_alt.Set(1);
            DirectionalLayer allTrue_size = new DirectionalLayer(3, 3);
            allTrue_size.Set(1);
            DirectionalLayer allFalse = new DirectionalLayer(5, 5);

            //Test ==
            Assert.IsTrue(allTrue == allTrue);
            Assert.IsFalse(allTrue == allTrue_alt);
            Assert.IsFalse(allTrue == allFalse);
            Assert.IsFalse(allTrue == allTrue_size);

            //Test !=
            Assert.IsFalse(allTrue != allTrue);
            Assert.IsTrue(allTrue != allTrue_alt);
            Assert.IsTrue(allTrue != allFalse);
            Assert.IsTrue(allTrue != allTrue_size);

            //Test Equals
            Assert.IsTrue(allTrue.Equals(allTrue_alt));
            Assert.IsFalse(allTrue.Equals(allTrue_size));
            Assert.IsFalse(allTrue.Equals(allFalse));
        }


        [Test]
        public void Test_Invert()
        {
            DirectionalLayer allTrue = new DirectionalLayer(5, 5);
            allTrue.Set(1);
            DirectionalLayer allFalse = new DirectionalLayer(5, 5);

            //Test ~
            Assert.IsTrue(allTrue.Equals(~allFalse));
            Assert.IsTrue(allFalse.Equals(~allTrue));
            Assert.IsTrue(allTrue.Equals(~~allTrue));
        }


        [Test]
        public void Test_GetSlice()
        {
            DirectionalLayer allTrue = new DirectionalLayer(5, 5);
            DirectionalLayer allTrue_34 = new DirectionalLayer(3, 4);

            DirectionalLayer allTrue_00 = new DirectionalLayer(0, 0);
            DirectionalLayer layer_22 = new DirectionalLayer(2, 2);
            allTrue.Set(1);
            allTrue_34.Set(1);
            allTrue_00.Set(1);

            //Special layer to test offset.
            layer_22.Set(1);
            layer_22.directionalNodes[0, 0] = new DirectionalNode(0);

            DirectionalLayer sliceLayer = allTrue.GetSlice(0, 0, 5, 5);
            Assert.IsTrue(allTrue.Equals(sliceLayer));

            sliceLayer = allTrue.GetSlice(0, 0, 3, 4);
            Assert.IsTrue(allTrue_34.Equals(sliceLayer));

            sliceLayer = allTrue.GetSlice(0, 0, 0, 0);
            Assert.IsTrue(allTrue_00.Equals(sliceLayer));

            //Test offset : 
            DirectionalLayer allTrue_11 = new DirectionalLayer(1, 1);
            allTrue_11.Set(1);
            sliceLayer = layer_22.GetSlice(-1, -1, 1, 1);
            Assert.IsTrue(sliceLayer.directionalNodes[0, 0].Equals(new DirectionalNode(0)));

            sliceLayer = layer_22.GetSlice(0, -1, 1, 1);
            Assert.IsTrue(sliceLayer.Equals(allTrue_11));

            sliceLayer = layer_22.GetSlice(-1, 0, 1, 1);
            Assert.IsTrue(sliceLayer.Equals(allTrue_11));
        }
    }
}