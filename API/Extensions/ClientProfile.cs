using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client,ClientDTO>();
            
            CreateMap<ClientDTO,Client>()
            .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<MembershipType,MembershipTypeDTO>();
            
            CreateMap<MembershipTypeDTO,MembershipType>()
            .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<IdentityRole,Role>();
        }
    }
}