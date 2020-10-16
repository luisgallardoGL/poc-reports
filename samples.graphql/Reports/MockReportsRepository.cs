using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Reports
{
    public class MockReportsRepository : IReportsRepository
    {

        private List<Association> associations;

        public MockReportsRepository()
        {
            associations = FakeAssociation().Generate(1000).ToList();
        }


        public Task<IQueryable<Association>> GetAssociations(int branchId)
        {
            return Task<IQueryable<Association>>.Factory.StartNew(() => associations.AsQueryable());
        }

        private int _id = 0;
        private Faker<Association> FakeAssociation()
        {
            _id = 0;
            return new Faker<Association>()
                .RuleFor(p => p.id, f => _id++)
                .RuleFor(p => p.name, f => f.Company.CompanyName())
                .RuleFor(p => p.legalName, f => f.Lorem.Sentence(10))
                .RuleFor(p => p.liveReason, f => f.Lorem.Sentence(5));
        }
    }
}
