using NUnit.Framework;

namespace DirectionalPathingLayers.Tests.Profiling_Tests
{
    class ProfileMapDirectionalLayer
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static MapDirectionalLayer mapLayer; //Common layer to avoid constructing when not neccesary
        static DirectionalLayer simpleLayer;
        static DirectionalNode simpleNode;

        [Test]
        public void TestConstructor()
        {
            dummyInt = 0;
            simpleNode = new DirectionalNode(0);
            profile_Benchmark.Benchmark(this.ProfileConstructor, 100000);
        }
        public void ProfileConstructor()
        {
            ProfileMapDirectionalLayer.mapLayer = new MapDirectionalLayer(10, 10, simpleNode);
        }

        [Test]
        public void TestAddLayer()
        {
            dummyInt = 0;
            simpleNode = new DirectionalNode(0);
            simpleLayer = new DirectionalLayer(3, 3);
            mapLayer = new MapDirectionalLayer(10, 10, simpleNode);
            profile_Benchmark.Benchmark(this.ProfileAddLayer, 100000);
        }
        public void ProfileAddLayer()
        {
            ProfileMapDirectionalLayer.mapLayer.AddDirectionalLayerAtPoint(simpleLayer, 0, 0);
        }
    }


}
