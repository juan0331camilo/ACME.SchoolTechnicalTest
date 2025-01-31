namespace ACME.School.Application.Seeder
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;

    public class DataSeeder
    {
        public static void SeedUsers(IUserService usersService)
        {
            List<UserDTO> users =
            [
                new() { UserId = Guid.Parse("c0de77dd-adc7-4a25-ad0d-d89b4a69ca00"), Password = "7ca86aef7868477" }
            ];

            users.ForEach(user => usersService.CreateUser(user));
        }
    }
}
