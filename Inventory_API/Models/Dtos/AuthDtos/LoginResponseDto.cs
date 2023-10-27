namespace Inventory_API.Models.Dtos.AuthDtos
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }    
        public string Roles { get; set; }
    }
}
