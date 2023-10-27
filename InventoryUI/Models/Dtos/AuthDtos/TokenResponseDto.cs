namespace InventoryUI.Models.Dtos.AuthDtos
{
    public class TokenResponseDto
    {
        public string JwtToken { get; set; }
        public string Roles { get; set; }
    }
}
