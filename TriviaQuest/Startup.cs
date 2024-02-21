using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaQuest
{
    public class Startup
    {
        private readonly IHost _host;

        public Startup()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                
            }).Build();
        }
    }
}
