using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaQuest.Extensions;
using TriviaQuest.Models;
using TriviaQuest.Services;

namespace TriviaQuest.Helpers
{
    public class TriviaHelper
    {
        private List<Question> _questions = new();
        public async Task<Question> AskQuestionAsync(string category)
        {
            _questions = new();
            var rnd = new Random();
            if (_questions.Count < 1)
            {
                
                var trivia = new TriviaProviderService();
                var questions_root = await trivia.LoadQuestionsAsync(category, "easy", "multiple");
                

                foreach (var q in questions_root.results!)
                {
                    var quest = q.question!.Sanitize();
                    var incorrect_answers = q.incorrect_answers;
                    var correct_answer = q.correct_answer;
                    var cat = q.category;
                    var dif = q.difficulty;
                    var type = q.type;

                    _questions.Add(new Question(cat, type, dif, quest, correct_answer, incorrect_answers));
                }
                var index = rnd.Next(_questions.Count);
                var picked_question = _questions[index];
                _questions.Remove(picked_question);
                return picked_question;
            }
            else
            {
                var index = rnd.Next(_questions.Count);
                var picked_question = _questions[index];
                _questions.Remove(picked_question);
                return picked_question;
            }
        }
    }
}
