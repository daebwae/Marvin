using Distriubtions;
using Marvin.Sampling.Metropolis;
using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using static Toolkit.Misc;
namespace Marvin.Votes
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 1d;
            var b = 3d;
            var n = 420000;
            var beta = new Beta(a, b);
            Func<List<double>, double> probability =
                xVector => beta.CalcPdf(xVector[0]);

            var parameters = new ParameterRoot();
            parameters.Add(new Parameter(0.5, 0.19));

            var metropolis = new Metropolis(probability, parameters, new Toolkit.RandomNumberGenerator());
            var data = metropolis.Sample(n);
            data.Labels.Add("x");
            data.ExportCsv("d:\\datum\\datum.csv");

            var mean = data.Sum(x => x[0]) / n;
            var closedFormMean = a / (a + b);

            Console.WriteLine($"Sampled Mean: {mean}");
            Console.WriteLine($"Closed form Mean: {closedFormMean}");
            Console.ReadKey(); 

            /*
            var n = 40;
            var p = 0.5;
            Console.WriteLine(Gamma(3.7));


    
            for (double i=0.0; i<=1; i+=0.01)
            {
                x.Add(i); 
            }

            var binomial1 = new Beta(0.5, 0.5);
            var y = x.Select(current => binomial1.CalcCdf(current)); 
           

            var envPath = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", envPath + System.IO.Path.PathSeparator + System.IO.Path.Combine(@"C:\Program Files\R\R-3.2.3\bin\x64"));

            // Create our static engine instance
            var engine = REngine.CreateInstance("RDotNet");

            // From v1.5, REngine requires explicit initialization.
            // You can set some parameters.
            engine.Initialize();

            // .NET Framework array to R vector.
            var rX = engine.CreateNumericVector(x.Select( cur => (double) cur).ToArray());
            engine.SetSymbol("x", rX);

            var rY = engine.CreateNumericVector(y.ToArray());
            engine.SetSymbol("y", rY);
            // Direct parsing from R script.
            engine.Evaluate("require('ggplot2')");
            engine.Evaluate("library('ggplot2')");
            engine.Evaluate("data <- data.frame(x=x, y=y)");
            engine.Evaluate("graph <- ggplot(data, aes(x=x, y=y,colour='some')) + geom_point(alpha=0.009)");
            engine.Evaluate("print(graph)");
            engine.Evaluate("print(graph)");


    */
            // you should always dispose of the REngine properly.
            // After disposing of the engine, you cannot reinitialize nor reuse it
            //Console.ReadKey();
            //engine.Dispose();
        }
    }
}
