﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

#pragma warning disable 1591

namespace middlerApp.IDP.DataAccess.Entities.Entities
{
    public abstract class UserClaim
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}