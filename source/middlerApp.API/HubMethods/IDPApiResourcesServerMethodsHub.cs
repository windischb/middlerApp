using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using middlerApp.Events;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.DtoModels;
using middlerApp.IDP.Library.Services;
using SignalARRR.Attributes;
using SignalARRR.Server;

namespace middlerApp.API.HubMethods
{
    [MessageName("IDPApiResources")]
    public class IDPApiResourcesServerMethodsHub : ServerMethods<UIHub>
    {
        public IApiResourcesService ApiResourcesService { get; }
        public DataEventDispatcher EventDispatcher { get; }
        private readonly IMapper _mapper;


        public IDPApiResourcesServerMethodsHub(IApiResourcesService apiResourcesService, IMapper mapper, DataEventDispatcher eventDispatcher)
        {
            ApiResourcesService = apiResourcesService;
            EventDispatcher = eventDispatcher;
            _mapper = mapper;

        }


        public async Task<List<MApiResourceListDto>> GetApiResourcesList()
        {
            var resources = await ApiResourcesService.GetAllApiResourceDtosAsync();
            return resources;
        }

        public IObservable<DataEvent> Subscribe()
        {
            return EventDispatcher.Notifications.Where(ev => ev.Subject == "IDPApiResources");
        }
    }
}
