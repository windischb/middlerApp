using System;

namespace middlerApp.IDP.Library.DtoModels
{
    public class SecretDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
    }
}
