using middlerApp.IDP.DataAccess.Entities.Models;

namespace middlerApp.API.Providers
{
    public interface IExternalUserFactory
    {
        string GetSubject();

        MUser BuildUser();

        void UpdateClaims(MUser existingUser);

    }
}