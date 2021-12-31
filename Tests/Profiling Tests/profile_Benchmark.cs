using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace DirectionalPathingLayers.Tests.Profiling_Tests
{
    public static class profile_Benchmark
    {
        static bool writeToFile = true;
        static string outputFile = "benchmarks.log";
        static string outputFile_run;
        static profile_Benchmark()
        {
            //Setup files once per entire test run.
            if (writeToFile)
            {
                //Clear old file's contents.
                System.IO.File.WriteAllText(outputFile, string.Empty);

                //Create unique test-run file.
                string timeNow = DateTime.Now.ToString("yyyyMMddTHHmmss");
                outputFile_run = timeNow + "_" + outputFile;
            }
        }

        //https://stackoverflow.com/questions/1622440/benchmarking-method-calls-in-c-sharp
        public static void Benchmark(Action act, int iterations)
        {

            //JIT - run it a couple times before measuring.
            WarmupMethod(act, iterations);

            //Ensure garbage collection has completed.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            //Perform benchmark
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                act.Invoke();
            }
            sw.Stop();


            //Adjust per-run time scaling.
            float timePerRun = (1.0f * sw.ElapsedMilliseconds) / iterations;
            string timePerRunUnit = "mS";
            if (timePerRun < 0.00001) //nS
            {
                timePerRun *= 1000 * 1000;
                timePerRunUnit = "nS";
            }
            else if (timePerRun < 0.01) //uS
            {
                timePerRun *= 1000;
                timePerRunUnit = "uS";
            }

            //String output
            string totalTime = sw.ElapsedMilliseconds.ToString();
            string testName = act.Method.GetType().Namespace;
            string writeLine = $"{act.Method.Name} | #{iterations}, {totalTime} mS | {timePerRun} {timePerRunUnit}";

            Console.WriteLine($"act.Method.GetBaseDefinition().Name {act.Method.GetBaseDefinition().Name}");
            Console.WriteLine($"act.Method.GetType().Name {act.Method.GetType().Name}");
            Console.WriteLine($"act.Method.GetType().FullName {act.Method.GetType().FullName}");
            Console.WriteLine($"act.Method.GetType().Namespace {act.Method.GetType().Namespace}");


            Console.WriteLine(writeLine);
            if (writeToFile)
            {
                WriteLineToDebugFiles(writeLine);
            }

        }

        private static void WarmupMethod(Action act, int iterations)
        {
            
            for (int i = 0; i < (iterations * 0.25); ++i)
            {
                act.Invoke(); // run once outside of loop to avoid initialization costs
            }
        }

        static void WriteLineToDebugFiles(string outputLine)
        {
            using (StreamWriter streamWriter = File.AppendText(outputFile))
            {
                streamWriter.WriteLine(outputLine);

            }

            using (StreamWriter streamWriter = File.AppendText(outputFile_run))
            {
                streamWriter.WriteLine(outputLine);

            }
        }
    }
}
