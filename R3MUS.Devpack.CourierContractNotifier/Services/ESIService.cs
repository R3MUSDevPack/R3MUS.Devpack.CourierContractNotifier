using R3MUS.Devpack.ESI;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Corporation;
using R3MUS.Devpack.ESI.Models.Universe;
using R3MUS.Devpack.ESI.Models.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public class ESIService : IESIService
    {
        private readonly ISingleSignOnService _singleSignOnService;

        public ESIService(ISingleSignOnService singleSignOnService)
        { 
            _singleSignOnService = singleSignOnService;
        }

        public KeyValuePair<string, TokenResponse> GetContractToken()
        {
            TokenResponse token;

            if (string.IsNullOrEmpty(Properties.Settings.Default.ContractToken))
            {
                var authToken = _singleSignOnService.SignOn(Properties.Settings.Default.CallbackURL,
                    Properties.Settings.Default.ClientID, new List<string>() {
                        ESI.Infrastructure.Scopes.Contracts.CorporationContractsRead,
                        ESI.Infrastructure.Scopes.Universe.ReadStructures });
                token = SingleSignOn.GetTokensFromAuthenticationToken(Properties.Settings.Default.ClientID,
                    Properties.Settings.Default.AppKey, authToken);
                Properties.Settings.Default.ContractToken = token.RefreshToken;
                Properties.Settings.Default.Save();
            }
            else
            {
                token = SingleSignOn.GetTokensFromRefreshToken(Properties.Settings.Default.ClientID,
                    Properties.Settings.Default.AppKey, Properties.Settings.Default.ContractToken);
            }
            return new KeyValuePair<string, TokenResponse>("CONTRACT", token);
        }

        public KeyValuePair<string, TokenResponse> GetStructureToken()
        {
            TokenResponse token;

            if (string.IsNullOrEmpty(Properties.Settings.Default.StructureToken))
            {
                var authToken = _singleSignOnService.SignOn(Properties.Settings.Default.CallbackURL,
                    Properties.Settings.Default.ClientID, new List<string>() {
                        ESI.Infrastructure.Scopes.Contracts.CorporationContractsRead,
                        ESI.Infrastructure.Scopes.Universe.ReadStructures });
                token = SingleSignOn.GetTokensFromAuthenticationToken(Properties.Settings.Default.ClientID,
                    Properties.Settings.Default.AppKey, authToken);
                Properties.Settings.Default.StructureToken = token.RefreshToken;
                Properties.Settings.Default.Save();
            }
            else
            {
                token = SingleSignOn.GetTokensFromRefreshToken(Properties.Settings.Default.ClientID,
                    Properties.Settings.Default.AppKey, Properties.Settings.Default.StructureToken);
            }
            return new KeyValuePair<string, TokenResponse>("STRUCTURE", token);
        }

        public IEnumerable<Contract> GetContracts(string accessToken)
        {
            var corp = new ESI.Models.Corporation.Detail(Properties.Settings.Default.LogisticsCorpId);
            return corp.GetContracts(accessToken);
        }

        public List<ESI.Models.Character.Detail> GetContacts(IEnumerable<long> ids)
        {
            var result = new List<ESI.Models.Character.Detail>();
            ids.ToList().ForEach(id => {
                try
                {
                    result.Add(new ESI.Models.Character.Detail(id));
                }
                catch
                {
                    result.Add(new ESI.Models.Character.Detail() { Id = id, Name = "Unknown Character" });
                }
            });
            return result;
        }

        public List<Structure> GetStructures(IEnumerable<long> ids, string token)
        {
            var result = new List<Structure>();
            ids.ToList().ForEach(id => {
                try
                {
                    result.Add(new Structure(id, token));
                }
                catch
                {
                    result.Add(new Structure() { Id = id, Name = "Unknown Structure" });
                }
            });
            return result;
        }

        public List<Station> GetStations(IEnumerable<long> ids)
        {
            var result = new List<Station>();
            ids.ToList().ForEach(id => {
                try
                {
                    result.Add(new Station(id));
                }
                catch
                {
                    result.Add(new Station() { Id = id, Name = "Unknown Station" });
                }
            });
            return result;
        }
    }
}
