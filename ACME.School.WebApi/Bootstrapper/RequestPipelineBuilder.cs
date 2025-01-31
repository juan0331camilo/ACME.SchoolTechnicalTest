using ACME.School.Application.Interfaces;
using ACME.School.Application.Seeder;

namespace ACME.School.WebApi.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestPipelineBuilder
    {
        /// <summary>
        /// configure
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var usersService = scope.ServiceProvider.GetRequiredService<IUserService>();
                DataSeeder.SeedUsers(usersService);
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
