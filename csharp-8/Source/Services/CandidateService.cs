using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext codenationContext { get; }
        public CandidateService(CodenationContext context)
        {
            codenationContext = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return codenationContext.Candidates
                .Where(c => c.AccelerationId == accelerationId).ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return codenationContext.Candidates
                .Where(c => c.CompanyId == companyId).ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return codenationContext.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            if ((candidate.UserId == 0) && (candidate.AccelerationId == 0) && (candidate.CompanyId == 0))
                codenationContext.Candidates.Add(candidate);
            else
                codenationContext.Candidates.Update(candidate);

            return candidate;
        }
    }
}
