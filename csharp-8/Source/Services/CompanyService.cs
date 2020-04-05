using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext codenationContext { get; }
        public CompanyService(CodenationContext context)
        {
            codenationContext = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return codenationContext.Accelerations
                .Where(a => a.Id == accelerationId)
                .SelectMany(a => a.Candidates)
                .Select(a => a.Company)
                .Distinct()
                .ToList();
        }

        public Company FindById(int id)
        {
            return codenationContext.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return codenationContext.Candidates
                .Where(c => c.UserId == userId)
                .Select(c => c.Company)
                .Distinct()
                .ToList();
        }

        public Company Save(Company company)
        {
            if (company.Id == 0)
                codenationContext.Companies.Add(company);
            else
                codenationContext.Companies.Update(company);

            return company;
        }
    }
}