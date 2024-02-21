using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaQuest.Models;

namespace TriviaQuest.Interfaces
{
    public interface ITriviaProvider
    {
        Task<Root> LoadQuestionsAsync(string category, string difficulty, string type);
    }
}
