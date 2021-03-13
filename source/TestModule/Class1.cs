using System;
using System.Collections.Generic;
using Scripter.Shared;

namespace TestModule
{
    public class TestModule : IScripterModule
    {

        public Dictionary<string, object> Test1(string str1, string str2, object data = null)
        {
            return new Dictionary<string, object>
            {
                ["str1"] = str1,
                ["str2"] = str2,
                ["data"] = data
            };
        }

        public ITestClient GetClient()
        {
            return new TestClient();
        }

    }
}
