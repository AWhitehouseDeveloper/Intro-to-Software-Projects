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
using System.Reflection;

namespace Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int index = 0;
        static Month January = new Month("January");
        static Month Febuary = new Month("Febuary");
        static Month March = new Month("March");
        static Month April = new Month("April");
        static Month May = new Month("May");
        static Month June = new Month("June");
        static Month July = new Month("July");
        static Month August = new Month("August");
        static Month September = new Month("September");
        static Month October = new Month("October");
        static Month November = new Month("November");
        static Month December = new Month("December");
        static readonly Month[] months = { January, Febuary, March, April, May, June, July, August, September, October, November, December };

        static Month CurrentMonth = months[index];
        public Event[] events = new Event[25];


        public MainWindow()
        {
            InitializeComponent();
            Type t = typeof(System.Data.DataSet);
            string s = t.Assembly.FullName.ToString();
            Console.WriteLine("The fully qualified assembly name " +
                "containing the specified class is {0}.", s);

            index = DateTime.Now.Month;
            YearDisplay.Text = DateTime.Now.Year.ToString();
            
        }


        public class Month
        {
            public Month() { }
            public Month(string name) { Name = name; }

            public string Name { get; set; }

        }

        public class Event
        {
            public Event() { }
            public Event(string name, DateTime start, DateTime end, string desc, Color color) { Name = name; StartOfEvent = start; EndOfEvent = end; Description = desc; Color = color; }

            public string Name { get; set; }
            public DateTime StartOfEvent { get; set; }
            public DateTime EndOfEvent { get; set; }
            public string Description { get; set; }
            public Color Color { get; set; }

            public string ToString()
            {
                return $"Name: {Name} Start:{StartOfEvent} End:{EndOfEvent} Desc:{ Description } Color: {Color}";
            }

        }

        public void displayMonth()
        {
            CurrentMonth = months[index];
            MonthDisplay.Text = CurrentMonth.Name;

            DateTime date = DateTime.Parse(YearDisplay.Text.ToString() + "," + MonthDisplay.Text.ToString() + ",1");
            string day = date.DayOfWeek.ToString();
        }
        
        private void prevMonth_Click(object sender, RoutedEventArgs e)
        {
            index--;
            if (index < 0)
            {
                index = months.Length - 1;
                int year = int.Parse(YearDisplay.Text.ToString()) - 1;
                YearDisplay.Text = year.ToString();
            }

            displayMonth();
        }
        
        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            index++;
            if (index + 1 > months.Length)
            {
                index = 0;
                int year = int.Parse(YearDisplay.Text.ToString()) + 1;
                YearDisplay.Text = year.ToString();
            }

            displayMonth();
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < events.Length; i++)
            {
                if (events[i] == null)
                {
                    events[i] = (new Event("TestEvent", DateTime.Today, DateTime.Now, "Just testing the button click", Color.FromRgb(10, 40, 200)));
                    break;
                }
            }
            Console.WriteLine(events.ToString());
        }
    }
}
