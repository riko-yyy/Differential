using System;
using System.Collections.Generic;
using Differential.Dto;

namespace Differential.Accessors
{
    public interface IAccessor
    {
        ValueDto GetSequenceValue();
    }
}
