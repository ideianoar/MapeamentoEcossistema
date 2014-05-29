using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MapeamentoEcossistema.WebUI.Code
{
    /// <summary>
    /// Provides methods to deal with DateTime and TimeSpan objects concerning Local and UTC representations.
    /// </summary>
    public class DateTimeZoneHelper
    {
        // Fields.
        /// <summary>
        /// The default time zone to use when converting to and from UTC.
        /// </summary>
        private static TimeZoneInfo _defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        // Properties.
        /// <summary>
        /// Gets or sets the default time zone to be used when converting DateTime and TimeSpan values to and from UTC.
        /// </summary>
        public static TimeZoneInfo DefaultTimeZone
        {
            get
            {
                return _defaultTimeZone;
            }
            set
            {
                _defaultTimeZone = value;
            }
        }

        // DateTime/TimeSpan public methods.
        /// <summary>
        /// Converts a DateTime object from UTC to its local representation according to the default time zone.
        /// </summary>
        /// <param name="dateTime">
        /// The UTC DateTime object.
        /// </param>
        /// <returns>
        /// The local representation of the DateTime object.
        /// </returns>
        public static DateTime ConvertFromUtc(DateTime dateTime)
        {
            return ConvertDateTime(dateTime, DateTimeKind.Local, _defaultTimeZone);
        }
        /// <summary>
        /// Converts a DateTime object from UTC to its local representation.
        /// </summary>
        /// <param name="dateTime">
        /// The UTC DateTime object.
        /// </param>
        /// <param name="timeZone">
        /// The target time zone.
        /// </param>
        /// <returns>
        /// The local representation of the DateTime object.
        /// </returns>
        public static DateTime ConvertFromUtc(DateTime dateTime, TimeZoneInfo timeZone)
        {
            return ConvertDateTime(dateTime, DateTimeKind.Local, timeZone);
        }
        /// <summary>
        /// Converts a TimeSpan object from UTC to its local representation.
        /// </summary>
        /// <param name="timeSpan">
        /// The UTC TimeSpan object.
        /// </param>
        /// <returns>
        /// The local representation of the TimeSpan object.
        /// </returns>
        public static TimeSpan ConvertFromUtc(TimeSpan timeSpan)
        {
            return ConvertFromUtc(timeSpan, _defaultTimeZone);
        }
        /// <summary>
        /// Converts a TimeSpan object from UTC to its local representation.
        /// </summary>
        /// <param name="timeSpan">
        /// The UTC TimeSpan object.
        /// </param>
        /// <param name="timeZone">
        /// The target time zone.
        /// </param>
        /// <returns>
        /// The local representation of the TimeSpan object.
        /// </returns>
        public static TimeSpan ConvertFromUtc(TimeSpan timeSpan, TimeZoneInfo timeZone)
        {
            var dateTime = ConvertFromUtc(new DateTime(timeSpan.Ticks, DateTimeKind.Utc), timeZone);
            return new TimeSpan(dateTime.Ticks);
        }
        /// <summary>
        /// Converts a DateTime object from local time to its UTC representation according to the default time zone.
        /// </summary>
        /// <param name="dateTime">
        /// The local DateTime object.
        /// </param>
        /// <returns>
        /// The local representation of the DateTime object.
        /// </returns>
        public static DateTime ConvertToUtc(DateTime dateTime)
        {
            return ConvertDateTime(dateTime, DateTimeKind.Utc, _defaultTimeZone);
        }
        /// <summary>
        /// Converts a DateTime object from local time to its UTC representation.
        /// </summary>
        /// <param name="dateTime">
        /// The local DateTime object.
        /// </param>
        /// <param name="timeZone">
        /// The source time zone.
        /// </param>
        /// <returns>
        /// The local representation of the DateTime object.
        /// </returns>
        public static DateTime ConvertToUtc(DateTime dateTime, TimeZoneInfo timeZone)
        {
            return ConvertDateTime(dateTime, DateTimeKind.Utc, timeZone);
        }
        /// <summary>
        /// Converts a TimeSpan object from local time to its UTC representation according to the default time zone.
        /// </summary>
        /// <param name="timeSpan">
        /// The local TimeSpan object.
        /// </param>
        /// <returns>
        /// The local representation of the TimeSpan object.
        /// </returns>
        public static TimeSpan ConvertToUtc(TimeSpan timeSpan)
        {
            return ConvertToUtc(timeSpan, _defaultTimeZone);
        }
        /// <summary>
        /// Converts a TimeSpan object from local time to its UTC representation.
        /// </summary>
        /// <param name="timeSpan">
        /// The local TimeSpan object.
        /// </param>
        /// <param name="timeZone">
        /// The source time zone.
        /// </param>
        /// <returns>
        /// The local representation of the TimeSpan object.
        /// </returns>
        public static TimeSpan ConvertToUtc(TimeSpan timeSpan, TimeZoneInfo timeZone)
        {
            var dateTime = ConvertToUtc(new DateTime(timeSpan.Ticks, DateTimeKind.Local), timeZone);
            return new TimeSpan(dateTime.Ticks);
        }

        // Complex objects public methods.
        /// <summary>
        /// Converts all DateTime and TimeSpan properties of a complex type instance to its local representation according to the default time zone.
        /// </summary>
        /// <param name="target">
        /// The target object.
        /// </param>
        /// <param name="recursive">
        /// Specifies whether properties of a complex type should have its nested properties also converted.
        /// </param>
        public static void ConvertFromUtc(object target, bool recursive)
        {
            ConvertFromUtc(target, recursive, _defaultTimeZone);
        }
        /// <summary>
        /// Converts all DateTime and TimeSpan properties of a complex type instance to its local representation.
        /// </summary>
        /// <param name="target">
        /// The target object.
        /// </param>
        /// <param name="timeZone">
        /// The target time zone.
        /// </param>
        /// <param name="recursive">
        /// Specifies whether properties of a complex type should have its nested properties also converted.
        /// </param>
        public static void ConvertFromUtc(object target, bool recursive, TimeZoneInfo timeZone)
        {
            ConvertDateTimeProperties(target, recursive, DateTimeKind.Local, timeZone);
        }
        /// <summary>
        /// Converts all DateTime and TimeSpan properties of a complex type instance to its UTC representation according to the default time zone.
        /// </summary>
        /// <param name="target">
        /// The target object.
        /// </param>
        /// <param name="recursive">
        /// Specifies whether properties of a complex type should have its nested properties also converted.
        /// </param>
        public static void ConvertToUtc(object target, bool recursive)
        {
            ConvertToUtc(target, recursive, _defaultTimeZone);
        }
        /// <summary>
        /// Converts all DateTime and TimeSpan properties of a complex type instance to its local representation.
        /// </summary>
        /// <param name="target">
        /// The target object.
        /// </param>
        /// <param name="timeZone">
        /// The source time zone.
        /// </param>
        /// <param name="recursive">
        /// Specifies whether properties of a complex type should have its nested properties also converted.
        /// </param>
        public static void ConvertToUtc(object target, bool recursive, TimeZoneInfo timeZone)
        {
            ConvertDateTimeProperties(target, recursive, DateTimeKind.Utc, timeZone);
        }

        // Private methods.
        /// <summary>
        /// Converts a DateTime object to its local or UTC representation.
        /// </summary>
        /// <param name="dateTime">
        /// The DateTime object.
        /// </param>
        /// <param name="targetKind">
        /// The target representation. Should be either Local or Utc.
        /// </param>
        /// <param name="timeZone">
        /// The source or target time zone, depending on the target kind.
        /// </param>
        /// <returns>
        /// The local or UTC representation of the DateTime object.
        /// </returns>
        private static DateTime ConvertDateTime(DateTime dateTime, DateTimeKind targetKind, TimeZoneInfo timeZone)
        {
            var converted = dateTime;
            dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);

            switch (targetKind)
            {
                case DateTimeKind.Local:
                    converted = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
                    break;

                case DateTimeKind.Utc:
                    converted = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
                    break;

                case DateTimeKind.Unspecified:
                    throw new ArgumentException("The 'targetKind' parameter cannot be DateTimeKind.Unspecified.", "targetKind");
            }

            return converted;
        }
        /// <summary>
        /// Identifies and converts DateTime and TimeSpan properties to their local or UTC representations.
        /// </summary>
        /// <param name="target">
        /// The target object.
        /// </param>
        /// <param name="recursive">
        /// Specifies whether properties of a complex type should have its nested properties also converted.
        /// </param>
        /// <param name="targetKind">
        /// The target representation. Should be either Local or Utc.
        /// </param>
        /// <param name="timeZone">
        /// The source or target time zone, depending on the target kind.
        /// </param>
        private static void ConvertDateTimeProperties(object target, bool recursive, DateTimeKind targetKind, TimeZoneInfo timeZone)
        {
            if (target == null)
                return;

            var properties = target
                .GetType()
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var p in properties)
            {
                var value = p.GetValue(target, null);

                if (value == null)
                    continue;

                object converted;

                // Tries to convert the value.
                if (TryConvertDateTimeValue(value, targetKind, timeZone, out converted))
                {
                    p.SetValue(target, converted);
                }

                // Tries to convert the value as an enumerable of DateTime or TimeSpan objects.
                else if (TryConvertEnumerable(value, targetKind, timeZone, out converted))
                {
                    p.SetValue(target, converted);
                }

                // Recursively converts DateTime and TimeSpan properties of complex types.
                else if (recursive && !p.PropertyType.IsValueType && p.PropertyType != typeof(string))
                {
                    ConvertDateTimeProperties(value, true, targetKind, timeZone);
                }
            }
        }
        /// <summary>
        /// Returns a value indicating whether the type represents a DateTime, Nullable&lt;DateTime&gt; or a collection of DateTime objects.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// true if the type represents a DateTime, Nullable&lt;DateTime&gt; or a collection of DateTime objects; false, otherwise.
        /// </returns>
        private static bool IsDateTime(Type type)
        {
            return type == typeof(DateTime) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(type) == typeof(DateTime));
        }
        /// <summary>
        /// Returns a value indicating whether the type represents a TimeSpan, Nullable&lt;TimeSpan&gt; or a collection of TimeSpan objects.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// true if the type represents a TimeSpan, Nullable&lt;TimeSpan&gt; or a collection of TimeSpan objects; false, otherwise.
        /// </returns>
        private static bool IsTimeSpan(Type type)
        {
            return type == typeof(TimeSpan) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(type) == typeof(TimeSpan));
        }
        /// <summary>
        /// Attempts to perform a conversion of the input value if it is a DateTime, TimeSpan, Nullable&lt;DateTime&gt; or Nullable&lt;TimeSpan&gt; object.
        /// </summary>
        /// <param name="value">
        /// The input value.
        /// </param>
        /// <param name="targetKind">
        /// The target DateTimeKind. Should be either Local or Utc.
        /// </param>
        /// <param name="timeZone">
        /// The target or source time zone, depending on the target kind.
        /// </param>
        /// <param name="converted">
        /// The converted value.
        /// </param>
        /// <returns>
        /// true if the converstion attempt happened and was successful; false, otherwise.
        /// </returns>
        private static bool TryConvertDateTimeValue(object value, DateTimeKind targetKind, TimeZoneInfo timeZone, out object converted)
        {
            var type = value.GetType();
            converted = null;

            if (IsDateTime(type))
            {
                converted = targetKind == DateTimeKind.Local
                    ? ConvertFromUtc((DateTime)value, timeZone)
                    : ConvertToUtc((DateTime)value, timeZone);
                return true;
            }

            else if (IsTimeSpan(type))
            {
                converted = targetKind == DateTimeKind.Local
                    ? ConvertFromUtc((TimeSpan)value, timeZone)
                    : ConvertToUtc((TimeSpan)value, timeZone);
                return true;
            }

            return false;
        }
        /// <summary>
        /// Attempts to perform a conversion of the input value if it is an enumerable of DateTime, TimeSpan, Nullable&lt;DateTime&gt; or Nullable&lt;TimeSpan&gt; objects.
        /// </summary>
        /// <param name="value">
        /// The input value.
        /// </param>
        /// <param name="targetKind">
        /// The target DateTimeKind. Should be either Local or Utc.
        /// </param>
        /// <param name="timeZone">
        /// The target or source time zone, depending on the target kind.
        /// </param>
        /// <param name="converted">
        /// The converted value.
        /// </param>
        /// <returns>
        /// true if the converstion attempt happened and was successful; false, otherwise.
        /// </returns>
        private static bool TryConvertEnumerable(object value, DateTimeKind targetKind, TimeZoneInfo timeZone, out object converted)
        {
            converted = null;

            var type = value.GetType();
            if (type.GetInterface(typeof(IEnumerable<>).FullName) == null)
                return false;

            var enumerable = (value as IEnumerable).Cast<object>();
            var count = enumerable.Count();
            if (count == 0)
                return true;

            var elementType = enumerable.First().GetType();
            if (!IsDateTime(elementType) && !IsTimeSpan(elementType))
                return false;

            var array = type.IsArray ? Array.CreateInstance(elementType, count) : null;
            var list = type.IsArray ? null : (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            var index = 0;

            foreach (var item in enumerable)
            {
                object convertedItem = null;

                if (item != null)
                    TryConvertDateTimeValue(item, targetKind, timeZone, out convertedItem);

                if (type.IsArray)
                    array.SetValue(convertedItem, index++);
                else
                    list.Add(convertedItem);
            }

            if (type.IsArray)
            {
                converted = array;
            }
            else
            {
                var genericType = type.GetGenericTypeDefinition();
                var constructedType = genericType.MakeGenericType(elementType);
                converted = Activator.CreateInstance(constructedType, list);
            }

            return true;
        }
    }
}
