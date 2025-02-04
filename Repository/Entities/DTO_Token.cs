namespace Repository.Entities;

public class DTO_Token
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DTO_TokenUser User { get; set; }
}