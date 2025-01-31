namespace ACME.School.Domain.Repositories
{
    using ACME.School.Domain.Entities;
    using ACME.School.Domain.Interfaces;

    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly Dictionary<Guid, User> _users = [];
        public IEnumerable<User> GetUsers()
        {
            return _users.Values;
        }

        public User GetUser(Guid id)
        {
            return _users[id];
        }

        public void AddUser(User user)
        {
            _users.Add(user.UserId, user);
        }
    }
}
