using System;
using Differential.Models;

namespace Differential.Repositories
{
    public interface ISequenceRepository
    {
        Sequence GetLastSequence();

        Sequence CreateSequence(Sequence sequence);
    }
}
