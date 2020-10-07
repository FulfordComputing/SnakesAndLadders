using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    class GameSquare
    {
        public int number;
        
        public List<Connection> connections;

        public GameSquare()
        {
            connections = new List<Connection>();
        }
    }
}
