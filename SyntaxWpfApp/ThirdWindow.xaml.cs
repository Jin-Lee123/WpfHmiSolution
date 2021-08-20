using BusinessLogic;
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
using System.Windows.Shapes;

namespace SyntaxWpfApp
{
    /// <summary>
    /// ThirdWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public ThirdWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TxtTest.Text = "Meeting!";
            //TxtTest.FontSize = 10;
            //TxtTest.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));

            // Car
            Car c = new Car();
            c.Speed = 200;
            c.Color = Colors.White;
            c.Driver = null;

            TxtTest.DataContext = c;
            //TxtTest.Text = c.Speed.ToString();
            StpCar.DataContext = c;
        }
    }
}
