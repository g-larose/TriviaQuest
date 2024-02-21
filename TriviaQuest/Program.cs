using TriviaQuest.Interfaces;
using TriviaQuest.Services;

namespace TriviaQuest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Bot = new Bot();
            Bot.RunAsync().GetAwaiter().GetResult();
        }

    }
}
