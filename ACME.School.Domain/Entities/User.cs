namespace ACME.School.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }

        public User() { }

        public User(Guid userId,
            string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}
