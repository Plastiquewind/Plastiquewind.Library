using Plastiquewind.Base.Abstractions.Errors;

namespace Plastiquewind.Validation.Abstractions.Errors
{
    public abstract class GreaterThanFieldError : IError
    {
        public virtual string Field { get; }
        public abstract string Message { get; }

        public GreaterThanFieldError(string field)
        {
            Field = field;
        }
    }
}
