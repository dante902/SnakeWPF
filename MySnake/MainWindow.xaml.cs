using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MySnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int x = 1;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        DirectionAfterKey d;
        List<Rectangle> snakeList = new List<Rectangle>();
        private Point foodPosition = new Point();

        public MainWindow()
        {
            
            InitializeComponent();
            snakeList.Add(SnakeHead);
            CreateFood(ref foodPosition);
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            EatFood();
            x++;
            label1.Content = x;

            var headPosition = GetHeadPosition(SnakeHead);

            if (CheckWallColision(headPosition))
            {
                Directions(d);
                
            }
            else
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Collision");
            }
            
           

        }
        public Point GetHeadPosition(UIElement element)
        {
            Point position = new Point();
            position.X = Canvas.GetLeft(element);
            position.Y = Canvas.GetBottom(element);
            return position;

        }
        private bool CheckWallColision(Point HeadPosition)
        {
            if (HeadPosition.X < 0 || HeadPosition.X > 241 || HeadPosition.Y < 0 || HeadPosition.Y > 241)
            {
                return false;
                
            }
            return true;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 60);
            dispatcherTimer.Start();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
        }

        private enum DirectionAfterKey { Up, Down, left, Right };
        
        
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Up:
                    Directions(Key.Up);
                    break;
                case Key.Down:
                    Directions(Key.Down);
                    break;
                case Key.Left:
                    Directions(Key.Left);
                    break;
                case Key.Right:
                    Directions(Key.Right);
                    break;
            };
                
        }
        private void CreateFood(ref Point foodPosition)
        {
            Random rand = new Random();
            foodPosition.X = rand.Next(24)*10;
            foodPosition.Y = rand.Next(24)*10;
            Rectangle rect = new Rectangle() { Height = 10, Width = 10, Fill = new SolidColorBrush(Colors.Green), HorizontalAlignment= System.Windows.HorizontalAlignment.Left, Stroke=new SolidColorBrush(Colors.Black), Uid="food" };
            Canvas.SetBottom(rect, foodPosition.Y);
            Canvas.SetLeft(rect, foodPosition.X);
            //Canvas.SetBottom(rect, 0);
            //Canvas.SetLeft(rect, 0);
            GamePlace.Children.Add(rect);
            
        }
        private void Directions(Key klawisz)
        {
            
            var headPosition = GetHeadPosition(SnakeHead);
            if (klawisz == Key.Up)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X);
                Canvas.SetBottom(SnakeHead, headPosition.Y + 10);
                GamePlace.UpdateLayout();
                d = DirectionAfterKey.Up;
            }
            else if (klawisz == Key.Down)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X);
                Canvas.SetBottom(SnakeHead, headPosition.Y - 10);
                GamePlace.UpdateLayout();
                d = DirectionAfterKey.Down;
            }
            else if (klawisz == Key.Left)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X - 10);
                Canvas.SetBottom(SnakeHead, headPosition.Y);
                GamePlace.UpdateLayout();
                d = DirectionAfterKey.left;
            }
            else if (klawisz == Key.Right)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X + 10);
                Canvas.SetBottom(SnakeHead, headPosition.Y);
                GamePlace.UpdateLayout();
                d = DirectionAfterKey.Right;
            }
            else
            {
                return;
            }
        }
        private void Directions(DirectionAfterKey dak)
        {
            var headPosition = GetHeadPosition(SnakeHead);
            if (dak == DirectionAfterKey.Up)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X);
                Canvas.SetBottom(SnakeHead, headPosition.Y + 10);
                GamePlace.UpdateLayout();
            }
            if (dak == DirectionAfterKey.Down)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X);
                Canvas.SetBottom(SnakeHead, headPosition.Y - 10);
                GamePlace.UpdateLayout();
            }
            if (dak == DirectionAfterKey.left)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X - 10);
                Canvas.SetBottom(SnakeHead, headPosition.Y);
                GamePlace.UpdateLayout();
            }
            if (dak == DirectionAfterKey.Right)
            {
                Canvas.SetLeft(SnakeHead, headPosition.X + 10);
                Canvas.SetBottom(SnakeHead, headPosition.Y);
                GamePlace.UpdateLayout();
            }
            else
            {
                return;
            }
        }
        
        private void EatFood()
        {
            if (isEaten())
            {
                foreach (UIElement element in GamePlace.Children)
                {
                    if (element.Uid == "food")
                    {
                        snakeList.Add((Rectangle)element);
                        element.Visibility = System.Windows.Visibility.Hidden;
                        element.Uid = "snake";
                        GamePlace.UpdateLayout();
                        CreateFood(ref foodPosition);
                        break;
                    }
                }
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
        private bool isEaten()
        {
            if (GetHeadPosition(SnakeHead).X == foodPosition.X && GetHeadPosition(SnakeHead).Y == foodPosition.Y)
            {
                return true;
            }
            return false;
        }
    }
}
