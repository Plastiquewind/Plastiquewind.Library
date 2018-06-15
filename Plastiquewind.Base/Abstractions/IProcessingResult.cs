using System.Collections.Generic;
using Plastiquewind.Base.Abstractions.Errors;

namespace Plastiquewind.Base.Abstractions
{
    public interface IProcessingResult<T>
    {
        T Result { get; }

        IEnumerable<IError> Errors { get; }
    }
}
