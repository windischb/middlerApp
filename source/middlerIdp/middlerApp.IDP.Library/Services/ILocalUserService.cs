using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using middlerApp.IDP.DataAccess.Entities.Models;

namespace middlerApp.IDP.Library.Services
{
    public interface ILocalUserService
    { 
        //Task<bool> ValidateClearTextCredentialsAsync(
        //    string userName, 
        //    string password);
        Task<bool> ValidateCredentialsAsync(
            string userName,
            string password);
        Task<IEnumerable<MUserClaim>> GetUserClaimsBySubjectAsync(
            string subject);         
        Task<MUser> GetUserByUserNameOrEmailAsync(
            string userName);
        Task<MUser> GetUserBySubjectAsync(
            string subject);

        Task AddUserAsync(MUser userToAdd, string password = null);

        void AddUser(
            MUser userToAdd,
            string password);
        Task<bool> IsUserActive(
            string subject);
        Task<bool> ActivateUser(
            string securityCode);
        Task<bool> SaveChangesAsync();
        Task<string> InitiatePasswordResetRequest(
            string email);
        Task<bool> SetPassword(
            string securityCode,
            string password);

        Task<bool> SetPassword(Guid id, string password);

        Task<MUser> GetUserByExternalProvider(
            string provider,
            string providerIdentityKey);
        MUser ProvisionUserFromExternalIdentity(
            string provider,
            string providerIdentityKey,
            IEnumerable<Claim> claims);
        Task AddExternalProviderToUser(
            string subject,
            string provider,
            string providerIdentityKey);
        Task<bool> AddUserSecret(
            string subject, 
            string name, 
            string secret);
        Task<MUserSecret> GetUserSecret(
            string subject,
            string name);
        Task<bool> UserHasRegisteredTotpSecret(
            string subject);


        public Task<bool> ClearPassword(Guid id);

        //Task<List<MUser>> GetAllUsersAsync();
        //Task<MUser> GetUserByIdAsync(Guid id);
        //Task UpdateUserAsync(MUser userModel);
        //Task DeleteUser(params Guid[] id);
    }
}
