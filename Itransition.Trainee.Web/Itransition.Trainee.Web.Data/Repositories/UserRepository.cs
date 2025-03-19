using Itransition.Trainee.Web.Data.Interface.Repositories;
using Itransition.Trainee.Web.Data.Models;

namespace Itransition.Trainee.Web.Data.Repositories
{
    public interface IUserRepositoryReal : IUserRepository<UserData>
    {

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

        public void Delete(Guid id)
        {
            var user = _webDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _webDbContext.Remove(user);
                _webDbContext.SaveChanges();
            }
        }

    }
}
