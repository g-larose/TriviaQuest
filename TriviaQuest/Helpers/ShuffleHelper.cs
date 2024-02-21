using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaQuest.Helpers
{
    public class ShuffleHelper
    {
        public static string ShuffleScienceCategory(string[] cats)
        {
           Random rnd = new Random();
           int index = rnd.Next(0, cats.Length);
           var result = cats[index];
           switch (result)
            {
                case "nature":
                    return "17";
                case "computers":
                    return "18";
                case "mathmatics":
                    return "19";
                case "gadgets":
                    return "30";
                default:
                    return "17";
            }
        }

        public static string ShuffleEntertainmentCategory(string[] cats)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, cats.Length);
            var result = cats[index];
            switch (result)
            {
                case "comics":
                    return "29";
                case "anime":
                    return "31";
                case "cartoons":
                    return "32";
                default:
                    return "17";
            }
            
        }
    }
}
