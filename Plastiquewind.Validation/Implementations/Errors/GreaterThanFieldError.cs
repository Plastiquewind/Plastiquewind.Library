using Plastiquewind.Validation.Abstractions.Errors;
using Plastiquewind.Validation.Resources;

namespace Plastiquewind.Validation.Implementations.Errors
{
    public class GreaterThanFieldError<T> : GreaterThanFieldError
    {
        public GreaterThanFieldError(string field, T value) : base(field)
        {
            MinValue = value;
        }

        public T MinValue { get; }

        public override string Message => string.Format(ErrorMessages.GreaterThanFieldError, Field, MinValue);
    }
}
