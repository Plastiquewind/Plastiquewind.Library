using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Parsers.Resources;

namespace Plastiquewind.Parsers.Implementations.Errors
{
    public class XlsxMissingHeaderError : IError
    {
        public virtual string Message => ErrorMessages.XlsxMissingHeaderError;
    }
}
