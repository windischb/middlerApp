using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.DtoModels;

namespace middlerApp.IDP.Library.ExtensionMethods
{
    public static class MExternalClaimExtensions
    {

        public static MExternalClaimDto ToDto(this MExternalClaim externalClaim)
        {
            var dto = new MExternalClaimDto();
            dto.Id = externalClaim.Id;
            dto.Value = externalClaim.Value;
            dto.Issuer = externalClaim.Issuer;
            dto.Type = externalClaim.Type;

            return dto;
        }
    }
}
