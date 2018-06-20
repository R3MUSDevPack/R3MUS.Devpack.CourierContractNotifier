using R3MUS.Devpack.CourierContractNotifier.Models;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public interface ISlackService
    {
        void PostMessage(NotificationRequest request);
    }
}