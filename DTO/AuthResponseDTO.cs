namespace BakeryAPI.DTOs
{
	public class AuthResponseDTO
	{
		public string Token { get; set; }           // JWT ����� ��� ������ ����� �����������
		public string Username { get; set; }        // ��� ������������
		public string Role { get; set; }            // ���� ������������ (���� ����)
		public int UserId { get; set; }              // Id ������������
	}
}
