using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class GameDoesNotExistException : Exception
    {
        public GameDoesNotExistException(): base("Game Does Not Exist.")
        {

        }
    }
}
