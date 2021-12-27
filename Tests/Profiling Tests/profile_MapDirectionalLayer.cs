using System;

namespace DirectionalPathingLayers.Tests
{
    class Profile_MapDirectionalLayer
    {
        static void SimpleRun()
        {
            //Create a layer with blank nodes.
            MapDirectionalLayer mapDirectionalLayer = new MapDirectionalLayer(100, 100, new DirectionalNode(0));

            //Create a layer filled with 1s
            DirectionalLayer directionalLayer = new DirectionalLayer(5, 5);
            directionalLayer.Set(1);

            for (int i = 0; i < 10000; ++i)
            {
                mapDirectionalLayer.AddDirectionalLayerAtPoint(directionalLayer, 0, 0);
            }

            for (int i = 0; i < 10000; ++i)
            {
                mapDirectionalLayer.AddDirectionalLayerAtPoint(-1 * directionalLayer, 0, 0);
            }

            for (int i = 0; i < 10000; ++i)
            {
                mapDirectionalLayer.AddDirectionalLayerAtPoint(directionalLayer, 0, 0);
                mapDirectionalLayer.AddDirectionalLayerAtPoint(-1 * directionalLayer, 0, 0);
            }
        }
    }
}