using AutoMapper;

namespace ZavrsniIspit.Models
{
    public class RibaProfile : Profile
    {
        public RibaProfile() 
        {
            CreateMap<Riba, RibaDetailDTO>(); // automatski će mapirati Author.Name u AuthorName
            //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // ako želimo eksplicitno zadati mapranje
        }

    }
}
