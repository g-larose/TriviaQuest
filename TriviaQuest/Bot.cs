using Guilded;
using Guilded.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaQuest.Commands;
using TriviaQuest.Interfaces;
using TriviaQuest.Services;

namespace TriviaQuest
{
    public class Bot
    {

        private static readonly IDataProvider _dataProviderService = new DataProviderService();
        private static readonly string? Token = _dataProviderService.GetToken();
        private static readonly string? Prefix = _dataProviderService.GetPrefix();
        private static readonly string? timePattern = "hh:mm:ss tt";
        
        public async Task RunAsync()
        {
            
            await using var client = new GuildedBotClient(Token!)
                .AddCommands(new TriviaCommands(), Prefix!);

            client.Prepared
                .Subscribe(async me =>
                {
                    var date = DateTime.Now;
                    var time = DateTime.Now.ToString(timePattern);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[{date}][{time}] [{client.Name}] connected!");
                });




            await client.ConnectAsync();
            await client.SetStatusAsync("Generating Question...", 90002579);
            await Task.Delay(-1);
        }
    }
}
