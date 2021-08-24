using BusinessLogic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SyntaxWpfApp
{
    /// <summary>
    /// FourthWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FourthWindow : Window
    {
        public FourthWindow()
        {
            InitializeComponent();

            Inits();
        }

        private void Inits()
        {
            List<Car> cars = new List<Car>();   // Car 리스트
            for (int i = 0; i < 10; i++)        // 10 개 데이터를 임의로 생성
            {
                cars.Add(new Car { 
                    Speed = i * 10 ,
                    Driver = new Human
                    {
                        FirstName = "Name" + i,
                        HasLicense = true
                    },
                    Color = Color.FromRgb(255,0,0)
                });
            }
            this.DataContext = cars; // 데이터를 XAML(화면쪽)으로 보내는 작업
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("버튼클릭");
        }
    }
}
