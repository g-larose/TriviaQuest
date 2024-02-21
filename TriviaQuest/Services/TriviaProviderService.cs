using System.Text.Json;
using TriviaQuest.Extensions;
using TriviaQuest.Interfaces;
using TriviaQuest.Models;

namespace TriviaQuest.Services
{
    public class TriviaProviderService : ITriviaProvider
    {
        public async Task<Root> LoadQuestionsAsync(string category, string difficulty, string type)
        {
            var cat = category.FromCatToNumber();
            var endPoint = $"https://opentdb.com/api.php?amount=5&category={cat}&difficulty={difficulty}&type={type}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(endPoint);
            var content = await response.Content.ReadAsStringAsync();
            var question_json = JsonSerializer.Deserialize<Root>(content)!;
            return question_json;
        }
    }
}
