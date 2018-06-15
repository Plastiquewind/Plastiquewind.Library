using System.Linq;
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
    public class MaxLength<TEntity> : IValidationRule<TEntity>
    {
        private static readonly TypeAccessor typeAccessor = TypeAccessor.Create(typeof(TEntity));

        public MaxLength(IEntityField<TEntity> field, int length)
        {
            Raise.ArgumentNullException.IfIsNull(field, nameof(field));
            // TODO: Make obvious for the end user that the field has to be IEnumerable.
            Raise.ArgumentException.IfNot(field.Type.GetInterfaces().Contains(typeof(IEnumerable)), nameof(field));

            Field = field;
            Length = length;
        }

        public virtual IEntityField<TEntity> Field { get; }
        public virtual int Length { get; }

        public virtual IProcessingResult<bool> Check(TEntity entity)
        {
            if (typeAccessor[entity, Field.Name] != null && ((IEnumerable)typeAccessor[entity, Field.Name]).Count() > Length)
            {
                return new ProcessingResult<bool>(false, new[] { new MaxLengthFieldError(Field.Description, Length) });
            }
            else
            {
                return new ProcessingResult<bool>(true);
            }
        }
    }
}
