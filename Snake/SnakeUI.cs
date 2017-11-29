using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake
{
    class SnakeUI
    {
        Snakee snake;
        List<BlockUI> blocksUI = new List<BlockUI>();
        public SnakeUI(Snakee snake)
        {
            this.snake = snake;
            for (int i = 0; i < snake.NumOfBlocks; i++)
            {
                BlockUI blockUI = new BlockUI(snake.GetBlock(i));
                blocksUI.Add(blockUI);
            }
        }

        
        public void Draw(Canvas canvas)
        {
            for (int i = 0; i < blocksUI.Count; i++)
            {
                blocksUI[i].Draw(canvas);
            }
        }
    }
}
