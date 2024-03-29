﻿using System;
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
    [MessageName("IDPUsers")]
    public class IDPUsersServerMethodsHub : ServerMethods<UIHub>
    {
        public IUsersService UserService { get; }
        public DataEventDispatcher EventDispatcher { get; }
        private readonly IMapper _mapper;


        public IDPUsersServerMethodsHub(IUsersService userService, IMapper mapper, DataEventDispatcher eventDispatcher)
        {
            UserService = userService;
            EventDispatcher = eventDispatcher;
            _mapper = mapper;

        }


        public async Task<List<MUserListDto>> GetUsersList()
        {
            var roles = await UserService.GetAllUserListDtosAsync();
            return roles;
        }

        public IObservable<DataEvent> Subscribe()
        {
            return EventDispatcher.Notifications.Select(ev =>
            {
                var z = ev;
                return z;
            }).Where(ev => ev.Subject == "IDPUsers");
        }
    }
}
