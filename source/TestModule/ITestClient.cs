using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModule
{
    public interface ITestClient
    {
        Dictionary<string, object> Get(string str1, string str2, object data = null);
    }
}
