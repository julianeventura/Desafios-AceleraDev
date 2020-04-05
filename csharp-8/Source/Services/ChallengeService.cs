using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext codenationContext { get; }
        public ChallengeService(CodenationContext context)
        {
            codenationContext = context;
 z       }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            List<int> accelerationIds = codenationContext.Candidates
                .Where(c => (c.AccelerationId == accelerationId) && (c.UserId == userId))
                .Select(a => a.AccelerationId).ToList();

            List<Acceleration> accelerations = codenationContext.Accelerations
                .Where(a => accelerationIds.Contains(a.Id)).ToList();

            List<int> challengesIds = accelerations.Select(a => a.ChallengeId).ToList();

            return codenationContext.Challenges
                .Where(c => challengesIds.Contains(c.Id)).ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            if (challenge.Id == 0)
                codenationContext.Challenges.Add(challenge);
            else
                codenationContext.Challenges.Update(challenge);

            return challenge;
        }
    }
}