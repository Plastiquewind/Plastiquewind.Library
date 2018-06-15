using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Validation.Resources;

namespace Plastiquewind.Validation.Implementations.Errors
{
    public class RequiredFieldError : IError
    {
        public RequiredFieldError(string field)
        {
            Field = field;
        }

        public string Field { get; }
        public string Message => string.Format(ErrorMessages.RequiredFieldError, Field);
    }
}
