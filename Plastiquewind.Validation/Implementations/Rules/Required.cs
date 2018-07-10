using System.Collections;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Base.Implementations;
using Plastiquewind.Base.Helpers;
using Plastiquewind.Validation.Abstractions;
using Plastiquewind.Validation.Implementations.Errors;
using FastMember;
using PommaLabs.Thrower;

namespace Plastiquewind.Validation.Implementations.Rules
{
    public class Required<T> : IValidationRule<T>
    {
        private static readonly TypeAccessor typeAccessor = TypeAccessor.Create(typeof(T));

        public Required(IEntityField<T> field)
        {
            // TODO: Move all arguments checking to attributes and run them in a "decorator".
            Raise.ArgumentNullException.IfIsNull(field, nameof(field));

            Field = field;
        }
        
        public virtual IEntityField<T> Field { get; }

        public virtual IProcessingResult<bool> Check(T entity)
        {
            if (typeAccessor[entity, Field.Name] == null ||
                typeAccessor[entity, Field.Name] is IEnumerable enumerable && enumerable.Count() == 0)
            {
                return new ProcessingResult<bool>(false, new[] { new RequiredFieldError(Field.Description) });
            }
            else
            {
                return new ProcessingResult<bool>(true);
            }
        }
    }
}
