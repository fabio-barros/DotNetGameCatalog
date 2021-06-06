using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Game
    {
        public int Id;

        public string Title { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        public DateTime Year { get; set; }

        public decimal Price { get; set; }
    }
}
