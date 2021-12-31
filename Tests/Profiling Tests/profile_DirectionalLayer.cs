using NUnit.Framework;

namespace DirectionalPathingLayers.Tests.Profiling_Tests
{
    class profile_DirectionalLayer
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static DirectionalLayer simpleLayer; //Common layer to avoid constructing when not neccesary

        [Test]
        public void test_Constructor()
        {
            dummyInt = 0;
            profile_Benchmark.Benchmark(this.profile_Constructor, 100000);
        }
        public void profile_Constructor()
        {
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
        }


        [Test]
        public void test_Set()
        {
            dummyInt = 0;
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
            profile_Benchmark.Benchmark(this.profile_Set, 100000);
        }
        public void profile_Set()
        {
            profile_DirectionalLayer.simpleLayer.Set(1);
        }


        [Test]
        public void test_And()
        {
            dummyInt = 0;
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
            profile_Benchmark.Benchmark(this.profile_And, 100000);
        }
        public void profile_And()
        {
            profile_DirectionalLayer.simpleLayer = profile_DirectionalLayer.simpleLayer & profile_DirectionalLayer.simpleLayer;
        }

        [Test]
        public void test_XOR()
        {
            dummyInt = 0;
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
            profile_Benchmark.Benchmark(this.profile_XOR, 100000);
        }
        public void profile_XOR()
        {
            profile_DirectionalLayer.simpleLayer = profile_DirectionalLayer.simpleLayer ^ profile_DirectionalLayer.simpleLayer;
        }

        [Test]
        public void test_OR()
        {
            dummyInt = 0;
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
            profile_Benchmark.Benchmark(this.profile_OR, 100000);
        }
        public void profile_OR()
        {
            profile_DirectionalLayer.simpleLayer = profile_DirectionalLayer.simpleLayer | profile_DirectionalLayer.simpleLayer;
        }

        [Test]
        public void test_MULT()
        {
            dummyInt = 0;
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
            profile_Benchmark.Benchmark(this.profile_MULT, 100000);
        }
        public void profile_MULT()
        {
            profile_DirectionalLayer.simpleLayer = profile_DirectionalLayer.simpleLayer * profile_DirectionalLayer.simpleLayer;
        }

        [Test]
        public void test_INVERT()
        {
            dummyInt = 0;
            profile_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);
            profile_Benchmark.Benchmark(this.profile_INVERT, 100000);
        }
        public void profile_INVERT()
        {
            profile_DirectionalLayer.simpleLayer = ~profile_DirectionalLayer.simpleLayer;
        }
    }


}
