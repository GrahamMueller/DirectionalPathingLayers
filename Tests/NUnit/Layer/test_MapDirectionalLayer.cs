using NUnit.Framework;

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
        Assert.IsTrue(mapLayer.DirectionLayer.Equals(~(new DirectionalLayer(5,5))));
    }

    [Test]
    public void SimpleAddSub_cost()
    {
        MapDirectionalLayer mapLayer = new MapDirectionalLayer(5, 5, new DirectionalNode(new int[] { 0,0,0,0,0,0 }));

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
}

