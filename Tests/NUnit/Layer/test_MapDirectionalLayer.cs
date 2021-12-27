using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace DirectionalPathingLayers.Tests
{
    public class test_DirectionalPathingLayers
    {
        [Test]
        public void SimpleAdd()
        {
            MapDirectionalLayer mapLayer = new MapDirectionalLayer(5, 5, new DirectionalNode(new int[] { 1, 1, 1, 1, 1, 1 }));

            //Create layer with alternating 1 and 0 spreading from 0.
            DirectionalLayer addingLayer = new DirectionalLayer(2, 2);

            mapLayer.AddDirectionalLayerAtPoint(addingLayer, 0, 0);
            DirectionalLayer slice = mapLayer.DirectionLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.Equals(~addingLayer));

            //Add same layer to map again.  Slice should not change.  Cost should change.
            mapLayer.AddDirectionalLayerAtPoint(addingLayer, 0, 0);
            slice = mapLayer.DirectionLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.Equals(~addingLayer));

        }

        [Test]
        public void SimpleAddSub_offset()
        {
            MapDirectionalLayer mapLayer = new MapDirectionalLayer(5, 5, new DirectionalNode(new int[] { 1, 1, 1, 1, 1, 1 }));

            //Create layer with alternating 1 and 0 spreading from 0.
            DirectionalLayer addingLayer = new DirectionalLayer(2, 2);

            mapLayer.AddDirectionalLayerAtPoint(addingLayer, 1, 1);
            DirectionalLayer slice = mapLayer.DirectionLayer.GetSlice(1, 1, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.Equals(~addingLayer));

            //Add same layer to map again.  Slice should not change.  Cost should change.
            mapLayer.AddDirectionalLayerAtPoint(addingLayer, 1, 1);
            slice = mapLayer.DirectionLayer.GetSlice(1, 1, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.Equals(~addingLayer));

            //Now subtract layer.  Should result in no change.
            mapLayer.AddDirectionalLayerAtPoint(-1 * addingLayer, 1, 1);
            slice = mapLayer.DirectionLayer.GetSlice(1, 1, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.Equals(~addingLayer));

            //Now subtract again layer.  Should result in initial layer that matches defaults.
            mapLayer.AddDirectionalLayerAtPoint(-1 * addingLayer, 1, 1);
            Assert.IsTrue(mapLayer.DirectionLayer.Equals(~(new DirectionalLayer(5, 5))));
        }

        [Test]
        public void SimpleAddSub_cost()
        {
            MapDirectionalLayer mapLayer = new MapDirectionalLayer(5, 5, new DirectionalNode(new int[] { 0, 0, 0, 0, 0, 0 }));

            //Create layer with alternating 1 and 0 spreading from 0.
            DirectionalLayer addingLayer = new DirectionalLayer(0, 0);

            //Add layer that is the same to mapLayer.  This should not modify it.
            mapLayer.AddDirectionalLayerAtPoint(addingLayer, 0, 0);
            DirectionalLayer slice = mapLayer.AddedCountLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.directionalNodes[0, 0].Equals(new DirectionalNode(0)));

            //Add layer once
            mapLayer.AddDirectionalLayerAtPoint(~addingLayer, 0, 0);
            slice = mapLayer.AddedCountLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.directionalNodes[0, 0].Equals(new DirectionalNode(1)));

            //Add layer twice
            mapLayer.AddDirectionalLayerAtPoint(~addingLayer, 0, 0);
            slice = mapLayer.AddedCountLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.directionalNodes[0, 0].Equals(new DirectionalNode(2)));

            //Subtract layer.  
            mapLayer.AddDirectionalLayerAtPoint(-1 * ~addingLayer, 0, 0);
            slice = mapLayer.AddedCountLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(slice.directionalNodes[0, 0].Equals(new DirectionalNode(1)));

            mapLayer.AddDirectionalLayerAtPoint(-1 * ~addingLayer, 0, 0);
            slice = mapLayer.AddedCountLayer.GetSlice(0, 0, addingLayer.GetSideWidth(), addingLayer.GetSideLength());
            Assert.IsTrue(mapLayer.AddedCountLayer.Equals((new DirectionalLayer(5, 5))));

        }

        struct DirectionalLayerTestType
        {
            public int centerX;
            public int centerY;
            public DirectionalLayer layer;
        };
        [Test]
        public void Test_LargeAddRemove()
        {
            MapDirectionalLayer mapLayer = new MapDirectionalLayer(50, 50, new DirectionalNode(new int[] { 0, 0, 0, 0, 0, 0 }));

            //Add a large number of points randomly around.  Some of these will exist entirely outside the map.
            List<DirectionalLayerTestType> addedLayersList = new List<DirectionalLayerTestType>();
            Random rand = new Random();
            int test_amount = 500;
            for (int i = 0; i < test_amount; ++i)
            {
                DirectionalLayerTestType newTestLayer;
                newTestLayer.centerX = rand.Next(-mapLayer.DirectionLayer.GetSideWidth() - 20, mapLayer.DirectionLayer.GetSideWidth() + 20);
                newTestLayer.centerY = rand.Next(-mapLayer.DirectionLayer.GetSideLength() - 20, mapLayer.DirectionLayer.GetSideLength() + 20);
                newTestLayer.layer = new DirectionalLayer(rand.Next(0, 10), rand.Next(0, 10));
                newTestLayer.layer.Set(new DirectionalNode(new int[] { rand.Next(0, 2), rand.Next(0, 2), rand.Next(0, 2), rand.Next(0, 2), rand.Next(0, 2), rand.Next(0, 2) }));
                addedLayersList.Add(newTestLayer);
                mapLayer.AddDirectionalLayerAtPoint(newTestLayer.layer, newTestLayer.centerX, newTestLayer.centerY);
            }

            foreach (DirectionalLayerTestType addedTestLayer in addedLayersList)
            {
                mapLayer.AddDirectionalLayerAtPoint(-1 * addedTestLayer.layer, addedTestLayer.centerX, addedTestLayer.centerY);
            }

            //Now get complete slice of map layer and compare it to empty.
            Assert.IsTrue(mapLayer.DirectionLayer.GetSlice(0, 0, 50, 50).Equals(new DirectionalLayer(50, 50)));
            Assert.IsTrue(mapLayer.AddedCountLayer.GetSlice(0, 0, 50, 50).Equals(new DirectionalLayer(50, 50)));
        }

        [TestCase(0, 0, true)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -1, false)]
        [TestCase(-1, -1, false)]
        [TestCase(10, 10, true)]
        [TestCase(10, 11, false)]
        [TestCase(11, 10, false)]
        [Test]
        public void Test_IsIndexPointInLayer(int indexX, int indexY, bool expected_result)
        {
            MapDirectionalLayer mapLayer = new MapDirectionalLayer(5, 5, new DirectionalNode(new int[] { 0, 0, 0, 0, 0, 0 }));
            Assert.AreEqual(mapLayer.IsIndexPointInLayer(indexX, indexY), expected_result);
        }
    }
}