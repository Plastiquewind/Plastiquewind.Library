using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Parsers.Resources;

namespace Plastiquewind.Parsers.Implementations.Errors
{
    public class XlsxProtectedViewError : IError
    {
        public virtual string Message => ErrorMessages.XlsxProtectedViewError;
    }
}
