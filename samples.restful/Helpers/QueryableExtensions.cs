using System;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace samples.restful.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, string sort) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (string.IsNullOrEmpty(sort))
            {
                return source;
            }

            var sortAfterSplit = sort.Split(',');
            var sortFields = sortAfterSplit.Select(sortField => sortField.EndsWith(" desc")
                    ? $"{sortField.Replace(" desc", " descending")}"
                    : $"{sortField.Replace(" asc", " ascending")}")
                .ToList();

            return source.OrderBy(string.Join(",", sortFields));
        }
    }
}
