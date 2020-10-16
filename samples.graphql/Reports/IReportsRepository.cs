using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Reports
{
    public interface IReportsRepository
    {
        Task<IQueryable<Association>> GetAssociations(int branchId);

    }
}
