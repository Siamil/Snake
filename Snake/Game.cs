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
        bool isServer;
        private int points;
        Snakee snake;
        List<Snakee> snakes;
        Snakee multiSnake;
        Food food;
        bool multiEat;
        bool eat;
        bool gameover = false;
        //Direction direction = Direction.DOWN;
        //Direction multiDirection = Direction.LEFT;
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
            snake = new Snakee(1);
            food = new Food();
            random = new Random();
            MultiSnake = new Snakee(2);
            snakes = new List<Snakee>();
            snakes.Add(snake);
            snakes.Add(multiSnake);

            MoveSnake();
            GameOver(snake);
            GenerateFood();
            //timer.Interval = 300;
            //timer.Tick += MoveSnake;
            //timer.Start();

        }
        public bool GameOver(Snakee snake)
        {
            for (int i = 3; i < snake.NumOfBlocks; i++)
            {
               //// if (snake.GetBlock(0).Posx == snake.GetBlock(i).Posx && snake.GetBlock(0).Posy == snake.GetBlock(i).Posy && isServer)
                //    return true;
            }
            return false;
        }
        public bool IsFoodEaten(Snakee snake)
        {
            if (food.Posx == snake.GetBlock(0).Posx && food.Posy == snake.GetBlock(0).Posy)
            {
                return true;
            }
            else return false;
        }
        public void GenerateFood()
        {
            if (isServer)
            {
                food.Posx = random.Next(0, (int)Config.NumOfPositionsX);
                food.Posy = random.Next(0, (int)Config.NumOfPositionsY);
                EventArgs e = new EventArgs();
                OnFoodGenerated(e);
            }
        }
        public void OnKeyDownHandler(KeyEventArgs e)
        {
            switch (isServer)
            {
                case true:
                    {
                        switch (e.Key)
                        {
                            case Key.W:
                                if (snake.Direction == Direction.DOWN) break;
                                snake.Direction = Direction.UP;
                                break;

                            case Key.S:
                                if (snake.Direction == Direction.UP) break;
                                snake.Direction = Direction.DOWN;
                                break;
                            case Key.A:
                                if (snake.Direction == Direction.RIGHT) break;
                                snake.Direction = Direction.LEFT;
                                break;
                            case Key.D:
                                if (snake.Direction == Direction.LEFT) break;
                                snake.Direction = Direction.RIGHT;
                                break;
                        }
                        break;
                    }
                case false:
                    {
                        switch (e.Key)
                        {
                            case Key.W:
                                if (MultiSnake.Direction == Direction.DOWN) break;
                                MultiSnake.Direction = Direction.UP;
                                break;

                            case Key.S:
                                if (MultiSnake.Direction == Direction.UP) break;
                                MultiSnake.Direction = Direction.DOWN;
                                break;
                            case Key.A:
                                if (MultiSnake.Direction == Direction.RIGHT) break;
                                MultiSnake.Direction = Direction.LEFT;
                                break;
                            case Key.D:
                                if (MultiSnake.Direction == Direction.LEFT) break;
                                MultiSnake.Direction = Direction.RIGHT;
                                break;
                        }
                        break;
                    }



                default:
                    break;
            }
        }

        public int Points { get => points; set => points = value; }

        internal Snakee Snake { get => snake; set => snake = value; }
       // internal Direction Direction { get => direction; set => direction = value; }
        internal Food Food { get => food; set => food = value; }
        internal Snakee MultiSnake { get => multiSnake; set => multiSnake = value; }
       // internal Direction MultiDirection { get => multiDirection; set => multiDirection = value; }
        public bool IsServer { get => isServer; set => isServer = value; }

        public async void MoveSnake()
        {
            
            

            while (!gameover)
            {
                EventArgs e = new EventArgs();
                OnSnakeMoved(e);
                await Task.Run(() =>
                 {
                     Thread.Sleep(Config.Speed);
                     foreach (var snake in snakes)
                     {
                         if (snake.Posx == -1 && snake.Direction == Direction.LEFT) snake.GetBlock(0).Posx = (int)Config.NumOfPositionsX;
                         if (snake.Posy == -1) snake.GetBlock(0).Posy = (int)Config.NumOfPositionsY - 1;
                         if (snake.Posx == Config.NumOfPositionsX - 1 && snake.Direction == Direction.RIGHT) snake.GetBlock(0).Posx = -1;
                         if (snake.Posy == Config.NumOfPositionsY) snake.GetBlock(0).Posy = -1;
                         
                         gameover = GameOver(snake);
                         
                        
                     }
                     
                     
                     Snake.Move();
                     MultiSnake.Move();
                     multiEat = IsFoodEaten(multiSnake);
                     eat = IsFoodEaten(snake);

                 })
                 ;
                if (eat)
                {
                    GenerateFood();
                        snake.Eat();
                }
                if (multiEat)
                {
                    GenerateFood();
                    multiSnake.Eat();
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
