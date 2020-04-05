using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext codenationContext { get; }
        public UserService(CodenationContext context)
        {
            codenationContext = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return codenationContext.Accelerations
                .Where(a => a.Name == name)
                .SelectMany(a => a.Candidates)
                .Select(c => c.User)
                .Distinct()
                .ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            List<int> userIds = codenationContext.Candidates
                .Where(c => c.CompanyId == companyId)
                .Select(cc => cc.UserId)
                .ToList();

            return codenationContext.Users
                .Where(u => userIds.Contains(u.Id))
                .ToList();
        }

        public User FindById(int id)
        {
            return codenationContext.Users.Find(id);
        }

        public User Save(User user)
        {
            if (user.Id == 0)
                codenationContext.Users.Add(user);
            else
                codenationContext.Users.Update(user);

            return user;
        }
    }
}
