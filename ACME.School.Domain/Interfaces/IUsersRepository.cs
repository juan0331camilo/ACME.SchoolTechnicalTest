namespace ACME.School.Domain.Interfaces
{
    using ACME.School.Domain.Entities;

    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        void AddUser(User user);
    }
}
