using System.Collections.Generic;

namespace Plastiquewind.Validation.Abstractions
{
    public interface IValidationRulesFactory<T>
    {
        IEnumerable<IValidationRule<T>> Create();
    }
}
