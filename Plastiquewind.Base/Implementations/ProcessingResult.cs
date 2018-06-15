using System.Collections.Generic;
using System.Linq;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Base.Abstractions.Errors;


namespace Plastiquewind.Base.Implementations
{
    public class ProcessingResult<T> : IProcessingResult<T>
    {
        public ProcessingResult(T result, IEnumerable<IError> errors = null)
        {
            Result = result;
            Errors = errors ?? Enumerable.Empty<IError>();
        }

        public virtual T Result { get; }

        public virtual IEnumerable<IError> Errors { get; }
    }
}
