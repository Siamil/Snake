using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Game
    {
       private int points;
        Snakee snake;
        Direction direction = Direction.UP;
        //Timer timer = new Timer();
        public event EventHandler SnakeMoved;
        protected virtual void OnSnakeMoved(EventArgs e)
        {
            SnakeMoved?.Invoke(this, e);
        }

        public Game()
        {
            snake = new Snakee();
            MoveSnake();
            //timer.Interval = 300;
            //timer.Tick += MoveSnake;
            //timer.Start();
            
        }

       

        public int Points { get => points; set => points = value; }
        
        internal Snakee Snake { get => snake; set => snake = value; }

        public async Task<Direction> GetDirection()
        {
            
            await Task.Run(() =>
            {

                // Listening to AWSD down

            });
            return direction;
        }
        public async void MoveSnake()
        {
            while (true)
            {
                await Task.Run(() =>
                 {
                     Thread.Sleep(300);
                     Snake.Move(direction);

                 })
                 ;
                EventArgs e = new EventArgs();
                OnSnakeMoved(e);
            }
        }
    }
}
