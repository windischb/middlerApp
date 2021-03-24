using System;
using System.Collections.Generic;
using JintTsDefinition;

namespace TsDefinitionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var defBuilder = new DefinitionBuilder();

            defBuilder.AddTypes(typeof(List<>));


            var Definitions = defBuilder.Render();


        }
    }
}
