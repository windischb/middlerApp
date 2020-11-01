using System;
using System.Reactive.Linq;
using AutoMapper;
using middlerApp.Events;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.Services;
using SignalARRR.Attributes;
using SignalARRR.Server;

namespace middlerApp.API.HubMethods
{
    [MessageName("IDPApiScopes")]
    public class IDPApiScopesServerMethodsHub : ServerMethods<UIHub>
    {
        public IApiScopesService ApiScopesService { get; }
        public DataEventDispatcher EventDispatcher { get; }
        private readonly IMapper _mapper;


        public IDPApiScopesServerMethodsHub(IApiScopesService apiScopesService, IMapper mapper, DataEventDispatcher eventDispatcher)
        {
            ApiScopesService = apiScopesService;
            EventDispatcher = eventDispatcher;
            _mapper = mapper;

        }

        public IObservable<DataEvent> Subscribe()
        {
            return EventDispatcher.Notifications.Where(ev => ev.Subject == "IDPApiScopes");
        }
    }
}
