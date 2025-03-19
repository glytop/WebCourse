using Itransition.Trainee.Web.Data.Interface.Models;

namespace Itransition.Trainee.Web.Data.Models
{
    public class UserData : BaseData, IUserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
