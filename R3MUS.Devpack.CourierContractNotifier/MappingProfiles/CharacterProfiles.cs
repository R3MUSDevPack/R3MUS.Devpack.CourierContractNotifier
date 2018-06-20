using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.Devpack.CourierContractNotifier.MappingProfiles
{
    public class CharacterProfiles : Profile
    {
        public CharacterProfiles()
        {
            CreateMap<ESI.Models.Character.Detail, Entity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
