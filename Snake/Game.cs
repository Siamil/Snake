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
        Random random;
        
        private int points;
        Snakee snake;
        Food food;
        bool gameover = false;
        Direction direction = Direction.DOWN;
        //Timer timer = new Timer();
        public event EventHandler SnakeMoved;
        protected virtual void OnSnakeMoved(EventArgs e)
        {
            
            SnakeMoved?.Invoke(this, e);
        }
        public event EventHandler GameEnded;
        protected virtual void OnGameEnded(EventArgs e)
        {

            GameEnded?.Invoke(this, e);
        }
        public event EventHandler FoodGenerated;
        protected virtual void OnFoodGenerated(EventArgs e)
        {
            FoodGenerated?.Invoke(this, e);
        }

        public Game()
        {
            snake = new Snakee();
            food = new Food();
            random = new Random();
            

            MoveSnake();
            GameOver();
            GenerateFood();
            //timer.Interval = 300;
            //timer.Tick += MoveSnake;
            //timer.Start();

        }
        public bool GameOver()
        {
            for (int i = 2; i < snake.NumOfBlocks; i++)
            {
                if (snake.GetBlock(0).Posx == snake.GetBlock(i).Posx && snake.GetBlock(0).Posy == snake.GetBlock(i).Posy) return true;
            }
            return false;
        }
        public bool IsFoodEaten()
        {
            if (food.Posx == snake.Posx && food.Posy == snake.Posy)
            {
                return true;
            }
            else return false;
        }
        public void GenerateFood()
        {

            food.Posx = random.Next(0, (int)Config.NumOfPositionsX);
            food.Posy = random.Next(0, (int)Config.NumOfPositionsY);
            for (int i = 0; i < snake.NumOfBlocks; i++)
            {
                if (Food.Posx == snake.GetBlock(i).Posx && Food.Posy == snake.GetBlock(i).Posy) GenerateFood();
            }

            EventArgs e = new EventArgs();
            OnFoodGenerated(e);
        }
        public void OnKeyDownHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if (Direction == Direction.DOWN) break;
                    Direction = Direction.UP;
                    break;

                case Key.S:
                    if (Direction == Direction.UP) break;
                    Direction = Direction.DOWN;
                    break;
                case Key.A:
                    if (Direction == Direction.RIGHT) break;
                    Direction = Direction.LEFT;
                    break;
                case Key.D:
                    if (Direction == Direction.LEFT) break;
                    Direction = Direction.RIGHT;
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
            
            while (!gameover)
            {
                EventArgs e = new EventArgs();
                OnSnakeMoved(e);
                await Task.Run(() =>
                 {
                     Thread.Sleep(Config.Speed);
                      if (snake.Posx == -1) snake.GetBlock(0).Posx = (int)Config.NumOfPositionsX;
                      if (snake.Posy == -1) snake.GetBlock(0).Posy = (int)Config.NumOfPositionsY - 1;
                      if (snake.Posx == Config.NumOfPositionsX - 1 && Direction == Direction.RIGHT) snake.GetBlock(0).Posx = -1;
                      if (snake.Posy == Config.NumOfPositionsY) snake.GetBlock(0).Posy = -1; 
                     Snake.Move(Direction);
                     gameover = GameOver();
                     eat = IsFoodEaten();
                 })
                 ;
                if (eat)
                {
                    GenerateFood();
                    snake.Eat();
                }
                if (gameover)
                {
                    EventArgs d = new EventArgs();
                    OnGameEnded(d);
                }
                
            }
        }
    }
}
