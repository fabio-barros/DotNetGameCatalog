using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class GameAlreadyExistsException : Exception
    {
        public GameAlreadyExistsException() : base("Game Already Exists.")
        {
            
        }

    }
}
