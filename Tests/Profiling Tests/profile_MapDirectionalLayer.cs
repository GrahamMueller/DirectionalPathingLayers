using NUnit.Framework;

namespace DirectionalPathingLayers.Tests.Profiling_Tests
{
    class profile_MapDirectionalLayer
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static MapDirectionalLayer mapLayer; //Common layer to avoid constructing when not neccesary
        static DirectionalLayer simpleLayer;
        static DirectionalNode simpleNode;

        [Test]
        public void test_Constructor()
        {
            dummyInt = 0;
            simpleNode = new DirectionalNode(0);
            profile_Benchmark.Benchmark(this.profile_Constructor, 100000);
        }
        public void profile_Constructor()
        {
            profile_MapDirectionalLayer.mapLayer = new MapDirectionalLayer(10, 10, simpleNode);
        }

        [Test]
        public void test_AddLayer()
        {
            dummyInt = 0;
            simpleNode = new DirectionalNode(0);
            simpleLayer = new DirectionalLayer(3, 3);
            mapLayer = new MapDirectionalLayer(10, 10, simpleNode);
            profile_Benchmark.Benchmark(this.profile_AddLayer, 100000);
        }
        public void profile_AddLayer()
        {
            profile_MapDirectionalLayer.mapLayer.AddDirectionalLayerAtPoint(simpleLayer, 0, 0);
        }
    }


}
