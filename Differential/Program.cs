using System;
using Differential.Generators;
using Differential.Repositories;

namespace Differential
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();


            var rep = new SequenceRepository();
            var res = rep.GetLastSequence();
        }

        private static void Initialize()
        {
            RandomGenerator.Initialize();
        }


    }
}
