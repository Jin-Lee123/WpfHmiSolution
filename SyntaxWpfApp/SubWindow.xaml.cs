using MahApps.Metro.Controls;
using System.Windows;

namespace SyntaxWpfApp
{
    /// <summary>
    /// SubWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SubWindow : MetroWindow
    {
        public SubWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThirdWindow win = new ThirdWindow();
            win.Show();
        }

        private void BtnFourth_Click(object sender, RoutedEventArgs e)
        {
            FourthWindow win = new FourthWindow();
            win.Show();
        }

        private void BtnFourth_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}