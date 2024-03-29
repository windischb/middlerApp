﻿using System;
using System.Collections.Generic;

namespace middlerApp.IDP.Library.DtoModels
{
    public class MRoleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public ICollection<MUserListDto> Users { get; set; } = new List<MUserListDto>();
    }
}
