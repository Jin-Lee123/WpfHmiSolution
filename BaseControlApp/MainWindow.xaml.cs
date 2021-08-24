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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaseControlApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnPush1_CustomClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Push 버튼 1 클릭!");
        }

        private void BtnPush2_CustomClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Push 버튼 2 클릭!");
        }

        private void BtnPush3_CustomClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Push 버튼 3 클릭!");
        }

        private void BtnAnimate_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;    //0도
            da.To = 360;    //360도
            da.Duration = new Duration(TimeSpan.FromSeconds(3)); //3초 동안
            //da.RepeatBehavior = RepeatBehavior.Forever;

            RotateTransform rt = new RotateTransform();
            rt.CenterX = RectOne.Width / 2;
            rt.CenterY = RectOne.Height / 2;
            RectOne.RenderTransform = rt;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }
    }
}
