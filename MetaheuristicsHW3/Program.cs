using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsHW3
{
    class Program
    {
        static void Main(string[] args)
        {
            DifferentialEvolution de =
                new DifferentialEvolution(100, 30, -100, 100,
                    TestFunction.F1, 300000, 0.5, 0.5);
            double[] solution = de.Run();
            Console.WriteLine("{0}", TestFunction.F1(solution));
            HelperFunction.Print(solution);
        }
    }
}
