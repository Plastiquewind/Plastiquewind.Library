using System.Collections.Generic;
using Plastiquewind.Base.Abstractions;

namespace Plastiquewind.Parsers.Abstractions
{
    public interface IXlsxFieldsMap<TEntity> : IEnumerable<KeyValuePair<string, IEntityField<TEntity>>>
    {
        IEnumerable<IEntityField<TEntity>> Fields { get; }

        IEnumerable<string> Columns { get; }

        IEntityField<TEntity> this[string column] { get; }

        bool Has(string column);

        IXlsxFieldsMap<TEntity> Add(string column, IEntityField<TEntity> field);

        IXlsxFieldsMap<TEntity> Remove(string column);

        IXlsxFieldsMap<TEntity> Clear();
    }
}
