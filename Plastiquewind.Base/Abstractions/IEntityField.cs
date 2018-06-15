using System;

namespace Plastiquewind.Base.Abstractions
{
    public interface IEntityField<TEntity>
    {
        string Name { get; }

        string Description { get; }

        Type Type { get; }
    }
}
