using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Validation.Resources;

namespace Plastiquewind.Validation.Implementations.Errors
{
    public class MaxLengthFieldError : IError
    {
        public MaxLengthFieldError(string field, int length)
        {
            Field = field;
            MaxLength = length;
        }

        public string Field { get; }
        public int MaxLength { get; }
        public string Message => string.Format(ErrorMessages.MaxLengthFieldError, Field, MaxLength);
    }
}
