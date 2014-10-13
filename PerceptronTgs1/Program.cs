using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JST;

namespace PerceptronTgs1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("nilai α = 1");
            Console.WriteLine("nilai ambang = 0.5");
            Console.WriteLine("nilai target = | 1 | -1 | -1 | 1 | -1 | -1 |");
            
            Perceptron pct = new Perceptron();
            pct.Training();
        }
    }
}
