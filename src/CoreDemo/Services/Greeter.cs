using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Services
{
    public class Greeter : IGreeter
    {
        private string greeting;

        public Greeter(IConfiguration config)
        {
            greeting = config["greeting"];
        }

        public string GetGreeting()
        {
            return greeting;
        }
    }
}
