namespace BakeryAPI.DTOs
{
	public class AuthResponseDTO
	{
		public string Token { get; set; }           // JWT токен или другой токен авторизации
		public string Username { get; set; }        // »м€ пользовател€
		public string Role { get; set; }            // –оль пользовател€ (если есть)
		public int UserId { get; set; }              // Id пользовател€
	}
}
