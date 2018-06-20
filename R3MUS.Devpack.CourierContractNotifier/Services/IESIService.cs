using System.Collections.Generic;
using R3MUS.Devpack.ESI.Models.Corporation;
using R3MUS.Devpack.ESI.Models.Verification;
using R3MUS.Devpack.ESI.Models.Universe;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public interface IESIService
    {
        List<ESI.Models.Character.Detail> GetContacts(IEnumerable<long> ids);
        IEnumerable<Contract> GetContracts(string accessToken);
        KeyValuePair<string, TokenResponse> GetContractToken();
        List<Station> GetStations(IEnumerable<long> ids);
        List<Structure> GetStructures(IEnumerable<long> ids, string token);
        KeyValuePair<string, TokenResponse> GetStructureToken();
    }
}