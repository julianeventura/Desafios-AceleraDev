using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext _context;
        public CompanyService(CodenationContext context)
        {
            this._context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return _context.Accelerations.
                Where(x => x.Id == accelerationId).
                SelectMany(x => x.Candidates).
                Select(x => x.Company).
                Distinct().
                ToList();
        }

        public Company FindById(int id)
        {
            return _context.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return _context.Candidates.
                Where(x => x.UserId == userId).
                Select(x => x.Company).
                Distinct().
                ToList();
        }

        public Company Save(Company company)
        {            
            var state = company.Id == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(company).State = state;
            _context.SaveChanges();
            return company;
        }
    }
}