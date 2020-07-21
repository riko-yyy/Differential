using System;
using Differential.Generators;
using Differential.Models;
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

            var dto = RandomGenerator.CreateValue();
            var diff = dto.Value - res?.Value ?? 0.0;

            res = rep.CreateSequence(new Sequence() { CreatedOn = dto.CreateOn, Value = dto.Value, Differential = diff });

        }

        private static void Initialize()
        {
            RandomGenerator.Initialize();
        }


    }
}
