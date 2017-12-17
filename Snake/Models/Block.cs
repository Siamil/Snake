using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Block
    {
        private int posx;
        private int posy;

        public Block()
        {
        }

        public int Posx { get => posx; set => posx = value; }
        public int Posy { get => posy; set => posy = value; }
    }
}
