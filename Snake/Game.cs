using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake
{
    class Game
    {
       private int points;
        Snakee snake;
        Food food;
        Direction direction = Direction.DOWN;
        //Timer timer = new Timer();
        public event EventHandler SnakeMoved;
        protected virtual void OnSnakeMoved(EventArgs e)
        {
            SnakeMoved?.Invoke(this, e);
        }
        public event EventHandler FoodGenerated;
        protected virtual void OnFoodGenerated(EventArgs e)
        {
            FoodGenerated?.Invoke(this, e);
        }

        public Game()
        {
            snake = new Snakee();
            Food = new Food();
            MoveSnake();
            GenerateFood();
            //timer.Interval = 300;
            //timer.Tick += MoveSnake;
            //timer.Start();
            
        }
         public bool IsFoodEaten()
        {
            if (Food.Posx == snake.Posx && Food.Posy == snake.Posy)
            {
                return true;
            }
            else return false;
        }
        public void GenerateFood()
        {
            Random random = new Random();
            Food.Posx = random.Next(1, 10);
            Food.Posy = random.Next(1, 10);
            EventArgs e = new EventArgs();
            OnFoodGenerated(e);
        }
        public void OnKeyDownHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: Direction = Direction.UP;
                    break;

                case Key.S: Direction = Direction.DOWN;
                    break;
                case Key.A: Direction = Direction.LEFT;
                    break;
                case Key.D: Direction = Direction.RIGHT;
                    break;

                default:
                    break;
            }
        }

        public int Points { get => points; set => points = value; }
        
        internal Snakee Snake { get => snake; set => snake = value; }
        internal Direction Direction { get => direction; set => direction = value; }
        internal Food Food { get => food; set => food = value; }

        //public async Task<Direction> GetDirection(KeyEventArgs e)
        //{
        //    Direction direction;
        //    await Task.Run(() =>
        //    {
        //        OnKeyDownHandler(e);


        //    });

        //}
        public async void MoveSnake()
        {
            bool eat = false;
            while (true)
            {
                await Task.Run(() =>
                 {
                     Thread.Sleep(500);
                     Snake.Move(Direction);
                     eat = IsFoodEaten();

                 })
                 ;
                if(eat)
                {
                    GenerateFood();
                    snake.Eat();
                }
                EventArgs e = new EventArgs();
                OnSnakeMoved(e);
            }
        }
    }
}
