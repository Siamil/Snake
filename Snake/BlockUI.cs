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
        public BlockUI() {
            Publisher();
        }
        public BlockUI(Block block)
        {
            this.block = block;
            
        }
        private int width;
        private int heigt;
        

        public int Width { get => width; set => width = value; }
        public int Heigt { get => heigt; set => heigt = value; }
        public void Draw(Canvas canvas)
        {
            Rectangle rect = new Rectangle();
            rect.Width = canvas.Width/10;
            rect.Height = canvas.Height/10;
            rect.Fill = Brushes.Goldenrod;
            rect.Stroke = Brushes.Black;
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, (double) ((block.Posx * 0.1)) * canvas.Width);
            Canvas.SetTop(rect, (block.Posy * 0.1) * canvas.Height);
        }
        public void Publisher() { }
    }
}
