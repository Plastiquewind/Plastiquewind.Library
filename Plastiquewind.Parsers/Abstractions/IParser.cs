using System.Collections.Generic;
using System.IO;
using Plastiquewind.Base.Abstractions;

namespace Plastiquewind.Parsers.Abstractions
{
    public interface IParser<T>
    {
        IProcessingResult<IEnumerable<T>> Parse(Stream stream);
    }
}
