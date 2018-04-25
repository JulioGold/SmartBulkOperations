using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SmartBulkOperations
{
    public sealed class BulkHelper
    {
        /// <summary>
        /// Return a string with comma separated properties from passed type.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static string CommaSeparatedPropertiesNameFromType(Type sourceObject)
        {
            if (sourceObject == null)
            {
                return null;
            }

            string comaSeparatedProperties = String.Join(",", sourceObject.GetProperties().Where(w => !(bool)IsVirtual(w)).ToArray().Select(s => s.Name).ToArray());

            return comaSeparatedProperties;
        }

        public static string GetPropertyName<TType, TPropertyType>(Expression<Func<TType, TPropertyType>> entityPropertyName) where TType : class where TPropertyType : struct
        {
            MemberExpression body = entityPropertyName.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)entityPropertyName.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public static List<PropertyInfo> PropertiesToSql(object sourceObject)
        {
            List<PropertyInfo> propertiesValueList = sourceObject
                .GetType()
                .GetProperties()
                .Where(w => !(bool)IsVirtual(w))
                .Select(item =>
                {
                    return item;
                })
                .ToList();

            return propertiesValueList;
        }

        private static string PropertyToSql(PropertyInfo property, object sourceObject)
        {
            if (property.PropertyType == typeof(Int32) || property.PropertyType == typeof(Nullable<Int32>) || property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Nullable<Int16>) || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Nullable<Int64>))
            {
                return ToSqlFromInt((Nullable<Int32>)property.GetValue(sourceObject));
            }
            else if (property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Nullable<Int16>) || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Nullable<Int64>))
            {
                return ToSqlFromInt((Nullable<Int16>)property.GetValue(sourceObject));
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(Nullable<long>) || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Nullable<Int64>))
            {
                return ToSqlFromInt((Nullable<long>)property.GetValue(sourceObject));
            }
            else if (property.PropertyType == typeof(String))
            {
                return ToSqlFromString((string)property.GetValue(sourceObject));
            }
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(Nullable<DateTime>))
            {
                return ToSqlFromDateTime((Nullable<DateTime>)property.GetValue(sourceObject));
            }
            else if (property.PropertyType == typeof(Boolean) || property.PropertyType == typeof(Nullable<Boolean>))
            {
                return ToSqlFromBoolean((Nullable<Boolean>)property.GetValue(sourceObject));
            }
            else
            {
                return property.GetValue(sourceObject).ToString();
            }
        }

        /// <summary>
        /// Return the list of values from the passed object.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static string CommaSeparatedPropertiesValueFromObject(object sourceObject)
        {
            if (sourceObject == null)
            {
                return null;
            }

            List<string> propertiesValueList = PropertiesToSql(sourceObject)
                .Select(item =>
                {
                    string collumnValue = PropertyToSql(item, sourceObject);

                    return collumnValue;
                })
                .ToList();

            string commaSeparatedValues = String.Join(",", propertiesValueList);

            return commaSeparatedValues;
        }

        /// <summary>
        /// Return the list of name&values from the passed object.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static string CommaSeparatedPropertiesNamedValueFromObject(object sourceObject)
        {
            if (sourceObject == null)
            {
                return null;
            }

            List<string> propertiesValueList = PropertiesToSql(sourceObject)
                .Select(item =>
                {
                    string collumnNameValue = $"{item.Name}={PropertyToSql(item, sourceObject)}";

                    return collumnNameValue;
                })
                .ToList();

            string commaSeparatedValues = String.Join(",", propertiesValueList);

            return commaSeparatedValues;
        }

        public static string CmdLineDelimiter<T>(IEnumerable<T> list, T item)
        {
            return list.Last().Equals(item) ? ";" : " UNION";
        }

        public static string ToSqlFromBoolean(bool? value)
        {
            return value == null ? "NULL" : ((bool)value ? "1" : "0");
        }

        public static string ToSqlFromInt(int? value)
        {
            return value == null ? "NULL" : value.ToString();
        }

        public static string ToSqlFromInt(Int16? value)
        {
            return value == null ? "NULL" : value.ToString();
        }

        public static string ToSqlFromInt(long? value)
        {
            return value == null ? "NULL" : value.ToString();
        }

        public static string ToSqlFromString(string value)
        {
            return value == null ? "NULL" : $"'{value}'";
        }

        public static string ToSqlFromDateTime(DateTime? value)
        {
            return value == null ? "NULL" : $"'{value.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
        }

        /// <summary>
        /// Indicate if a property is virtual or not.
        /// </summary>
        /// <param name="self">Self property.</param>
        /// <returns></returns>
        public static bool? IsVirtual(PropertyInfo self)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            bool? found = null;

            foreach (MethodInfo method in self.GetAccessors())
            {
                if (found.HasValue)
                {
                    if (found.Value != method.IsVirtual)
                    {
                        return null;
                    }
                }
                else
                {
                    found = method.IsVirtual;
                }
            }

            return found;
        }
    }
}
