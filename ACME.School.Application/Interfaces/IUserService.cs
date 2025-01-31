namespace ACME.School.Application.Interfaces
{
    using ACME.School.Application.DTOs;

    public interface IUserService
    {
        Guid CreateUser(UserDTO userInfoDTO);

        UserDTO GetUser(Guid userId, string password);
    }
}
