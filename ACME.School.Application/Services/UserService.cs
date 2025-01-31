namespace ACME.School.Application.Services
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;
    using ACME.School.Domain.Entities;
    using ACME.School.Domain.Interfaces;
    using AutoMapper;

    public class UserService(
        IMapper mapper,
        IUsersRepository usersRepository) : IUserService
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public Guid CreateUser(
            UserDTO userInfoDTO)
        {
            var user = new User(userInfoDTO.UserId,
                userInfoDTO.Password);

            _usersRepository.AddUser(user);
            return user.UserId;
        }

        public UserDTO GetUser(
            Guid userId,
            string password)
        {
            List<User> users = _usersRepository.GetUsers().ToList();
            UserDTO userDTO = users
                .Where(x => x.UserId == userId && x.Password == password)
                .Select(x => mapper.Map<UserDTO>(x))
                .FirstOrDefault();

            return userDTO;
        }
    }
}
