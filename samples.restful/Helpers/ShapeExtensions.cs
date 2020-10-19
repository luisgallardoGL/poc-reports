using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace samples.restful.Helpers
{
    public static class ShapeExtensions
    {
        private static readonly Dictionary<string, PropertyInfo[]> TypePropertiesCache = new Dictionary<string, PropertyInfo[]>();
        public static IEnumerable<dynamic> ShapeData<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (string.IsNullOrEmpty(fields))
            {
                return source.ToDynamicList();
            }

            var propertyList = GetPropertiesByFields<TSource>(fields);
            return source.Select(o => CreateShapedObject(o, propertyList));
        }
        public static dynamic ShapeData<TSource>(this TSource source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(fields))
            {
                return source;
            }
            var propertyList = GetPropertiesByFields<TSource>(fields);
            return CreateShapedObject(source, propertyList);
        }

        private static ExpandoObject CreateShapedObject<TSource>(TSource source, IEnumerable<PropertyInfo> properties)
        {
            var dataShapedObject = new ExpandoObject();
            foreach (var propertyInfo in properties)
            {
                var propertyValue = propertyInfo.GetValue(source, null);
                ((IDictionary<string,object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
            }

            return dataShapedObject;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesByFields<TSource>(string fields)
        {
            var propertiesByFields = new List<PropertyInfo>();
            var objectProperties = GetPropertiesByObjectType(typeof(TSource)).ToList();
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = objectProperties.Find(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
                if (propertyInfo == null)
                {
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                }
                propertiesByFields.Add(propertyInfo);
            }

            return propertiesByFields;
        }

        public static bool ArePropertiesValidFor<TSource>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }
            var objectProperties = GetPropertiesByObjectType(typeof(TSource)).ToList();
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = objectProperties.Find(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesByObjectType(Type objectType)
        {
            PropertyInfo[] properties;
            if (TypePropertiesCache.ContainsKey(objectType.Name))
            {
                TypePropertiesCache.TryGetValue(objectType.Name, out properties);
                return properties;
            }
            properties = objectType.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            TypePropertiesCache.Add(objectType.Name, properties);
            return properties;
        }
    }
}