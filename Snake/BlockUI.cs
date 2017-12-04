using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace Snake
{
    class BlockUI 
    {
        Block block;
        Rectangle rect;
        public BlockUI() {
            Publisher();
        }
        public BlockUI(Block block)
        {
            this.block = block;
             Rect = new Rectangle();
            

        }
        private int width;
        private int heigt;
        

        public int Width { get => width; set => width = value; }
        public int Heigt { get => heigt; set => heigt = value; }
        public Rectangle Rect { get => rect; set => rect = value; }

        public void Draw(Canvas canvas)
        {  
            Rect.Width = canvas.ActualWidth * 0.1;
            Rect.Height = canvas.ActualHeight * 0.1;
            Rect.Fill = Brushes.Goldenrod;
            Rect.Stroke = Brushes.Black;    
            Canvas.SetLeft(Rect, ((block.Posx * 0.1)) * canvas.ActualWidth);
            Canvas.SetTop(Rect, (block.Posy * 0.1) * canvas.ActualHeight);
        }
        public void Publisher() { }
    }
}
