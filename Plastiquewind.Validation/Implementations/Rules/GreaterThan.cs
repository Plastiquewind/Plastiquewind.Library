using System;
using Plastiquewind.Validation.Implementations.Errors;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Base.Implementations;
using Plastiquewind.Validation.Abstractions;
using PommaLabs.Thrower;
using FastMember;

namespace Plastiquewind.Validation.Implementations.Rules
{
    public class GreaterThan<TEntity, TValue> : IValidationRule<TEntity> where TValue : IComparable
    {
        private static readonly TypeAccessor typeAccessor = TypeAccessor.Create(typeof(TEntity));

        public GreaterThan(IEntityField<TEntity> field, TValue value)
        {
            Raise.ArgumentNullException.IfIsNull(field, nameof(field));
            Raise.ArgumentNullException.IfIsNull(value, nameof(value));

            Field = field;
            Value = value;
        }

        public virtual TValue Value { get; }

        public virtual IEntityField<TEntity> Field { get; }

        public IProcessingResult<bool> Check(TEntity entity)
        {
            if (typeAccessor[entity, Field.Name] != null && (Value.CompareTo(typeAccessor[entity, Field.Name]) >= 0))
            {
                return new ProcessingResult<bool>(false, new[] { new GreaterThanFieldError<TValue>(Field.Description, Value) });
            }
            else
            {
                return new ProcessingResult<bool>(true);
            }
        }
    }
}
