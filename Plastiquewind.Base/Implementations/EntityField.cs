using System;
using System.Linq;
using System.Reflection;
using Plastiquewind.Base.Abstractions;
using PommaLabs.Thrower;

namespace Plastiquewind.Base.Implementations
{
    public class EntityField<TEntity> : IEntityField<TEntity>
    {
        public EntityField(string name) : this(name, null)
        {            
        }

        public EntityField(string name, string description)
        {
            Raise.ArgumentNullException.IfIsNull(name, nameof(name));

            var property = typeof(TEntity)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.CanWrite && m.Name.Equals(name));

            Raise.ArgumentException.If(property == null, nameof(name));

            Name = name;
            Description = description ?? name;
            Type = property.PropertyType;
        }

        public virtual string Name { get; }
        public virtual string Description { get; }
        public virtual Type Type { get; }
    }
}
