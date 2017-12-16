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
       // bool gameover = false;
      //  bool Multigameover = false;
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
        public event EventHandler MultiGameEnded;
        protected virtual void OnMultiGameEnded(EventArgs e)
        {

            MultiGameEnded?.Invoke(this, e);
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

           // MoveSnake();
            //GameOver(snake);
           // GenerateFood();

        }
        public bool GameOver(Snakee snake, Snakee snakeM)
        {
            for (int i = 4; i < snake.NumOfBlocks - 1; i++)
            {
                Block tempBlock = snake.GetBlock(0);
                if (tempBlock.Posx == snake.GetBlock(i).Posx && tempBlock.Posy == snake.GetBlock(i).Posy)
                   
                {
                    return true;
                }
            }

            for (int i = 0; i < snakeM.NumOfBlocks - 1; i++)
            {
                Block tempBlock = snake.GetBlock(0);
                if (tempBlock.Posx == snakeM.GetBlock(i).Posx && tempBlock.Posy == snakeM.GetBlock(i).Posy)
                   return true;
                
            }
            return false;
        }
        public void StartGame()
        {
            MoveSnake();
           //GameOver(snake, multiSnake);
           //GameOver(multiSnake, Snake);
            GenerateFood();
        }
        public bool IsFoodEaten(Snakee snake)
        {
            if (food.Posx == snake.GetBlock(0).Posx && food.Posy == snake.GetBlock(0).Posy)
            {
                Points += 100;
                return true;
            }
            else return false;
        }
        public void GenerateFood()
        {
            if (isServer)
            {
                food.Posx = random.Next(0, (int)Config.NumOfPositionsX);
                food.Posy = random.Next(0, (int)Config.NumOfPositionsY - 1);
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
        public bool MultiEat { get => multiEat; set => multiEat = value; }
        public bool Eat { get => eat; set => eat = value; }

        public async void MoveSnake()
        {
            
            

            while (true)
            {
                EventArgs e = new EventArgs();
                OnSnakeMoved(e);
                await Task.Run(() =>
                 {
                     Thread.Sleep(Config.Speed);
                     if (IsServer)
                     {
                         if (snake.Posx == -1 && snake.Direction == Direction.LEFT) snake.GetBlock(0).Posx = (int)Config.NumOfPositionsX;
                         if (snake.Posy == -1) snake.GetBlock(0).Posy = (int)Config.NumOfPositionsY - 1;
                         if (snake.Posx == Config.NumOfPositionsX - 1 && snake.Direction == Direction.RIGHT) snake.GetBlock(0).Posx = -1;
                         if (snake.Posy == Config.NumOfPositionsY) snake.GetBlock(0).Posy = -1;
                     }
                     else
                     {
                         if (MultiSnake.Posx == -1 && MultiSnake.Direction == Direction.LEFT) MultiSnake.GetBlock(0).Posx = (int)Config.NumOfPositionsX;
                         if (MultiSnake.Posy == -1) MultiSnake.GetBlock(0).Posy = (int)Config.NumOfPositionsY - 1;
                         if (MultiSnake.Posx == Config.NumOfPositionsX - 1 && MultiSnake.Direction == Direction.RIGHT) MultiSnake.GetBlock(0).Posx = -1;
                         if (MultiSnake.Posy == Config.NumOfPositionsY) MultiSnake.GetBlock(0).Posy = -1;
                     }
                     

                 })
                 ;
                Snake.Move();
                MultiSnake.Move();
             // gameover = GameOver(snake, MultiSnake);
            //  Multigameover = GameOver(MultiSnake, snake);
                
               // MultiEat = IsFoodEaten(multiSnake);
               // Eat = IsFoodEaten(snake);
                if (IsFoodEaten(snake))
                {
                    
                        snake.Eat();
                    GenerateFood();
                }
                if (IsFoodEaten(multiSnake))
                {
                    
                    multiSnake.Eat();
                    GenerateFood();
                }
                if (GameOver(snake, MultiSnake))
                {
                    EventArgs d = new EventArgs();
                   // OnGameEnded(d);
                    //break;
                }
                if (GameOver(MultiSnake, snake))
                {
                    EventArgs f = new EventArgs();
                   // OnMultiGameEnded(f);
                   // break;
                }

            }
        }
    }
}
