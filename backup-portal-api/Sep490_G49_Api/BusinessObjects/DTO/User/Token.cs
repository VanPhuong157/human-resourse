namespace BusinessObjects.DTO.User
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }

    }
}
