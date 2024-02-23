using Guilded.Base.Embeds;
using Guilded.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

        [Command(Aliases = [ "question", "ask" ])]
        [Description("starts a new trivia game")]
        public async Task Start(CommandEvent invokator)
        {
            var trivia = new TriviaHelper();
            var category = "general knowledge";
            var question = await trivia.AskQuestionAsync(category);
            var answers = new string[] { question.incorrect_answers[0], question.incorrect_answers[1], question.incorrect_answers[2], question.correct_answer };
            var correct_answer = question.correct_answer;
          
            var shuffled_answers = answers.Shuffle();
            var correctAnswerIndex = 0;
            var count = 0;
            foreach (var answer in shuffled_answers)
            {
                if (answer == correct_answer)
                {
                    correctAnswerIndex = count;
                    break;
                }
                count++;
            }
            var embed = new Embed()
            {
                Title = question.question,
                Description = $"Category - {question.category}\r\n\r\n-| **Answers** |-\r\n1.{shuffled_answers[0].Sanitize()}\r\n" +
                              $"2.{shuffled_answers[1].Sanitize()}\r\n" +
                              $"3.{shuffled_answers[2].Sanitize()}\r\n" +
                              $"4.{shuffled_answers[3].Sanitize()}\r\n" +
                              $"- |Correct Answer| - -> {question.correct_answer}",
                Footer = new EmbedFooter($"{invokator.ParentClient.Name}"),
                Timestamp = DateTime.Now
            };
            var emojis = new uint[] { 2245981, 2245982, 2245983, 2245984 };

            var message = await invokator.CreateMessageAsync(embed);

            foreach (var emoji in emojis)
            {
                await message.AddReactionAsync(emoji);
            }

            invokator.ParentClient.MessageReactionAdded
                         .Where(e => e.CreatedBy == invokator.CreatedBy)
                         .Subscribe(async reaction =>
                         {
                             var id = reaction.Name switch
                             {
                                 "one" => 0,
                                 "two" => 1,
                                 "three" => 2,
                                 "four" => 3,
                                 _ => 4
                             };

                             switch (reaction.Name)
                             {
                                 case "one":
                                     if (id == correctAnswerIndex)
                                     {
                                         await invokator.ReplyAsync("correct");
                                         foreach (var r in emojis)
                                         {
                                             await message.RemoveReactionAsync(r);
                                             await Task.Delay(500);
                                             
                                         }
                                         //message = null;
                                         return;
                                     }
                                     break;
                                 case "two":
                                     if (id == correctAnswerIndex)
                                     {
                                         await invokator.ReplyAsync("correct");
                                         foreach (var r in emojis)
                                         {
                                             await message.RemoveReactionAsync(r);
                                             await Task.Delay(500);
                                            
                                         }
                                         //message = null;
                                         return;
                                     }
                                     break;
                                 case "three":
                                     if (id == correctAnswerIndex)
                                     {
                                         await invokator.ReplyAsync("correct");
                                         foreach (var r in emojis)
                                         {
                                             await message.RemoveReactionAsync(r);
                                             await Task.Delay(500);

                                         }
                                        // message = null;
                                         return;
                                     }
                                     break;
                                 case "four":
                                     if (id == correctAnswerIndex)
                                     {
                                         await invokator.ReplyAsync("correct");
                                         foreach (var r in emojis)
                                         {
                                             await message.RemoveReactionAsync(r);
                                             await Task.Delay(500);
                                         }
                                         //message = null;
                                         return;
                                     }
                                     break;
                                 default:
                                     break;
                             }
                             //if (id == correctAnswerIndex)
                             //    await invokator.CreateMessageAsync("correct");

                         });

        }
    }
}
