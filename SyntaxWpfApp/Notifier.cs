using System.ComponentModel;

namespace SyntaxWpfApp
{
    //Binding TwoWay일때 화면상에서 값이 바뀐걸 시스템이 알아차리고 대응하기 위해서(데이터 저장, 알림)
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
