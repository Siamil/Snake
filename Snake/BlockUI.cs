using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class BlockUI
    {
        Block block;
        BlockUI(Block block)
        {
            this.block = block;
        }
        private int width;
        private int heigt;

        public int Width { get => width; set => width = value; }
        public int Heigt { get => heigt; set => heigt = value; }
        private void Draw()
        {

        }
    }
}
