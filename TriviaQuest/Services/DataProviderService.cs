using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TriviaQuest.Interfaces;
using TriviaQuest.Models;

namespace TriviaQuest.Services
{
    public class DataProviderService : IDataProvider
    {
        private readonly string? jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        public string GetConnectionString()
        {
            var json = File.ReadAllText(jsonPath!);
            var conString = JsonSerializer.Deserialize<ConfigJson>(json)!.ConnectionString!;
            return conString;
        }

        public string GetPrefix()
        {
            var json = File.ReadAllText(jsonPath!);
            var prefix = JsonSerializer.Deserialize<ConfigJson>(json)!.Prefix!;
            return prefix;
        }

        public string GetToken()
        {
            var json = File.ReadAllText(jsonPath!);
            var token = JsonSerializer.Deserialize<ConfigJson>(json)!.Token!;
            return token;
        }
    }
}
