using AutoMapper;
using fulbito_api.Dtos.Tournaments;
using fulbito_api.Dtos.Users;
using fulbito_api.Models;


public class AppMapper : Profile
{
	public AppMapper()
	{
		CreateMap<User, UserDto>().ReverseMap();
		CreateMap<User, CreateUserDto>().ReverseMap();
		CreateMap<User, UpdateUserDto>().ReverseMap();

		CreateMap<Tournament, TournamentDto>().ReverseMap();
		CreateMap<Tournament, CreateTournamentDto>().ReverseMap();
	}
}