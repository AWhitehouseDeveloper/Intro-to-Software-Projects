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
using System.Globalization;

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

        static TextBlock[] dayBlocks = new TextBlock[45];

        static Month CurrentMonth = months[index];
        public Event[] events = new Event[25];


        public MainWindow()
        {
            InitializeComponent();

            Type t = typeof(System.Data.DataSet);
            string s = t.Assembly.FullName.ToString();
            Console.WriteLine("The fully qualified assembly name " +
                "containing the specified class is {0}.", s);

            //full weeks per line else it gets really long and looks bad
            dayBlocks[1] = sun1; dayBlocks[2] = mon1; dayBlocks[3] = tue1; dayBlocks[4] = wed1; dayBlocks[5] = thur1; dayBlocks[6] = fri1; dayBlocks[7] = sat1;
            dayBlocks[8] = sun2; dayBlocks[9] = mon2; dayBlocks[10] = tue2; dayBlocks[11] = wed2; dayBlocks[12] = thur2; dayBlocks[13] = fri2; dayBlocks[14] = sat2;
            dayBlocks[15] = sun3; dayBlocks[16] = mon3; dayBlocks[17] = tue3; dayBlocks[18] = wed3; dayBlocks[19] = thur3; dayBlocks[20] = fri3; dayBlocks[21] = sat3;
            dayBlocks[22] = sun4; dayBlocks[23] = mon4; dayBlocks[24] = tue4; dayBlocks[25] = wed4; dayBlocks[26] = thur4; dayBlocks[27] = fri4; dayBlocks[28] = sat4;
            dayBlocks[29] = sun5; dayBlocks[30] = mon5; dayBlocks[31] = tue5; dayBlocks[32] = wed5; dayBlocks[33] = thur5; dayBlocks[34] = fri5; dayBlocks[35] = sat5;
            //dayBlocks[36] = sun6; dayBlocks[37] = mon6; dayBlocks[38] = tue6; dayBlocks[39] = wed6; dayBlocks[40] = thur6; dayBlocks[41] = fri6; dayBlocks[42] = sat6;

            index = DateTime.Now.Month -1;
            YearDisplay.Text = DateTime.Now.Year.ToString();
            displayMonth();

        }

        //lemme pull github

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
                return $"Name: {Name} \nStart:{StartOfEvent} \nEnd:{EndOfEvent} \nDesc:{ Description } \nColor: {Color}";
            }

        }

        public void displayMonth()
        {
            CurrentMonth = months[index];
            MonthDisplay.Text = CurrentMonth.Name;

            DateTime date = DateTime.Parse(YearDisplay.Text.ToString() + "," + MonthDisplay.Text.ToString() + ",1");
            string day = date.DayOfWeek.ToString();
            int start = 0;

            if (day.ToLower().Equals("sunday"))
            {
                sun1.Text = "1";
            }
            else if (day.ToLower().Equals("monday"))
            {
                mon1.Text = "1";
                start = 1;
            }
            else if(day.ToLower().Equals("tuesday"))
            {
                tue1.Text = "1";
                start = 2;
            }
            else if (day.ToLower().Equals("wednesday"))
            {
                wed1.Text = "1";
                start = 3;
            }
            else if (day.ToLower().Equals("thursday"))
            {
                thur1.Text = "1";
                start = 4;
            }
            else if (day.ToLower().Equals("friday"))
            {
                fri1.Text = "1";
                start = 5;
            }
            else if (day.ToLower().Equals("saturday"))
            {
                sat1.Text = "1";
                start = 6;
            }

            int days = DateTime.DaysInMonth(date.Year, date.Month);

            for (int i = start + 1; i <= days + start - 7; i++)
            {
                string prevday = dayBlocks[i - 1].Text;
                int dayCount;
                int.TryParse(prevday, out dayCount);
                dayCount++;
                dayBlocks[i].Text = "" + dayCount;
            }
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
