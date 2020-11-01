using System;

namespace middlerApp.IDP.Library.DtoModels
{
    public class MApiResourceListDto
    {
        public Guid Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
      
    }
}
