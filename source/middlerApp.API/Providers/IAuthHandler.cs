using System.Security.Claims;
using middlerApp.IDP.DataAccess.Entities.Entities;

namespace middlerApp.API.Providers
{
    public interface IAuthHandler
    {

        void Register(AuthenticationProvider provider);

        void UnRegister();

        IExternalUserFactory GetUserFactory(ClaimsPrincipal claimsPrincipal);

    }
}