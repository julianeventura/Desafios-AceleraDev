using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext _context;
        public ChallengeService(CodenationContext context)
        {
            this._context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            return _context.Users.
                Where(x => x.Id == userId).
                SelectMany(x => x.Candidates).
                Where(x => x.AccelerationId == accelerationId).
                Select(x => x.Acceleration.Challenge).
                Distinct().
                ToList();            
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            var state = challenge.Id == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(challenge).State = state;
            _context.SaveChanges();
            return challenge;
        }
    }
}