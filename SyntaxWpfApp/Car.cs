using System.Windows.Media;

namespace BusinessLogic
{
    public class Car
    {
        public double Speed { get; set; }
        public Color Color { get; set; }
        public Human Driver { get; set; }
    }
    public class Human
    {
        public string FirstName { get; set; }
        public bool HasLicense { get; set; }
    }
}
