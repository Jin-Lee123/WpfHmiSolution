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

namespace BaseControlApp
{
    /// <summary>
    /// UcCircleButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UcCircleButton : UserControl
    {
        // Step1, RoutedEvent 정의
        public static readonly RoutedEvent CustomClickEvent =
            EventManager.RegisterRoutedEvent("CustomClick", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(UcCircleButton));

        // Step2, RoutedEventHandler 선언
        public event RoutedEventHandler CustomClick
        {
            add { AddHandler(CustomClickEvent, value); } //+=
            remove { RemoveHandler(CustomClickEvent, value); }  // -= eventHandler
        }
        public UcCircleButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Step3, 
            RaiseEvent(new RoutedEventArgs(CustomClickEvent, sender));
        }
    }
}
