using R3MUS.Devpack.CourierContractNotifier.Models;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public interface IDiscordService
    {
        void PostMessage(NotificationRequest request);
    }
}