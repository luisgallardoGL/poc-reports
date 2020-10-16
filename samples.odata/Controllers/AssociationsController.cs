using Data.Models;
using Data.Repositories;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odata.Controllers
{
    public class AssociationsController : ControllerBase
    {

        private readonly IReportsRepository repo;

        public AssociationsController(IReportsRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [EnableQuery()]
        public async Task<IEnumerable<Association>> Get()
        {
            var assocs = await this.repo.GetAssociations(83);
            return assocs.AsEnumerable();
        }

    }
}
