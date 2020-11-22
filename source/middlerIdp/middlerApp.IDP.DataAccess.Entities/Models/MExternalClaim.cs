﻿using System;
using System.ComponentModel.DataAnnotations;

namespace middlerApp.IDP.DataAccess.Entities.Models
{
    public class MExternalClaim : IConcurrencyAware
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Type { get; set; }

        [MaxLength(250)]
        [Required]
        public string Value { get; set; }

        public string Issuer { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public Guid UserId { get; set; }

        public MUser User { get; set; }
    }
}
