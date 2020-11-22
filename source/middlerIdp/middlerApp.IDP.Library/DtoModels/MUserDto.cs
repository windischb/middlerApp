﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using middlerApp.IDP.DataAccess.Entities.Models;

namespace middlerApp.IDP.Library.DtoModels
{
    public class MUserDto
    {

        public Guid Id { get; set; }

        [MaxLength(200)]
        public string UserName { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public bool HasPassword { get; set; }

        public bool Active { get; set; }

        public ICollection<MUserClaimDto> Claims { get; set; } = new List<MUserClaimDto>();
        public ICollection<MExternalClaimDto> ExternalClaims { get; set; } = new List<MExternalClaimDto>();
        public ICollection<MUserLogin> Logins { get; set; } = new List<MUserLogin>();
        public ICollection<MUserSecret> Secrets { get; set; } = new List<MUserSecret>();

        public ICollection<MRoleListDto> Roles { get; set; } = new List<MRoleListDto>();

    }
}
