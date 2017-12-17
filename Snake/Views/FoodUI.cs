using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    class FoodUI
    {
        Food food;
        Rectangle Rect = new Rectangle();

        public FoodUI(Food food, Canvas canvas)
        {
            this.Food = food;
            canvas.Children.Add(Rect);
        }
        public void Draw(Canvas canvas)
        {
            Rect.Width = canvas.ActualWidth / Config.NumOfPositionsX;
            Rect.Height = canvas.ActualHeight / Config.NumOfPositionsY;
            Rect.Fill = Brushes.Red;
            Canvas.SetLeft(Rect, ((food.Posx / Config.NumOfPositionsX)) * canvas.Width);
            Canvas.SetTop(Rect, (food.Posy / Config.NumOfPositionsY) * canvas.Height);

        }

        internal Food Food { get => food; set => food = value; }
        public Rectangle Rect1 { get => Rect; set => Rect = value; }
    }
}
