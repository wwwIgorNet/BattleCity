using Microsoft.Practices.ServiceLocation;
using SuperTankWPF.Model;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for ScrenConstruction.xaml
    /// </summary>
    public partial class ScrenConstruction : UserControl
    {
        private Timer timer = new Timer(100);
        private Action Elapsed;

        private IImageSourceStor imgStor = ServiceLocator.Current.GetInstance<IImageSourceStor>();

        public bool left;
        public bool right;
        public bool up;
        public bool down;
        public bool space;
        public bool enter;
        
        private char spaceChar = ' ';
        private char currentChar = ' ';
        private char[,] map;
        private int col;
        private int row;

        private ElementMap[,] elements;

        public ScrenConstruction()
        {
            InitializeComponent();

            Binding bindingMap = new Binding("Map");
            bindingMap.Source = board.DataContext;
            bindingMap.Mode = BindingMode.TwoWay;
            this.SetBinding(ScrenConstruction.MapProperty, bindingMap);

            timer.Elapsed += Timer_Elapsed;
            Elapsed = new Action(Update);

            KeyDown += board_KeyDown;
            KeyUp += board_KeyUp;

            this.IsVisibleChanged += ScrenConstruction_IsVisibleChanged;

            col = ConfigurationWPF.WidthBoard / ConfigurationWPF.WidthTile;
            row = ConfigurationWPF.HeightBoard / ConfigurationWPF.HeightTile;
            map = new char[col, row];

            elements = new ElementMap[col, row];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    ElementMap em = new ElementMap();
                    em.SetValue(Canvas.LeftProperty, (double)i * ConfigurationWPF.WidthTile);
                    em.SetValue(Canvas.TopProperty, (double)j * ConfigurationWPF.HeightTile);
                    elements[i, j] = em;
                    board.Children.Add(elements[i, j]);
                }
            }
        }

        #region DependencyProperty
        public char[,] Map
        {
            get { return (char[,])GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }
        
        public static readonly DependencyProperty MapProperty =
            DependencyProperty.Register("Map", typeof(char[,]), typeof(ScrenConstruction), new PropertyMetadata(null));

        public double TankPosX
        {
            get { return (double)GetValue(TankPosXProperty); }
            set { SetValue(TankPosXProperty, value); }
        }
        
        public static readonly DependencyProperty TankPosXProperty =
            DependencyProperty.Register("TankPosX", typeof(double), typeof(ScrenConstruction), new PropertyMetadata(0.0, TankPosXChangedCallback));

        private static void TankPosXChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScrenConstruction)d).tankCursor.SetValue(Canvas.LeftProperty, e.NewValue);
        }

        public double TankPosY
        {
            get { return (double)GetValue(TankPosYProperty); }
            set { SetValue(TankPosYProperty, value); }
        }

        public static readonly DependencyProperty TankPosYProperty =
            DependencyProperty.Register("TankPosY", typeof(double), typeof(ScrenConstruction), new PropertyMetadata(0.0, TankPosYChangedCallback));

        private static void TankPosYChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScrenConstruction)d).tankCursor.SetValue(Canvas.TopProperty, e.NewValue);
        }
        #endregion

        private void Update()
        {
            if (left)
            {
                if (TankPosX >= ConfigurationWPF.WidthTank)
                    TankPosX -= ConfigurationWPF.WidthTank;
            }
            else if (right)
            {
                if (TankPosX < ConfigurationWPF.WidthBoard - ConfigurationWPF.WidthTank)
                    TankPosX += ConfigurationWPF.WidthTank;
            }
            else if (up)
            {
                if (TankPosY >= ConfigurationWPF.HeightTank)
                    TankPosY -= ConfigurationWPF.HeightTank;
            }
            else if (down)
            {
                if (TankPosY < ConfigurationWPF.HeightBoard - ConfigurationWPF.HeightTank)
                    TankPosY += ConfigurationWPF.HeightTank;
            }

            if (space)
            {
                ChangType((int)(TankPosX / ConfigurationWPF.WidthTile), (int)(TankPosY / ConfigurationWPF.HeightTile));
            }
        }

        private void ChangType(int x, int y)
        {
            for (int i = x; i < 2 + x; i++)
            {
                for (int j = y; j < 2 + y; j++)
                {
                    ElementMap em = elements[i, j];
                    em.Source = GetImg(currentChar);
                    em.Type = currentChar;
                }
            }
        }

        private ImageSource GetImg(char c)
        {
            if (c == ConfigurationWPF.CharBrickWall)
            {
                return imgStor.BrickWall;
            }
            else if (c == ConfigurationWPF.CharConcreteWall)
            {
                return imgStor.ConcreteWall;
            }
            else if (c == ConfigurationWPF.CharWater)
            {
                return imgStor.Water_1;
            }
            else if (c == ConfigurationWPF.CharForest)
            {
                return imgStor.Forest;
            }
            else if (c == ConfigurationWPF.CharIce)
            {
                return imgStor.Ice;
            }
            return null;
        }

        private void NextChar()
        {
            if (currentChar == spaceChar)
            {
                currentChar = ConfigurationWPF.CharBrickWall;
            }
            else if (currentChar == ConfigurationWPF.CharBrickWall)
            {
                currentChar = ConfigurationWPF.CharConcreteWall;
            }
            else if (currentChar == ConfigurationWPF.CharConcreteWall)
            {
                currentChar = ConfigurationWPF.CharForest;
            }
            else if (currentChar == ConfigurationWPF.CharForest)
            {
                currentChar = ConfigurationWPF.CharIce;
            }
            else if (currentChar == ConfigurationWPF.CharIce)
            {
                currentChar = ConfigurationWPF.CharWater;
            }
            else if (currentChar == ConfigurationWPF.CharWater)
            {
                currentChar = spaceChar;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(Elapsed);
        }

        private void ScrenConstruction_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                this.Focus();
                timer.Start();
            }
            else
                timer.Stop();
        }

        private void board_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    left = true;
                    break;
                case Key.Right:
                    right = true;
                    break;
                case Key.Up:
                    up = true;
                    break;
                case Key.Down:
                    down = true;
                    break;
                case Key.Space:
                    space = true;
                    break;
                case Key.Enter:
                    enter = true;
                    break;
            }
        }

        private void board_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    left = false;
                    break;
                case Key.Right:
                    right = false;
                    break;
                case Key.Up:
                    up = false;
                    break;
                case Key.Down:
                    down = false;
                    break;
                case Key.Space:
                    space = false;
                    break;
                case Key.Enter:
                    enter = false;
                    NextChar();
                    break;
                case Key.Escape:
                    InitMap();
                    e.Handled = true;
                    this.Visibility = Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainViewModel>().StartScrenVisibility = Visibility.Visible;
                    break;
            }
        }

        private void InitMap()
        {
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    map[i, j] = elements[i, j].Type;
                }
            }

            Map = map;
        }

        class ElementMap : Image
        {
            public char Type
            {
                get { return (char)GetValue(TypeProperty); }
                set { SetValue(TypeProperty, value); }
            }

            public static readonly DependencyProperty TypeProperty =
                DependencyProperty.Register("Type", typeof(char), typeof(ElementMap), new PropertyMetadata(' '));
        }
    }

}
