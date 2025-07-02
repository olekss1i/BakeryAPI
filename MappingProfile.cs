using AutoMapper;
using BakeryAPI.DTOs;
using BakeryAPI.Models;

namespace BakeryAPI.Utils
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Feedback mappings
			CreateMap<Feedback, FeedbackResponseDTO>()
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"));

			CreateMap<FeedbackCreateDTO, Feedback>();
		}
	}
}