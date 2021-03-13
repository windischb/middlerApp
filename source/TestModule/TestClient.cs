using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModule
{
    public class TestClient: ITestClient
    {
        public Dictionary<string, object> Get(string str1, string str2, object data = null)
        {
            return new Dictionary<string, object>
            {
                ["STR1"] = str1,
                ["STR2"] = str2,
                ["DATA"] = data
            };
        }
    }
}
