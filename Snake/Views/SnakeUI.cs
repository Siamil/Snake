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
        Canvas canvas;
        
        public SnakeUI(Canvas canvas, Snakee snake)
        {
            this.snake = snake;
            
            for (int i = 0; i < snake.NumOfBlocks; i++)
            {
                this.canvas = canvas;
                BlockUI blockUI = new BlockUI(snake.GetBlock(i), snake.Multi);
                canvas.Children.Add(blockUI.Rect);
                blocksUI.Add(blockUI);
                snake.BlockAdded += addBlockUI;
            }
        }

        

        public void addBlockUI(object sender, EventArgs e)
        {

            BlockUI blockUI = new BlockUI(snake.GetBlock(snake.NumOfBlocks- 1), snake.Multi);
            canvas.Children.Add(blockUI.Rect);
            blocksUI.Add(blockUI);

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
