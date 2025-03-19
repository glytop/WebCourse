using Itransition.Trainee.Web.Data.Interface.Repositories;
using Itransition.Trainee.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Itransition.Trainee.Web.Data.Repositories
{
    public interface IUserRepositoryReal : IUserRepository<UserData>
    {
        public void BlockUsers(List<Guid> id);
        public void UnblockUsers(List<Guid> id);
        public void DeleteUsers(List<Guid> id);
    }
    public class UserRepository : BaseRepository<UserData>, IUserRepositoryReal
    {
        public UserRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public UserData? GetByEmail(string email)
        {
            return _webDbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public UserData? GetById(Guid id)
        {
            return _webDbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<UserData> GetAll()
        {
            return _webDbContext
                .Users
                .ToList();
        }

        public void Add(UserData user)
        {
            if (user.Id == Guid.Empty)
            {
                user.Id = Guid.NewGuid();
            }

            _webDbContext.Users.Add(user);
            _webDbContext.SaveChanges();
        }

        public void Update(UserData user)
        {
            _webDbContext.Users.Update(user);
            _webDbContext.SaveChanges();
        }

        public void BlockUsers(List<Guid> id)
        {
            var users = _webDbContext.Users.Where(u => id.Contains(u.Id)).ToList();
            foreach (var user in users)
            {
                user.IsBlocked = true;
            }
            _webDbContext.SaveChanges();
        }

        public void UnblockUsers(List<Guid> id)
        {
            var users = _webDbContext.Users.Where(u => id.Contains(u.Id)).ToList();
            foreach (var user in users)
            {
                user.IsBlocked = false;
            }
            _webDbContext.SaveChanges();
        }

        public void DeleteUsers(List<Guid> id)
        {
            var users = _webDbContext.Users.Where(u => id.Contains(u.Id)).ToList();
            _webDbContext.Users.RemoveRange(users);
            _webDbContext.SaveChanges();
        }
    }
}
