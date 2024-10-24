using OgmentoAPI.Domain.Authorization.Abstractions.Enums;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Dto
{
	public class AddUserDto : UserDetailsDto
	{
		public string Password { get; set; }
		
	}
}
