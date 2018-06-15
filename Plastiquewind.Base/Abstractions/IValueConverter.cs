using System;

namespace Plastiquewind.Base.Abstractions
{
    public interface IValueConverter
    {
        bool TryConvert(object rawValue, Type targetType, out object result);
    }
}
