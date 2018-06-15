using System.Collections;
using System.Collections.Generic;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Parsers.Abstractions;

namespace Plastiquewind.Parsers.Implementations
{
    public class XlsxFieldsMap<TEntity> : IXlsxFieldsMap<TEntity>
    {
        private readonly Dictionary<string, IEntityField<TEntity>> map;

        public XlsxFieldsMap()
        {
            map = new Dictionary<string, IEntityField<TEntity>>();
        }

        public virtual IEnumerable<IEntityField<TEntity>> Fields => map.Values;

        public virtual IEnumerable<string> Columns => map.Keys;

        public virtual IXlsxFieldsMap<TEntity> Add(string column, IEntityField<TEntity> field)
        {
            if (Has(column))
            {
                map[column] = field;
            }
            else
            {
                map.Add(column, field);
            }

            return this;
        }

        public virtual IXlsxFieldsMap<TEntity> Clear()
        {
            map.Clear();

            return this;
        }

        public virtual IEntityField<TEntity> this[string column]
        {
            get
            {
                return map[column];
            }
        }

        public virtual IEnumerator<KeyValuePair<string, IEntityField<TEntity>>> GetEnumerator()
        {
            return map.GetEnumerator();
        }

        public virtual bool Has(string column)
        {
            return map.ContainsKey(column);
        }

        public virtual IXlsxFieldsMap<TEntity> Remove(string column)
        {
            map.Remove(column);

            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
