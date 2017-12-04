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
        public event EventHandler BlockAdded;
        protected virtual void OnBlockAdded(EventArgs e)
        {
            BlockAdded?.Invoke(this, e);
        }
        private int posx;
        private int posy;
        private int numOfBlocks;
        List<Block> blocks = new List<Block>();

        public Snakee()
        {
            numOfBlocks = 7;
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

        public void Eat()
        {
            NumOfBlocks += 1;
            Block block = new Block();
            block.Posx = blocks.Last().Posx;
            block.Posy = blocks.Last().Posy;
            blocks.Add(block);
            EventArgs e = new EventArgs();
            OnBlockAdded(e);
        }
        public void Move(Direction direction)
        {
            
            switch (direction)
            {
                case Direction.UP:
                    blocks[0].Posy -= 1;
                    break;
                      
                    case Direction.DOWN:

                    blocks[0].Posy += 1;
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
            int index = blocks.Count;
            this.Posx = blocks[0].Posx;
            this.Posy = blocks[0].Posy;
            while (index > 1)
            { 
                blocks[index - 1].Posy = blocks[index - 2].Posy;
                blocks[index - 1].Posx = blocks[index - 2].Posx;
                index--;
            }
        }
    }
    
}
