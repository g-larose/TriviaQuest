using Guilded.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TriviaQuest.Extensions;
using TriviaQuest.Helpers;
using TriviaQuest.Models;
using TriviaQuest.Services;

namespace TriviaQuest.Commands
{
    public class TriviaCommands: CommandModule
    {
        [Command(Aliases = [ "start", "ask" ])]
        [Description("starts a new trivia game")]
        public async Task Start(CommandEvent invokator, string category)
        {
            var trivia = new TriviaProviderService();
            var questions_root = await trivia.LoadQuestionsAsync(category, "easy", "multiple");
            var rnd = new Random();
            var questions = new List<Question>();
            foreach (var item in questions_root.results!)
            {
                var quest = item.question!.Sanitize();
                var incorrect_answers = item.incorrect_answers;
                var correct_answer = item.correct_answer;
                var cat = item.category;
                var dif = item.difficulty;
                var type = item.type;

                questions.Add(new Question(cat, type, dif, quest, correct_answer, incorrect_answers));
            }
            var index = rnd.Next(1, questions.Count);
            var picked_question = questions[index];
            var answers = new string[] { picked_question.incorrect_answers[0], picked_question.incorrect_answers[1], picked_question.incorrect_answers[2], picked_question.correct_answer };
            var shuffled_answers = answers.Shuffle();
            var test = "";
        }
    }
}
