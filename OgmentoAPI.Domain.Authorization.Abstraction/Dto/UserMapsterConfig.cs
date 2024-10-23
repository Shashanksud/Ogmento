using Mapster;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Dto
{
	public static class UserMapsterConfig
	{
		public static void  RegisterUserMappings()
		{
			TypeAdapterConfig<UserModel, UserDetailsDto>
				.NewConfig()
				.Map(dest => dest.EmailId, src => src.Email);

			TypeAdapterConfig<UserDetailsDto, UserModel>
				.NewConfig()
				.Map(dest => dest.Email, src => src.EmailId);

			TypeAdapterConfig<AddUserDto, UserModel>
			.NewConfig()
			.Map(dest => dest.UserId, src => 0);

		}

		public static List<UserDetailsDto> MapToDto(List<UserModel> userModels)
		{
			return userModels.Adapt<List<UserDetailsDto>>();
		}

		public static List<UserModel> MapToModel(List<UserDetailsDto> userDetails)
		{
			return userDetails.Adapt<List<UserModel>>();
		}
	}
}
