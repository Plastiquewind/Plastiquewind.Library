using Plastiquewind.Base.Abstractions;

namespace Plastiquewind.Validation.Abstractions
{
    public interface IValidationRule<T>
    {
        IProcessingResult<bool> Check(T entity);
    }
}
