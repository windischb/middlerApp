using System;
using System.ComponentModel.DataAnnotations;

namespace middlerApp.IDP.DataAccess.Entities.Entities
{
    public class UserConsent
    {
        [Key]
        public Guid Id { get; set; }

        public string SubjectId { get; set; }

        public string ClientId { get; set; }

        public string Scopes { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? Expiration { get; set; }

    }
}
