using System;
using System.ComponentModel.DataAnnotations;

namespace middlerApp.IDP.Library.DtoModels
{
    public class MExternalClaimDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string Issuer { get; set; }
        
    }
}
