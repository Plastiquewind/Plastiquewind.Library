using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Parsers.Resources;

namespace Plastiquewind.Parsers.Implementations.Errors
{
    public class XlsxOpeningError : IError
    {
        public virtual string Message => ErrorMessages.XlsxOpeningError;
    }
}
