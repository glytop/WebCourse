using Itransition.Trainee.Web.Data.Interface.Models;

namespace Itransition.Trainee.Web.Data.Interface.Repositories
{
    public interface IUserRepository<T> : IBaseRepository<T>
        where T : IUserData
    {
        T? GetByEmail(string email);
        T? GetById(Guid id);
        List<T> GetAll();
        void Add(T user);
        void Update(T user);
        void Delete(Guid id);
    }
}
