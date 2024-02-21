using Guilded.Base.Embeds;
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
        private List<Question> questions = new();
        [Command(Aliases = [ "question", "ask" ])]
        [Description("starts a new trivia game")]
        public async Task Start(CommandEvent invokator)
        {
            var category = "general knowledge";
            try
            {

            
                if (questions.Count < 1)
                {
                    var trivia = new TriviaProviderService();
                    var questions_root = await trivia.LoadQuestionsAsync(category, "easy", "multiple");
                    var rnd = new Random();

                    foreach (var q in questions_root.results!)
                    {
                        var quest = q.question!.Sanitize();
                        var incorrect_answers = q.incorrect_answers;
                        var correct_answer = q.correct_answer;
                        var cat = q.category;
                        var dif = q.difficulty;
                        var type = q.type;

                        questions.Add(new Question(cat, type, dif, quest, correct_answer, incorrect_answers));
                    }
                    var index = rnd.Next(questions.Count);
                    var picked_question = questions[index];
                    var answers = new string[] { picked_question.incorrect_answers[0], picked_question.incorrect_answers[1], picked_question.incorrect_answers[2], picked_question.correct_answer };
                    var shuffled_answers = answers.Shuffle();
                    var embed = new Embed()
                    {
                        Title = picked_question.question,
                        Description = $"Category - {picked_question.category}\r\n\r\n-| **Answers** |-\r\n1.{shuffled_answers[0].Sanitize()}\r\n" +
                                      $"2.{shuffled_answers[1].Sanitize()}\r\n" +
                                      $"3.{shuffled_answers[2].Sanitize()}\r\n" +
                                      $"4.{shuffled_answers[3].Sanitize()}",
                        Footer = new EmbedFooter($"{invokator.ParentClient.Name}"),
                        Timestamp = DateTime.Now
                    };

                    await invokator.CreateMessageAsync(embed);
                    questions.Remove(picked_question);
                }
                else
                {
                    if (questions.Count > 0)
                    {
                        var rnd = new Random();
                        var index = rnd.Next(questions.Count);
                        var picked_question = questions[index];
                        var answers = new string[] { picked_question.incorrect_answers[0], picked_question.incorrect_answers[1], picked_question.incorrect_answers[2], picked_question.correct_answer };
                        var shuffled_answers = answers.Shuffle();
                        var embed = new Embed()
                        {
                            Title = picked_question.question,
                            Description = $"Category - {picked_question.category}\r\n\r\n-| **Answers** |-\r\n1.{shuffled_answers[0].Sanitize()}\r\n" +
                                          $"2.{shuffled_answers[1].Sanitize()}\r\n" +
                                          $"3.{shuffled_answers[2].Sanitize()}\r\n" +
                                          $"4.{shuffled_answers[3].Sanitize()}",
                            Footer = new EmbedFooter($"{invokator.ParentClient.Name}"),
                            Timestamp = DateTime.Now
                        };

                        await invokator.CreateMessageAsync(embed);
                        questions.Remove(picked_question);
                    }

                    if (questions.Count == 0)
                        await invokator.CreateMessageAsync("\r\n**question list is empty, regenerating questions...**");
                }
            }
            catch (Exception ex)
            {

                var test = ex.Message;
            }

        }
    }
}
