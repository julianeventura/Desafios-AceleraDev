using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private CodenationContext codenationContext { get; }
        public AccelerationService(CodenationContext context)
        {
            codenationContext = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            List<int> accelerationIds = codenationContext.Candidates
                .Where(c => c.CompanyId == companyId).Select(c => c.AccelerationId).ToList();
            return codenationContext.Accelerations
                .Where(a => accelerationIds.Contains(a.Id)).ToList();
        }

        public Acceleration FindById(int id)
        {
            return codenationContext.Accelerations.Find(id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id == 0)
                codenationContext.Accelerations.Add(acceleration);
            else
                codenationContext.Accelerations.Update(acceleration);

            return acceleration;
        }
    }
}
