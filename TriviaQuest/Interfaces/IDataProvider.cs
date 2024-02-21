using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaQuest.Interfaces
{
    public interface IDataProvider
    {
        string GetToken();
        string GetPrefix();
        string GetConnectionString();
    }
}
