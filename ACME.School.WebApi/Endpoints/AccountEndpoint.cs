namespace ACME.School.WebApi.Endpoints
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// Minimal APIs Account
    /// </summary>
    public static class AccountEndpoint
    {
        /// <summary>
        /// Registro de APIs
        /// </summary>
        /// <param name="app"></param>
        public static WebApplication RegisterApis(this WebApplication app)
        {
            var root = app.MapGroup("/api/accounts")
               .WithTags(new[] { "Accounts" })
               .WithOpenApi();

            _ = root.MapPost("/login", Login)
                .WithName("Login")
                .WithDescription("Login")
                .Produces<ResponseDTO>(StatusCodes.Status200OK)
                .Produces<ResponseDTO>(StatusCodes.Status400BadRequest)
                .Produces<ResponseDTO>(StatusCodes.Status401Unauthorized)
                .Produces<ResponseDTO>(StatusCodes.Status500InternalServerError);

            return app;
        }

        static async Task<IResult> Login(
            [FromServices] IConfiguration config,
            [FromServices] IUserService usersService,
            [FromServices] IValidator<LoginDTO> validator,
            [FromBody] LoginDTO loginDTO)
        {
            #region Validar condiciones previas

            ValidationResult validationResult = await validator.ValidateAsync(loginDTO);
            if (!validationResult.IsValid)
            {
                ResponseDTO error = new() { Message = string.Join(", ", validationResult.Errors.Select(failure => $"Error: {failure.ErrorMessage}")) };
                return Results.BadRequest(error);
            }

            #endregion

            try
            {
                UserDTO user = usersService.GetUser(loginDTO.UserId, loginDTO.Password);
                if (user != null)
                {
                    // Crear los claims basados en la información del usuario
                    string issuer = config["Jwt:Issuer"];
                    string audience = config["Jwt:Audience"];
                    byte[] key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
                    long expiresIn = long.Parse(config["Jwt:ExpiresIn"]); // segundos
                    SecurityTokenDescriptor tokenDescriptor = new()
                    {
                        Subject = new ClaimsIdentity(
                        [
                            new Claim("UserId", user.UserId.ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                        ]),
                        Expires = DateTime.UtcNow.AddSeconds(expiresIn),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };
                    JwtSecurityTokenHandler tokenHandler = new();
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                    string stringToken = tokenHandler.WriteToken(token);

                    return Results.Ok(new ResponseDTO
                    {
                        Data = new TokenResponseDTO
                        {
                            AccessToken = stringToken,
                            ExpiresIn = expiresIn,
                            TokenType = "bearer"
                        },
                        IsSuccess = true,
                        Message = "Login OK"
                    });
                }
                else
                    return Results.Unauthorized();
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.InnerException?.Message) ? ex.Message : ex.InnerException?.Message;
                string stackTrace = $"{ex?.StackTrace} | {ex?.InnerException?.StackTrace}";

                ResponseDTO error = new() { Message = $"{message}, {stackTrace}" };
                return Results.Json(error, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
