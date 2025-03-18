namespace Itransition.Trainee.Web.Data.Interface.Models
{
    public interface IUserData : IBaseData
    {
        string Name { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
        DateTime LastLoginTime { get; set; }
        bool IsBlocked { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
