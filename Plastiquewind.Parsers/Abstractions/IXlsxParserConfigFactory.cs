using System.IO;
using Plastiquewind.Base.Abstractions;

namespace Plastiquewind.Parsers.Abstractions
{
    public interface IXlsxParserConfigFactory<T>
    {
        IProcessingResult<IXlsxParserConfig<T>> Create(Stream stream);
    }
}
