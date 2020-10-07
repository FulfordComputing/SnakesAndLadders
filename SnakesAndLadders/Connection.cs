using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    class Connection
    {
        public enum ConnectionType
        {
            SNAKE,
            LADDER,
            NEXT
        }

        public GameSquare from;
        public GameSquare to;
        public ConnectionType connectionType;
    }
}
