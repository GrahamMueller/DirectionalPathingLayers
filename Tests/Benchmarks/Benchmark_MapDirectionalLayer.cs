using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace DirectionalPathingLayers.Tests.Benchmarks
{
    [ShortRunJob]
    [HtmlExporter]
    //[MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    [Orderer(SummaryOrderPolicy.Method)]
    public class Benchmark_MapDirectionalLayer
    {
        static int dummyInt = 0; //By existing and being used in tests, we prevent optimizations removing test code.

        static MapDirectionalLayer mapLayer; //Common layer to avoid constructing when not neccesary
        static DirectionalLayer simpleLayer;
        static DirectionalNode simpleNode;

        public Benchmark_MapDirectionalLayer()
        {
            simpleNode = new DirectionalNode(0);
            simpleLayer = new DirectionalLayer(3, 3);
            mapLayer = new MapDirectionalLayer(10, 10, simpleNode);
        }

        [Benchmark(Description = "MapDirectionalLayer.Constructor")]
        public void Constructor()
        {
            Benchmark_MapDirectionalLayer.mapLayer = new MapDirectionalLayer(10, 10, simpleNode);
        }

        [Benchmark(Description = "MapDirectionalLayer.AddLayer")]
        public void AddLayer()
        {
            Benchmark_MapDirectionalLayer.mapLayer.AddDirectionalLayerAtPoint(simpleLayer, 0, 0);
        }

    }



}
