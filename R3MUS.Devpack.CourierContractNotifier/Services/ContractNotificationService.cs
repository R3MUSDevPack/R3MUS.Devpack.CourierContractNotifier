using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public class ContractNotificationService : IContractNotificationService
    {
        private readonly IESIService _esiService;
        private readonly IMapper _mapper;
        private readonly IDiscordService _discordService;
        private readonly ISlackService _slackService;
        private readonly IDataStorageService _dataStorageService;

        public bool LogisticsCorpHasStructureAccess
        {
            get
            {
                return Properties.Settings.Default.LogisticsCorpId == Properties.Settings.Default.StrucureAccessCorpId;
            }
        }

        public ContractNotificationService(IESIService esiService, IMapper mapper,
            IDiscordService discordService, ISlackService slackService, IDataStorageService dataStorageService)
        {
            _esiService = esiService;
            _mapper = mapper;
            _slackService = slackService;
            _discordService = discordService;
            _dataStorageService = dataStorageService;
        }
        
        public void RunWorker()
        {
            var data = _dataStorageService.LoadDataStore();

            var contractTokenResponse = _esiService.GetContractToken();
            var structureTokenResponse = LogisticsCorpHasStructureAccess ? contractTokenResponse : _esiService.GetStructureToken();

            var contracts = _mapper.Map<IEnumerable<NotificationRequest>>(_esiService.GetContracts(contractTokenResponse.Value.AccessToken)
                .Where(w => !w.Status.Equals(NotificationRequest.DeletedStatus)));
            
            data.CheckContracts(contracts.Select(s => s.ContractId));
            
            var issuers = _mapper.Map<IEnumerable<Entity>>(_esiService.GetContacts(contracts.Select(s => s.Issuer.Id).Distinct()));

            var structureIds = contracts.Select(s => s.Destination.Id).Concat(contracts.Select(s => s.Origin.Id)).Distinct().Where(w => w.ToString().Length == 13);
            var stationIds = contracts.Select(s => s.Destination.Id).Concat(contracts.Select(s => s.Origin.Id)).Distinct().Where(w => w.ToString().Length < 13);

            var endpoints = _mapper.Map<IEnumerable<Endpoint>>(
                _esiService.GetStructures(structureIds, structureTokenResponse.Value.AccessToken))
                .Concat(_mapper.Map<IEnumerable<Endpoint>>(_esiService.GetStations(stationIds)));
            
            contracts.ToList().ForEach(contract => {
                contract.Issuer = issuers.First(w => w.Id == contract.Issuer.Id);
                contract.Origin = endpoints.First(w => w.Id == contract.Origin.Id);
                contract.Destination = endpoints.First(w => w.Id == contract.Destination.Id);

                if (data.DoNotification(contract.ContractId, contract.Status))
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(contract.GetTitle());
                    Console.WriteLine(contract.GetMessageText());
                    Console.WriteLine();
                    _discordService.PostMessage(contract);
                    _slackService.PostMessage(contract);
                }

                _dataStorageService.SaveDataStore(data);

            });
            //Console.ReadLine();
        }
    }
}
