using NUnit.Framework;

namespace DirectionalPathingLayers.Tests.Profiling_Tests
{
    class profile_DirectionalNode
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static DirectionalNode simpleNode; //Common node to avoid constructing when not neccesary

        //Test speed of direction access
        [Test]
        public void test_Directions()
        {
            simpleNode = new DirectionalNode(0);
            dummyInt = 0;
            profile_Benchmark.Benchmark(this.profile_Directions, 100000000);
        }
        public void profile_Directions()
        {
            if (profile_DirectionalNode.simpleNode.Forward == 0 &&
                profile_DirectionalNode.simpleNode.Backward == 0 &&
                profile_DirectionalNode.simpleNode.Left == 0 &&
                profile_DirectionalNode.simpleNode.Right == 0 &&
                profile_DirectionalNode.simpleNode.Up == 0 &&
                profile_DirectionalNode.simpleNode.Down == 0)
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
            profile_DirectionalNode.simpleNode = new DirectionalNode(0);
        }


        [Test]
        public void test_AddSub()
        {
            dummyInt = 0;
            profile_DirectionalNode.simpleNode = new DirectionalNode(0);
            profile_Benchmark.Benchmark(this.profile_AddSub, 10000000);
        }
        public void profile_AddSub()
        {
            profile_DirectionalNode.simpleNode = profile_DirectionalNode.simpleNode + profile_DirectionalNode.simpleNode;
            profile_DirectionalNode.simpleNode = profile_DirectionalNode.simpleNode - profile_DirectionalNode.simpleNode;
            profile_DirectionalNode.simpleNode = profile_DirectionalNode.simpleNode * profile_DirectionalNode.simpleNode;
            profile_DirectionalNode.simpleNode = profile_DirectionalNode.simpleNode & profile_DirectionalNode.simpleNode;
            profile_DirectionalNode.simpleNode = profile_DirectionalNode.simpleNode | profile_DirectionalNode.simpleNode;
            profile_DirectionalNode.simpleNode = profile_DirectionalNode.simpleNode ^ profile_DirectionalNode.simpleNode;
        }

    }


}
