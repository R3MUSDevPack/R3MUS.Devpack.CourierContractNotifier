namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public interface IContractNotificationService
    {
        bool LogisticsCorpHasStructureAccess { get; }

        void RunWorker();
    }
}