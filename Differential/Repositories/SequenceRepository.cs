using System;
using System.Linq;
using Differential.Context;
using Differential.Models;

namespace Differential.Repositories
{
    public class SequenceRepository : ISequenceRepository
    {
        private DifferentialContext db = new DifferentialContext();

        public Sequence CreateSequence(Sequence sequence)
        {
            throw new NotImplementedException();
        }

        public Sequence GetLastSequence()
        {
            return db.Sequences.Last();
        }
    }
}
