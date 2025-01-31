namespace ACME.School.Application.DTOs
{
    public class TokenResponseDTO
    {
        public string TokenType { get; set; }

        public long ExpiresIn { get; set; }

        public string AccessToken { get; set; }
    }
}
