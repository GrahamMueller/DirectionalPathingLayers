using NUnit.Framework;

namespace DirectionalPathingLayers.Tests.Profiling_Tests
{
    class profile_DirectionalLayer
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static DirectionalNode simpleNode; //Common node to avoid constructing when not neccesary

        //Test speed of direction access
        [Test]
        public void test_Directions()
        {
            simpleNode = new DirectionalNode();
            dummyInt = 0;
            profile_Benchmark.Benchmark(this.profile_Directions, 100000000);
        }
        public void profile_Directions()
        {
            if (profile_DirectionalLayer.simpleNode.Forward == 0 &&
                profile_DirectionalLayer.simpleNode.Backward == 0 &&
                profile_DirectionalLayer.simpleNode.Left == 0 &&
                profile_DirectionalLayer.simpleNode.Right == 0 &&
                profile_DirectionalLayer.simpleNode.Up == 0 &&
                profile_DirectionalLayer.simpleNode.Down == 0)
            {
                dummyInt++;
            }
        }

        //Test speed of construction
        [Test]
        public void test_Constructor()
        {
            dummyInt = 0;
            profile_Benchmark.Benchmark(this.profile_Constructor, 100000000);
        }
        public void profile_Constructor()
        {
            profile_DirectionalLayer.simpleNode = new DirectionalNode();
        }
    }


}
