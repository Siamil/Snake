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
namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelView modelview;
        public MainWindow()
        {
            InitializeComponent();
            modelview = new ModelView(canvas);
            this.DataContext = modelview;
        }
        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            modelview.Game.OnKeyDownHandler(e);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {   
            canvas.Width = this.ActualWidth*0.7;
            canvas.Height = this.ActualHeight*0.7;
        }

       
    }
}
