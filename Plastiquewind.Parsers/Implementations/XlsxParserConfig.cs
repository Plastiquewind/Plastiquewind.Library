using FastMember;
using PommaLabs.Thrower;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Base.Implementations;
using Plastiquewind.Parsers.Abstractions;
using Plastiquewind.Parsers.Helpers;

namespace Plastiquewind.Parsers.Implementations
{
    public class XlsxParserConfig<T> : IXlsxParserConfig<T>
    {
        public XlsxParserConfig()
        {
            FieldsMap = new XlsxFieldsMap<T>();
            ValueConverter = new ValueConverter();
            TypeAccessor = TypeAccessor.Create(typeof(T));
        }

        public XlsxParserConfig(IXlsxFieldsMap<T> fieldsMap)
        {
            Raise.ArgumentNullException.IfIsNull(fieldsMap, nameof(fieldsMap));

            FieldsMap = fieldsMap;
            ValueConverter = new ValueConverter();
            TypeAccessor = TypeAccessor.Create(typeof(T));
        }

        public XlsxParserConfig(IXlsxFieldsMap<T> fieldsMap, IValueConverter valueConverter)
        {
            Raise.ArgumentNullException.IfIsNull(fieldsMap, nameof(fieldsMap));
            Raise.ArgumentNullException.IfIsNull(valueConverter, nameof(valueConverter));

            FieldsMap = fieldsMap;
            ValueConverter = valueConverter;
            TypeAccessor = TypeAccessor.Create(typeof(T));
        }

        public virtual int FirstSheet { get; set; } = 0;

        public virtual int LastSheet { get; set; } = int.MaxValue;

        public virtual int FirstRow { get; set; } = XlsxBounds.MinRow;

        public virtual int LastRow { get; set; } = XlsxBounds.MaxRow;

        public virtual string FirstColumn { get; set; } = XlsxBounds.MinColumn;

        public virtual string LastColumn { get; set; } = XlsxBounds.MaxColumn;

        public virtual IXlsxFieldsMap<T> FieldsMap { get; }

        public virtual IValueConverter ValueConverter { get; }

        public virtual TypeAccessor TypeAccessor { get; }
    }
}
