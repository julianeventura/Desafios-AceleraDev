using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext codenationContext { get; }
        public SubmissionService(CodenationContext context)
        {
            codenationContext = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return codenationContext.Candidates
                .Where(c => c.AccelerationId == accelerationId)
                .Select(c => c.User)
                .SelectMany(u => u.Submissions)
                .Where(c => c.ChallengeId == challengeId)
                .Distinct()
                .ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return codenationContext.Submissions
                .Where(c => c.ChallengeId == challengeId)
                .Select(s => s.Score)
                .Max();
        }

        public Submission Save(Submission submission)
        {
            if ((submission.UserId == 0) && (submission.ChallengeId == 0))
                codenationContext.Submissions.Add(submission);
            else
                codenationContext.Submissions.Update(submission);

            return submission;
        }
    }
}
