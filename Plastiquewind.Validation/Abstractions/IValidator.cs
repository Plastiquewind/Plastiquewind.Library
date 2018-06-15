using Plastiquewind.Base.Abstractions;

namespace Plastiquewind.Validation.Abstractions
{
    public interface IValidator<T>
    {
        IProcessingResult<bool> Validate(T entity);
    }
}
