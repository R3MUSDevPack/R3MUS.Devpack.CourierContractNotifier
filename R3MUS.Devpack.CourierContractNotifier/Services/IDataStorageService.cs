using R3MUS.Devpack.CourierContractNotifier.Models;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public interface IDataStorageService
    {
        DataStore LoadDataStore();
        void SaveDataStore(DataStore data);
    }
}