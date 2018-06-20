using R3MUS.Devpack.CourierContractNotifier.Enums;

namespace R3MUS.Devpack.CourierContractNotifier.Models
{
    public class Endpoint : Entity
    {
        public EndpointTypes EndpointType { get; set; }
        public Entity System { get; set; }
    }
}
