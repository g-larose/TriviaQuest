using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaQuest.Models
{
    public class Root
    {
        public int response_code { get; set; }
        public List<Question>? results { get; set; }
    }
}
