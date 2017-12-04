using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        SnakeUI snakeUI;
        FoodUI foodUI;
        

        public MainWindow()
        {
            InitializeComponent();
            canvas.Height = this.Height;
            canvas.Width = this.Width * 0.7;
            game = new Game();
            foodUI = new FoodUI(game.Food, canvas);
            game.SnakeMoved += Draw;
            game.FoodGenerated += Draw;
            game.GameEnded += GameEnded;
            snakeUI = new SnakeUI(canvas, game.Snake);
        }
        
        public void Draw(object sender, EventArgs e)
        {
            canvas.Height = this.ActualHeight;
            canvas.Width = this.ActualWidth;
            foodUI.Draw(canvas);
            snakeUI.Draw(canvas);
        }

        public void GameEnded(object sender, EventArgs e)
        {
            MessageBox.Show("Game Over");
        }
        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            game.OnKeyDownHandler(e);
           // MessageBox.Show("xd");
        }
    }
}
