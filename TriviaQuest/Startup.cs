using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaQuest.Interfaces;
using TriviaQuest.Services;

namespace TriviaQuest
{
    public class Startup
    {
        
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDataProvider, DataProviderService>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }


    }
}
