using System;
using Differential.Dto;
using Differential.Generators;

namespace Differential.Accessors
{
    public class Accessor : IAccessor
    {
        public ValueDto GetSequenceValue()
        {
            return RandomGenerator.CreateValue();
        }
    }
}
