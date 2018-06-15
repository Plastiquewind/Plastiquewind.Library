using Plastiquewind.Base.Abstractions;
using FastMember;

namespace Plastiquewind.Parsers.Abstractions
{
    public interface IXlsxParserConfig<T>
    {
        int FirstSheet { get; }

        int LastSheet { get; }

        int FirstRow { get; }

        int LastRow { get; }

        string FirstColumn { get; }

        string LastColumn { get; }

        IXlsxFieldsMap<T> FieldsMap { get; }

        IValueConverter ValueConverter { get; }

        TypeAccessor TypeAccessor { get; }
    }
}
