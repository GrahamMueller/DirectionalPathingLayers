using BenchmarkDotNet.Attributes;

namespace DirectionalPathingLayers.Tests.Benchmarks
{
    [ShortRunJob]
    [HtmlExporter]
   // [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    public class Benchmark_DirectionalLayer
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static DirectionalLayer simpleLayer; //Common layer to avoid constructing when not neccesary

        public Benchmark_DirectionalLayer()
        {
            Benchmark_DirectionalLayer.simpleLayer = new DirectionalLayer(10, 10);

        }

        [Benchmark(Description = "DirectionalLayer.Constructor")]
        public void Constructor()
        {
            DirectionalLayer testLayer = new DirectionalLayer(10, 10);
        }


        [Benchmark(Description = "DirectionalLayer.Set")]
        public void Set()
        {
            Benchmark_DirectionalLayer.simpleLayer.Set(0);
        }

        [Benchmark(Description = "DirectionalLayer.And")]
        public void And()
        {
            Benchmark_DirectionalLayer.simpleLayer = Benchmark_DirectionalLayer.simpleLayer & Benchmark_DirectionalLayer.simpleLayer;
        }

        [Benchmark(Description = "DirectionalLayer.XOR")]
        public void XOR()
        {
            Benchmark_DirectionalLayer.simpleLayer = Benchmark_DirectionalLayer.simpleLayer ^ Benchmark_DirectionalLayer.simpleLayer;
        }

        [Benchmark(Description = "DirectionalLayer.OR")]
        public void OR()
        {
            Benchmark_DirectionalLayer.simpleLayer = Benchmark_DirectionalLayer.simpleLayer | Benchmark_DirectionalLayer.simpleLayer;
        }

        [Benchmark(Description = "DirectionalLayer.MULT")]
        public void MULT()
        {
            Benchmark_DirectionalLayer.simpleLayer = Benchmark_DirectionalLayer.simpleLayer * Benchmark_DirectionalLayer.simpleLayer;
        }

        [Benchmark(Description = "DirectionalLayer.INVERT")]
        public void INVERT()
        {
            Benchmark_DirectionalLayer.simpleLayer = ~Benchmark_DirectionalLayer.simpleLayer;
        }


    }


}
