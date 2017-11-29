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
        

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            game.SnakeMoved += Draw;
            snakeUI = new SnakeUI(game.Snake);
            
            
            
        }
        
        public void Draw(object sender, EventArgs e)
        {
            
            snakeUI.Draw(canvas);
        }
    }
}
