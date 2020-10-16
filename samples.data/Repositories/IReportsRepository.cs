using Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IReportsRepository
    {
        Task<IQueryable<Association>> GetAssociations(int branchId);
    }
}
