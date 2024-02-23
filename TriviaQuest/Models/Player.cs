using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaQuest.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string? Membername { get; set; }
        public string? ServerId { get; set; }

    }
}
