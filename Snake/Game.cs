using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Game
    {
       private int points;
        Snakee snake = new Snakee();
        Direction direction = Direction.UP;

        public Game()
        {
        }

        public int Points { get => points; set => points = value; }
        

        private async Task<Direction> GetDirection()
        {
            
            await Task.Run(() =>
            {

                // Listening to AWSD down

            });
            return direction;
        }
        private async Task MoveSnake()
        {
            
            await Task.Run(() =>
            {

                snake.Move(direction);

            });
            
        }
    }
}
