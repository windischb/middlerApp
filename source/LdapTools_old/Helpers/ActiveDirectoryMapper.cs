using System.Collections.Generic;

namespace LdapTools.Helpers
{
    public class ActiveDirectoryMapper
    {

        Dictionary<string, ConcreteLdapAttribute> AttributeMappers = new Dictionary<string, ConcreteLdapAttribute>()
        {
            {"samAccountName", new ConcreteLdapAttribute().MapFromLdap(TypeMapper.ToStringValue)},

        };
    }
}
