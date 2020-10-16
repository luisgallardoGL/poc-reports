using Data.Models;
using Data.Repositories;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Collections.Generic;

namespace graphql.Queries.Reports
{
    [ExtendObjectType(Name = "Query")]
    public class AssociationQueries
    {

        /// <summary>
        /// Gets all association.
        /// </summary>
        /// <param name="repository"></param>
        /// <returns>The association.</returns>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Association> GetAssociations(
            [Service] IReportsRepository repository)
        {
            return repository.GetAssociations(83).Result;
        }
            

    }
}
