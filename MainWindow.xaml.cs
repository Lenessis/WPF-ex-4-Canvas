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
//using System.Drawing;
//using Rectangle = System.Drawing.Rectangle;

namespace zad4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Point startPosition, endPosition;
        Rectangle lastRect;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitRectangle(object sender, MouseButtonEventArgs e) //Canvas - MouseLeftButtonDown
        {
            Mouse.Capture(canvas); // przechwytywanie myszy poza obszarem canvy
            startPosition = e.GetPosition(canvas);

            Rectangle box = new Rectangle
            {
                Width = 0,
                Height = 0,
                Fill = Brushes.Blue,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            box.SetCurrentValue(Canvas.TopProperty, startPosition.Y);
            box.SetCurrentValue(Canvas.LeftProperty, startPosition.X);

            lastRect = box; //ostatnio stworzony prostokąt jest zapisany do zmiennej tymczasowej
            canvas.Children.Add(box); // prostokąt jest dodany do canvy            

        }

        private void CreatingRectangle(object sender, MouseEventArgs e) // Canvas - MoveMouse
        {
            
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                int lastRectIndex = canvas.Children.IndexOf(lastRect); // sprawdz index
                Rectangle temp = (Rectangle)canvas.Children[lastRectIndex]; // GET

                endPosition = e.GetPosition(canvas);


                temp.Width = Math.Abs(endPosition.X - startPosition.X);
                temp.Height = Math.Abs(endPosition.Y - startPosition.Y);

                temp.SetValue(Canvas.LeftProperty, Math.Min(startPosition.X, endPosition.X));
                temp.SetValue(Canvas.TopProperty, Math.Min(startPosition.Y, endPosition.Y));

                lastRect = temp;
                canvas.Children[lastRectIndex] = lastRect; //SET

            }

            // Rectangle x = (Rectangle)canvas.Children[canvas.Children.Count - 1 ]; //dziala GET
            //canvas.Children[canvas.Children.Count - 1] = x; // dziala SET
            // box.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0)); //zmiana wypełnienia prostokąta

        }

        private void EndRectangle(object sender, MouseButtonEventArgs e) // Canvas - MouseLeftButtonUp
        {           
            int lastRectIndex = canvas.Children.IndexOf(lastRect); // sprawdz index
            Rectangle temp = (Rectangle)canvas.Children[lastRectIndex]; // GET

            endPosition = e.GetPosition(canvas);


            temp.Width = Math.Abs(endPosition.X - startPosition.X);
            temp.Height = Math.Abs(endPosition.Y - startPosition.Y);

            temp.SetValue(Canvas.LeftProperty, Math.Min(startPosition.X, endPosition.X));
            temp.SetValue(Canvas.TopProperty, Math.Min(startPosition.Y, endPosition.Y));

            lastRect = temp;
            canvas.Children[lastRectIndex] = lastRect; //SET

            Mouse.Capture(null); // zwalnianie przechwytywania myszy
        }


        private void MoveRectangleKeyDown(object sender, KeyEventArgs e) // Window - KeyDown
        {
            int lastRectIndex = canvas.Children.IndexOf(lastRect); // sprawdz index
            Rectangle temp = (Rectangle)canvas.Children[lastRectIndex]; // GET

            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        if(temp.Height-5>0)
                            temp.Height-= 5;
                        else if(temp.Height>0 && temp.Height<=5)
                            temp.Height -= 1;
                        break;
                    case Key.Down:
                        temp.Height += 5;
                        break;
                    case Key.Right:
                        temp.Width += 5;
                        break;
                    case Key.Left:
                        if(temp.Width-5>0)
                            temp.Width -= 5;
                        else if (temp.Width > 0 && temp.Width <= 5)
                            temp.Width -= 1;
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        temp.SetValue(Canvas.TopProperty, (double)temp.GetValue(Canvas.TopProperty) - 5);
                        break;
                    case Key.Down:
                        temp.SetCurrentValue(Canvas.TopProperty, (double)temp.GetValue(Canvas.TopProperty) + 5);
                        break;
                    case Key.Right:
                        temp.SetCurrentValue(Canvas.LeftProperty, (double)temp.GetValue(Canvas.LeftProperty) + 5);
                        break;
                    case Key.Left:
                        temp.SetCurrentValue(Canvas.LeftProperty, (double)temp.GetValue(Canvas.LeftProperty) - 5);
                        break;
                }
            }

            lastRect = temp;
            canvas.Children[lastRectIndex] = lastRect;

        }

    }
}
