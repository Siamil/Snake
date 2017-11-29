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
            numOfBlocks = 3;
            for (int i = 0; i < numOfBlocks; i++)
            {
                Block block = new Block();
                block.Posx = i+1;
                block.Posy = 4;
                blocks.Add(block);
            }

        }

        public int Posy { get => posy; set => posy = value; }
        public int NumOfBlocks { get => numOfBlocks; set => numOfBlocks = value; }
        public int Posx { get => posx; set => posx = value; }
        public Block GetBlock(int index)
        {
            return blocks[index];
        }

        private void Eat()
        {
            NumOfBlocks += 1;
        }
        public void Move(Direction direction)
        {
            
            switch (direction)
            {
                case Direction.UP:
                    blocks[0].Posy += 1;
                    break;
                      
                    case Direction.DOWN:

                    blocks[0].Posy -= 1;
                    break;
                case Direction.LEFT:
                    blocks[0].Posx -= 1;
                    break;
                case Direction.RIGHT:
                    blocks[0].Posx += 1;
                    break;
                default:
                    break;
            }
            int index = 1;
            while (index < numOfBlocks)
            {
                
                blocks[index].Posy = blocks[index - 1].Posy;
                blocks[index].Posx = blocks[index - 1].Posx;
                index++;
            }
        }
    }
    
}
