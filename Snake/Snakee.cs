using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    enum Direction { UP, DOWN, LEFT, RIGHT};
    class Snakee
    {
        private int posx;
        private int posy;
        private int numOfBlocks;
        List<Block> blocks = new List<Block>();

        public Snakee()
        {
        }

        public int Posy { get => posy; set => posy = value; }
        public int NumOfBlocks { get => numOfBlocks; set => numOfBlocks = value; }
        public int Posx { get => posx; set => posx = value; }

        private void Eat()
        {
            NumOfBlocks += 1;
        }
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    Posy += 10;
                    break;
                case Direction.DOWN:
                    Posy -= 10;
                    break;
                case Direction.LEFT:
                    Posx -= 10;
                    break;
                case Direction.RIGHT:
                    Posx += 10;
                    break;
                default:
                    break;
            }
        }
    }
    
}
