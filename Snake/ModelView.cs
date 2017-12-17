using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Windows.Input;

namespace Snake
{
    class ModelView : INotifyPropertyChanged
    {
        Game game;
        SnakeUI snakeUI;
        FoodUI foodUI;
        SnakeUI multiSnakeUI;
        Canvas canvas;
        Label lPoints;
        

        public ModelView(Canvas canvas)
        {
            
            this.canvas = canvas;
            game = new Game();
            foodUI = new FoodUI(game.Food, canvas);
            game.SnakeMoved += Draw;
            game.FoodGenerated += Draw;
            game.GameEnded += GameEnded;
            game.MultiGameEnded += MultiGameEnded;
            snakeUI = new SnakeUI(canvas, game.Snake);
            multiSnakeUI = new SnakeUI(canvas, game.MultiSnake);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand bConnect { get { return new RelayCommand(bConnect_Click); } }
        public ICommand bServer { get { return new RelayCommand(bServer_Click); } }

        internal Game Game { get => game; set => game = value; }

        public void Draw(object sender, EventArgs e)
        {
           
            foodUI.Draw(canvas);
            snakeUI.Draw(canvas);
            multiSnakeUI.Draw(canvas);
            //lPoints.Content = game.Points.ToString();
        }

        public void GameEnded(object sender, EventArgs e)
        {
            MessageBox.Show("Client Snake Won");
        }
        public void MultiGameEnded(object sender, EventArgs e)
        {
            MessageBox.Show("Server Snake Won");
        }
        private void bServer_Click()
        {
            game.IsServer = true;
            Server server = new Server(ref game);
            server.waitConnection();
        }

        private void bConnect_Click()
        {
            game.IsServer = false;
            Client client = new Client(ref game);
            client.receiveBytes();
            client.sendBytes();
            game.StartGame();
            // client.receiveBytes();


        }
    }
}
