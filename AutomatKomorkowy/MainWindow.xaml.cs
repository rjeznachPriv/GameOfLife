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
using Automat.Logic;
using Automat.Logic.Calculators;
using Newtonsoft.Json.Linq;

namespace AutomatKomorkowy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;

        private bool _proceedWithGameLoop = false;

        private static int CANVAS_FIELD_SIZE = 10;

        private void GameAdvancedBy1Step(object sender, EventArgs e)
        {
            UpdateCanvas(_game);
        }

        public MainWindow()
        {
            InitializeComponent();

            _game = new Game(20, new ConwayCalculator());

            PrepareCanvas(_game);
            UpdateCanvas(_game);

            Game.GameAdvancedBy1Step += GameAdvancedBy1Step;
        }

        private void UpdateCanvas(Game game)
        {
            Canvas.Children.Clear();

            for (var y = 0; y < game.Height; y++)
            {
                for (var x = 0; x < game.Width; x++)
                {
                    if (game.Board[x, y] == 1)
                        PutRectangle(x, y, Colors.Black);
                    else
                        PutRectangle(x, y, Colors.White);
                }
            }
        }

        private void UpdateCanvasWithCustomColors(Game game)
        {
            Canvas.Children.Clear();

            for (var y = 0; y < game.Height; y++)
            {
                for (var x = 0; x < game.Width; x++)
                {
                    //var a = Color.From
                    //PutRectangle(x,y, Color. game.Board[x,y]);
                }
            }
        }

        private void PrepareCanvas(Game game)
        {
            Canvas.Background = Brushes.LightSteelBlue;

            Canvas.Width = CANVAS_FIELD_SIZE * game.Width;
            Canvas.Height = CANVAS_FIELD_SIZE * game.Height;
        }

        private void PutRectangle(int x, int y, Color color)
        {
            Rectangle r = new Rectangle();
            r.Width = CANVAS_FIELD_SIZE;
            r.Height = CANVAS_FIELD_SIZE;
            SolidColorBrush myBrush = new SolidColorBrush(color);
            r.Fill = myBrush;

            Canvas.Children.Insert(0, r);

            Canvas.SetTop(r, y * CANVAS_FIELD_SIZE);
            Canvas.SetLeft(r, x * CANVAS_FIELD_SIZE);
        }

        private Point GetClickedCellCoordinates(MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(Canvas);

            var x = (int)p.X / CANVAS_FIELD_SIZE;
            var y = (int)p.Y / CANVAS_FIELD_SIZE;

            return new Point(x, y);
        }

        private Point GetHoveredCellCoordinates(MouseEventArgs e)
        {
            Point p = e.GetPosition(Canvas);

            var x = (int)p.X / CANVAS_FIELD_SIZE;
            var y = (int)p.Y / CANVAS_FIELD_SIZE;

            return new Point(x, y);
        }

        private void Step_click(object sender, RoutedEventArgs e)
        {
            _game.NextStep();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var coordinates = GetClickedCellCoordinates(e);

            _game.ShiftConwayField(new Tuple<int, int>((int)coordinates.X, (int)coordinates.Y));
            UpdateCanvas(_game);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed) {
                var coordinates = GetHoveredCellCoordinates(e);
                _game.ShiftConwayField(new Tuple<int, int>((int)coordinates.X, (int)coordinates.Y));
                UpdateCanvas(_game);
            }
        }

        private async void Start_click(object sender, RoutedEventArgs e)
        {
            _proceedWithGameLoop = true;

            while(_proceedWithGameLoop)
            {
                _game.NextStep();
                await Task.Delay(200);
            }
        }

        private void Sop_click(object sender, RoutedEventArgs e)
        {
            _proceedWithGameLoop = false;
        }

        private void Generate_Conway_40_40(object sender, RoutedEventArgs e)
        {
            _game = new Game(40, new ConwayCalculator());

            PrepareCanvas(_game);
            UpdateCanvas(_game);
        }

        private void Generate_Conway_20_20(object sender, RoutedEventArgs e)
        {
            _game = new Game(20, new ConwayCalculator());

            PrepareCanvas(_game);
            UpdateCanvas(_game);
        }

        private void Generate_Bigger_20_20(object sender, RoutedEventArgs e)
        {
            _game = new Game(20, new BiggerCalculator());

            PrepareCanvas(_game);
            UpdateCanvas(_game);
        }

        private void Generate_Colorful_40_40(object sender, RoutedEventArgs e)
        {
            _game = new Game(40, new ColorfulCalculator());

            PrepareCanvas(_game);
            UpdateCanvas(_game);
        }
    }
}
