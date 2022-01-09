using BenchmarkDotNet.Attributes;

namespace DirectionalPathingLayers.Tests.Benchmarks
{
    [ShortRunJob]
    [HtmlExporter]
    //[MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    public class Benchmark_DirectionalNode
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static DirectionalNode simpleNode; //Common node to avoid constructing when not neccesary

        public Benchmark_DirectionalNode()
        {
            simpleNode = new DirectionalNode(0);
        }

        //Test access
        [Benchmark(Description = "DirectionalNode.AddLayer")]
        public void AccessDirections()
        {
            if (Benchmark_DirectionalNode.simpleNode.Forward == 0 &&
                Benchmark_DirectionalNode.simpleNode.Backward == 0 &&
                Benchmark_DirectionalNode.simpleNode.Left == 0 &&
                Benchmark_DirectionalNode.simpleNode.Right == 0 &&
                Benchmark_DirectionalNode.simpleNode.Up == 0 &&
                Benchmark_DirectionalNode.simpleNode.Down == 0)
            {
                dummyInt++;
            }
        }

        [Benchmark(Description = "DirectionalNode.Constructor")]
        public void Constructor()
        {
            DirectionalNode testNode = new DirectionalNode(0);
        }

        [Benchmark(Description = "DirectionalNode.AddSub")]
        public void AddSub()
        {
            Benchmark_DirectionalNode.simpleNode = Benchmark_DirectionalNode.simpleNode + Benchmark_DirectionalNode.simpleNode;
            Benchmark_DirectionalNode.simpleNode = Benchmark_DirectionalNode.simpleNode - Benchmark_DirectionalNode.simpleNode;
            Benchmark_DirectionalNode.simpleNode = Benchmark_DirectionalNode.simpleNode * Benchmark_DirectionalNode.simpleNode;
            Benchmark_DirectionalNode.simpleNode = Benchmark_DirectionalNode.simpleNode & Benchmark_DirectionalNode.simpleNode;
            Benchmark_DirectionalNode.simpleNode = Benchmark_DirectionalNode.simpleNode | Benchmark_DirectionalNode.simpleNode;
            Benchmark_DirectionalNode.simpleNode = Benchmark_DirectionalNode.simpleNode ^ Benchmark_DirectionalNode.simpleNode;
        }

    }


}
