using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Enums;
using R3MUS.Devpack.CourierContractNotifier.Models;
using R3MUS.Devpack.ESI.Models.Universe;

namespace R3MUS.Devpack.CourierContractNotifier.MappingProfiles
{
    public class StructureProfiles : Profile
    {
        public StructureProfiles()
        {
            CreateMap<Structure, Endpoint>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.System, opt => opt.MapFrom(src => new Entity() { Id = src.Id }))
                .ForMember(dest => dest.EndpointType, opt => opt.MapFrom(src => EndpointTypes.Structure));
        }
    }
}
