using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Models;
using R3MUS.Devpack.ESI.Models.Corporation;

namespace R3MUS.Devpack.CourierContractNotifier.MappingProfiles
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, NotificationRequest>()
                .ForMember(dest => dest.Issued, opt => opt.MapFrom(src => src.Issued))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => new Endpoint() { Id = src.StartEndpointId }))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => new Endpoint() { Id = src.EndEndpointId }))
                .ForMember(dest => dest.Issuer, opt => opt.MapFrom(src => new Entity() { Id = src.IssuerId }))
                .ForMember(dest => dest.Collateral, opt => opt.MapFrom(src => src.Collateral))
                .ForMember(dest => dest.Reward, opt => opt.MapFrom(src => src.Reward))
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Accepted, opt => opt.MapFrom(src => src.Accepted))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed))
                .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
