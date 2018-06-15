using System;
using System.Globalization;
using Plastiquewind.Base.Abstractions;

namespace Plastiquewind.Base.Implementations
{
    public class ValueConverter : IValueConverter
    {
        //todo extract internal methods. Make common sense validation.
        public bool TryConvert(object rawValue, Type targetType, out object result)
        {
            result = null;
            bool succ;
            switch (targetType)
            {
                case var type when type == typeof(decimal):
                    (succ, result) = TryParseDecimal();
                    return succ;
                case var type when type == typeof(decimal?):
                    (succ, result) = TryParseDecimalNullable();
                    return succ;
                case var type when type == typeof(DateTime):
                    (succ, result) = TryParseDateTime();
                    return succ;
                case var type when type == typeof(DateTime?):
                    (succ, result) = TryParseDateTimeNullable();
                    return succ;
                case var type when type == typeof(double):
                    (succ, result) = TryParseDouble();
                    return succ;
                case var type when type == typeof(int):
                    (succ, result) = TryParseInt();
                    return succ;
                case var type when type == typeof(int?):
                    (succ, result) = TryParseIntNullable();
                    return succ;
                case var type when type == typeof(long):
                    (succ, result) = TryParseLong();
                    return succ;
                case var type when type == typeof(long?):
                    (succ, result) = TryParseLongNullable();
                    return succ;
                case var type when type == typeof(string):
                    result = rawValue?.ToString()?.Trim();
                    return true;
                default:
                    return false;
            }

            (bool succ, decimal result) TryParseDecimal()
            {
                if (rawValue is string rawString)
                {
                    decimal decResult;
                    rawString = rawString.Replace(",", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                    rawString = rawString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                    if (decimal.TryParse(rawString, out decResult))
                    {
                        return (true, decResult);
                    }
                    if (decimal.TryParse(rawString, NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out decResult))
                    {
                        return (true, decResult);
                    }
                    if (decimal.TryParse(rawString, NumberStyles.Any, CultureInfo.CurrentCulture, out decResult))
                    {
                        return (true, decResult);
                    }
                }

                return (false, default(decimal));
            }

            (bool succ, decimal? result) TryParseDecimalNullable()
            {
                if (rawValue == null)
                    return (true, null);

                if (rawValue is string rawString)
                {
                    if (string.IsNullOrEmpty(rawString))
                        return (true, null);

                    return TryParseDecimal();
                }

                return (false, default(decimal));
            }

            (bool succ, double result) TryParseDouble()
            {
                if (rawValue is string rawString)
                {
                    double doubResult;
                    rawString = rawString.Replace(",", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                    rawString = rawString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                    if (double.TryParse(rawString, out doubResult))
                    {
                        return (true, doubResult);
                    }
                }

                return (false, default(double));
            }

            (bool succ, int result) TryParseInt()
            {
                if (rawValue is string rawString)
                {
                    int intResult;
                    if (int.TryParse(rawString, out intResult))
                    {
                        return (true, intResult);
                    }
                }

                return (false, default(int));
            }

            (bool succ, int? result) TryParseIntNullable()
            {
                if (rawValue == null)
                    return (true, null);

                if (rawValue is string rawString)
                {
                    if (string.IsNullOrEmpty(rawString))
                        return (true, null);

                    return TryParseInt();
                }

                return (false, default(int));
            }

            (bool succ, long? result) TryParseLongNullable()
            {
                if (rawValue == null)
                {
                    return (true, null);
                }

                if (rawValue is string rawString)
                {
                    if (string.IsNullOrEmpty(rawString))
                    {
                        return (true, null);
                    }

                    return TryParseLong();
                }

                return (false, default(long));
            }

            (bool succ, long result) TryParseLong()
            {
                if (rawValue is string rawString)
                {
                    long longResult;
                    if (long.TryParse(rawString, out longResult))
                    {
                        return (true, longResult);
                    }
                }

                return (false, default(int));
            }

            (bool succ, DateTime result) TryParseDateTime()
            {
                if (rawValue is string rawString)
                {
                    
                    if (DateTime.TryParse(rawString, out DateTime dateTimeResult))
                    {
                        return (true, dateTimeResult);
                    }
                    if (DateTime.TryParse(rawString, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTimeResult))
                    {
                        return (true, dateTimeResult);
                    }
                    if (DateTime.TryParse(rawString, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeResult))
                    {
                        return (true, dateTimeResult);
                    }
                    var ruCulture = new CultureInfo("ru-RU");
                    if (DateTime.TryParse(rawString, ruCulture, DateTimeStyles.None, out dateTimeResult))
                    {
                        return (true, dateTimeResult);
                    }
                    if (DateTime.TryParseExact(rawString, "YYYY-MM-DD", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dateTimeResult))
                    {
                        return (true, dateTimeResult);
                    }
                    if (Double.TryParse(rawString, out var tmpDouble))
                    {
                        try
                        {
                            var parsedFromDouble = DateTime.FromOADate(tmpDouble);
                            if (parsedFromDouble > new DateTime(1990, 1, 1) && parsedFromDouble < new DateTime(2100, 1, 1))
                            {
                                return (true, parsedFromDouble);
                            }
                        }
                        catch
                        {

                        }

                    }

                }

                return (false, default(DateTime));
            }

            (bool succ, DateTime? result) TryParseDateTimeNullable()
            {
                if (rawValue == null)
                    return (true, null);

                if (rawValue is string rawString)
                {
                    if (string.IsNullOrEmpty(rawString))
                        return (true, null);

                    if (TryParseDateTime().succ)
                    {
                        return (true, TryParseDateTime().result);
                    }
                }

                return (true, null);
            }
        }
    }
}