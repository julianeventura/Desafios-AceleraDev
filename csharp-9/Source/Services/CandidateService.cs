using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext _context;
        public CandidateService(CodenationContext context)
        {
            this._context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates.
                Where(x => x.AccelerationId == accelerationId).
                ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Candidates.
                Where(x => x.CompanyId == companyId).
                ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            var found = _context.Candidates.Find(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);
            if (found == null)
                _context.Candidates.Add(candidate);
            else
                found.Status = candidate.Status;
            _context.SaveChanges();
            return candidate;
        }
    }
}
