using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext _context;
        public SubmissionService(CodenationContext context)
        {
            this._context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return _context.Candidates.
                Where(x => x.AccelerationId == accelerationId).
                Select(x => x.User).
                SelectMany(x => x.Submissions).
                Where(x => x.ChallengeId == challengeId).
                Distinct().
                ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions.
                Where(x => x.ChallengeId == challengeId).
                Max(x => x.Score);
        }

        public Submission Save(Submission submission)
        {
            var found = _context.Submissions.Find(submission.UserId, submission.ChallengeId);
            if (found == null)
                _context.Submissions.Add(submission);
            else
                found.Score = submission.Score;
            _context.SaveChanges();
            return submission;
        }
    }
}
