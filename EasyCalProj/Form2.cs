using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using EasyCalProj.EasyCalDB;

namespace EasyCalProj
{
    public partial class Form2 : Form
    {

        public static string EventName = "";
        public static String month = DateTime.Now.Month.ToString();
        public static String year = DateTime.Now.Year.ToString();
        List<ListBox> allDays = new List<ListBox>();
        List<Tuple<String, int>> lessons = new List<Tuple<string, int>>();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /*private void button6_Click_2(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells != null && dataGridView1.SelectedCells.Count == 1)
            {
                int item;
                int.TryParse(dataGridView1.CurrentRow.Cells["SerialNumber"].Value.ToString(), out item);

                var eventList = GetEventList();

                var itemToRemove = eventList.FirstOrDefault(p => p.SerialNumber == item);

                using (var EasyCal = new EasyCalEntities1())
                {
                    EasyCal.Events1.Attach(itemToRemove);
                   EasyCal.Events1.Remove(itemToRemove);
                   EasyCal.SaveChanges();
                }
                dataGridView1.DataSource = GetEventList();
            }

        }

*/
        private void הוספתאירוערגילToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void הוספתמטלהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void אירועיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void מטלותToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        public IEnumerable<EasyCalProj.EasyCalDB.Tasks1> GetTaskList()
        {
            var TaskList = new List<EasyCalProj.EasyCalDB.Tasks1>();

            using (var EasyCal = new EasyCalEntities1())
            {
                TaskList = EasyCal.Tasks1.ToList();
            }
            return TaskList;
        }

        public IEnumerable<EasyCalProj.EasyCalDB.Activity> GetActivityList()
        {
            var ActivityList = new List<EasyCalProj.EasyCalDB.Activity>();

            using (var EasyCal = new EasyCalEntities1())
            {
                ActivityList = EasyCal.Activities.ToList();
            }
            return ActivityList;
        }

        public IEnumerable<EasyCalProj.EasyCalDB.TimeTable> GetTimeTableList()
        {
            var timeTable = new List<EasyCalProj.EasyCalDB.TimeTable>();

            using (var EasyCal = new EasyCalEntities1())
            {
                timeTable = EasyCal.TimeTables.ToList();
            }
            return timeTable;
        }

        public void clearListBoxes(List<ListBox> days)
        {
            for (int i = 0; i < days.Count; i++)
            {
                days.ElementAt(i).Items.Clear();
            }
        }

        public void fillListBoxes (List<ListBox> days,Dictionary<String,String> allEvents)
        {
            for (int i = 0; i < allEvents.Count; i++)
            {
                days.ElementAt(Convert.ToInt32(allEvents.ElementAt(i).Value) - 1).Items.Add(allEvents.ElementAt(i).Key);
            }
        }

        public void fillListBoxes(List<ListBox> days, List<Tuple<String,int>> lessons)
        {
            for(int i = 0; i<lessons.Count; i++)
            {
                days.ElementAt(lessons.ElementAt(i).Item2-1).Items.Add(lessons.ElementAt(i).Item1);
            }
        }

        public String getDayFromDate(String newDate)
        {
            String newDay = "";
            int k = 0;
            while (newDate.ElementAt(k) != '/')
            {
                if (newDate.ElementAt(k) == '0')
                {
                    k++;
                    continue;
                }
                else
                {
                    newDay += newDate.ElementAt(k);
                    k++;
                }
            }
            return newDay;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //label16.Parent = day15;
            //day15.Parent = Form2.DefaultBackColor;
            this.Text = "EasyCal Welcome " + Form1.AccountName;
            label34.Text = Form1.AccountName;
            var eventList = GetEventList();
            var taskList = GetTaskList();
            var activityList = GetActivityList();
            var timeTable = GetTimeTableList();
            allDays = new List<ListBox>{day1,day2,day3,day4,day5,day6,day7,day8,day9,day10,day11,day12,day13,day14,day15,day16,day17,day18,day19,day20,day21,day22,day23,day24,day25,day26,day27,day28,day29,day30,day31};
            Dictionary<String, String> allEvents = new Dictionary<String, String>();
            var eventsToRemove = eventList.Where(p => p.Date < new DateTime(Convert.ToInt32(year),Convert.ToInt32(month),DateTime.Now.Day)).ToList();
            using (var EasyCal = new EasyCalEntities1())
            {
                for (int i = 0; i < eventsToRemove.Count; i++)
                {
                    EasyCal.Events1.Attach(eventsToRemove.ElementAt(i));
                    EasyCal.Events1.Remove(eventsToRemove.ElementAt(i));
                    EasyCal.SaveChanges();
                }
            }
            RightAmountOfDays(day29,day30,day31,label30,label31,label32);
            eventList = eventList.Where(p => p.Date <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) && p.Date >= DateTime.Now.Date).ToList();
            taskList = taskList.Where(p => p.Date <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) && p.Date >= DateTime.Now.Date).ToList();
            activityList = activityList.Where(p => p.Date <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) && p.Date >= DateTime.Now.Date).ToList();
            for (int i = 0; i < eventList.Count(); i++)
            {
                var nextEvent = eventList.ElementAt(i);
                String newDate = nextEvent.Date.ToString();
                String newDay = getDayFromDate(newDate);
                allEvents.Add(nextEvent.EventName, newDay);
            }
            for (int i = 0; i < taskList.Count(); i++)
            {
                var nextTask = taskList.ElementAt(i);
                String newDate = nextTask.Date.ToString();
                String newDay = getDayFromDate(newDate);
                if (allEvents.ContainsKey(nextTask.TaskName) == false)
                {
                    allEvents.Add(nextTask.TaskName, newDay);
                }
            }
            for (int i = 0; i < activityList.Count(); i++)
            {
                var nextEvent = activityList.ElementAt(i);
                String newDate = nextEvent.Date.ToString();
                String newDay = getDayFromDate(newDate);
                allEvents.Add(nextEvent.ActivityName, newDay);
            }
            for (int i = 0; i < timeTable.Count(); i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    if (j * 7 <= 31)
                    { 
                        lessons.Add(new Tuple<String, int>(timeTable.ElementAt(i).CourseName, Convert.ToInt32(timeTable.ElementAt(i).Day)+(j*7)));
                    }
                }
            }
            fillListBoxes(allDays, allEvents);
            fillListBoxes(allDays,lessons);
            numToMonth(label1);
            label1.Text = label1.Text + " " + year;
           
        }

        public static void RightAmountOfDays(ListBox list29, ListBox list30, ListBox list31,Label text29, Label text30, Label text31)
        {
            if(month == "11" || month == "4" || month == "6" || month == "9")
            {
                text31.Visible = false;
                list31.Visible = false;
                text29.Visible = true;
                text30.Visible = true;
                list29.Visible = true;
                list30.Visible = true;
                
            }
            else if (month == "2")
            {
                if(DateTime.IsLeapYear(Convert.ToInt32(year)))
                {
                    text31.Visible = false;
                    list31.Visible = false;
                    text30.Visible = false;
                    list30.Visible = false;
                    text29.Visible = true;
                    list29.Visible = true;
                }
                else
                {
                    text31.Visible = false;
                    text30.Visible = false;
                    text29.Visible = false;
                    list31.Visible = false;
                    list30.Visible = false;
                    list29.Visible = false;
                }
            }
            else
            {
                text31.Visible = true;
                text30.Visible = true;
                text29.Visible = true;
                list31.Visible = true;
                list30.Visible = true;
                list29.Visible = true;
            }
        }

        public static void numToMonth(Label monthLabel)
        {
            switch (month)
            {
                case "1":
                    monthLabel.Text = "ינואר";
                    break;
                case "2":
                    monthLabel.Text = "פברואר";
                    break;
                case "3":
                    monthLabel.Text = "מרץ";
                    break;
                case "4":
                    monthLabel.Text = "אפריל";
                    break;
                case "5":
                    monthLabel.Text = "מאי";
                    break;
                case "6":
                    monthLabel.Text = "יוני";
                    break;
                case "7":
                    monthLabel.Text = "יולי";
                    break;
                case "8":
                    monthLabel.Text = "אוגוסט";
                    break;
                case "9":
                    monthLabel.Text = "ספטמבר";
                    break;
                case "10":
                    monthLabel.Text = "אוקטובר";
                    break;
                case "11":
                    monthLabel.Text = "נובמבר";
                    break;
                case "12":
                    monthLabel.Text = "דצמבר";
                    break;
                default: break;
            }
        }

        private void מבחניםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void פעילויותToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
        }

        public IEnumerable<EasyCalProj.EasyCalDB.Events1> GetEventList()
        {
            var ExamList = new List<EasyCalProj.EasyCalDB.Events1>();

            using (var EasyCal = new EasyCalEntities1())
            {
                ExamList = EasyCal.Events1.ToList();
            }
            return ExamList;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventDetails(sender as ListBox);
        }

        public void EventDetails(ListBox list)
        {
            if (list.SelectedItems.Count == 0) return;
            EventName = list.SelectedItem.ToString();
            list.ClearSelected();
            var eventList = GetEventList();
            var taskList = GetTaskList();
            var activityList = GetActivityList();
            var timeTable = GetTimeTableList();
            if (eventList.Where(p => p.EventName == EventName).SingleOrDefault() != null)
            {
                Form9 form9 = new Form9();
                form9.Show();
                return;
            }
            else if (taskList.Where(p => p.TaskName == EventName).SingleOrDefault() != null)
            {
                    
                 Form10 form10 = new Form10();
                 form10.Show();
                 return;
            }
            else if(activityList.Where(p=> p.ActivityName == EventName).SingleOrDefault() != null)
            {
                Form13 form13 = new Form13();
                form13.Show();
                return;
            }
            else if(timeTable.Where(p=>p.CourseName == EventName).SingleOrDefault() != null)
            {
                Form14 form14 = new Form14();
                form14.Show();
                return;
            }
        }

        private void openEventDetails_Click(object sender, EventArgs e)
        {
            EventDetails(sender as ListBox);
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (month == "12")
            {
                month = "1";
                year = (Convert.ToInt32(year) + 1).ToString();
            }
            else month = (Convert.ToInt32(month) + 1).ToString();
            numToMonth(label1);
            label1.Text = label1.Text + " " + year;
            RightAmountOfDays(day29, day30, day31, label30, label31, label32);
            clearListBoxes(allDays);
            var eventList = GetEventList();
            var taskList = GetTaskList();
            var activityList = GetActivityList();
            var timeTable = GetTimeTableList();
            Dictionary<String, String> allEvents = new Dictionary<String, String>();
            eventList = eventList.Where(p => p.Date <= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month))) && p.Date >= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).ToList();
            taskList = taskList.Where(p => p.Date <= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month))) && p.Date >= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).ToList();
            activityList = activityList.Where(p => p.Date <= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month))) && p.Date >= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).ToList();
            for (int i = 0; i < eventList.Count(); i++)
            {
                var nextEvent = eventList.ElementAt(i);
                String newDate = nextEvent.Date.ToString();
                String newDay = getDayFromDate(newDate);
                allEvents.Add(nextEvent.EventName, newDay);
            }
            for (int i = 0; i < taskList.Count(); i++)
            {
                var nextTask = taskList.ElementAt(i);
                String newDate = nextTask.Date.ToString();
                String newDay = getDayFromDate(newDate);
                if (allEvents.ContainsKey(nextTask.TaskName) == false)
                {
                    allEvents.Add(nextTask.TaskName, newDay);
                }
            }
            for (int i = 0; i < activityList.Count(); i++)
            {
                var nextEvent = activityList.ElementAt(i);
                String newDate = nextEvent.Date.ToString();
                String newDay = getDayFromDate(newDate);
                allEvents.Add(nextEvent.ActivityName, newDay);
            }
            fillListBoxes(allDays, allEvents);
            fillListBoxes(allDays, lessons);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (month == "1")
            {
                month = "12";
                year = (Convert.ToInt32(year) - 1).ToString();
            }
            else month = (Convert.ToInt32(month) - 1).ToString();
            numToMonth(label1);
            label1.Text = label1.Text + " " + year;
            RightAmountOfDays(day29, day30, day31, label30, label31, label32);
            clearListBoxes(allDays);
            var eventList = GetEventList();
            var taskList = GetTaskList();
            var activityList = GetActivityList();
            Dictionary<String, String> allEvents = new Dictionary<String, String>();
            eventList = eventList.Where(p => p.Date <= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month))) && p.Date >= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).ToList();
            taskList = taskList.Where(p => p.Date <= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month))) && p.Date >= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).ToList();
            activityList = activityList.Where(p => p.Date <= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month))) && p.Date >= new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).ToList();
            for (int i = 0; i < eventList.Count(); i++)
            {
                var nextEvent = eventList.ElementAt(i);
                String newDate = nextEvent.Date.ToString();
                String newDay = getDayFromDate(newDate);
                allEvents.Add(nextEvent.EventName, newDay);
            }
            for (int i = 0; i < taskList.Count(); i++)
            {
                var nextTask = taskList.ElementAt(i);
                String newDate = nextTask.Date.ToString();
                String newDay = getDayFromDate(newDate);
                if (allEvents.ContainsKey(nextTask.TaskName) == false)
                {
                    allEvents.Add(nextTask.TaskName, newDay);
                }
            }
            for (int i = 0; i < activityList.Count(); i++)
            {
                var nextEvent = activityList.ElementAt(i);
                String newDate = nextEvent.Date.ToString();
                String newDay = getDayFromDate(newDate);
                allEvents.Add(nextEvent.ActivityName, newDay);
            }
            fillListBoxes(allDays,allEvents);
            fillListBoxes(allDays, lessons);
        }

        private void day8_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day19_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        public void SetColor(ListBox day, DrawItemEventArgs e)
        {
            if (day.Items.Count == 0) return;
            e.DrawBackground();
            Graphics g = e.Graphics;
            string text = day.Items[e.Index].ToString();
            StringFormat drawFormat = new StringFormat();
            SolidBrush EventColor = new SolidBrush(Color.Black);
            int itemX;
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            var eventList = GetEventList();
            var taskList = GetTaskList();
            var activityList = GetActivityList();
            var timeTable = GetTimeTableList();
            if (eventList.Where(p => p.EventName == text).SingleOrDefault() != null)
            {
                EventColor.Color = Color.DodgerBlue;
            }
            else if (taskList.Where(p => p.TaskName == text).SingleOrDefault() != null)
            {
                var task = taskList.Where(p => p.TaskName == text).SingleOrDefault();
                if (task.Status == "לא בוצע")
                {
                    EventColor.Color = Color.Red;
                }
                else if (task.Status ==  "בוצע")
                {
                    EventColor.Color = Color.Lime;
                }
                else EventColor.Color = Color.Gold;
            }
            else if (activityList.Where(p => p.ActivityName == text).SingleOrDefault() != null)
            {
                EventColor.Color = Color.Orange;
            }
            else if(timeTable.Where(p=> p.CourseName == text).SingleOrDefault() != null)
            {
                EventColor.Color = Color.DarkCyan;
            }
            g.FillRectangle(EventColor, e.Bounds);
            if (day.Items.Count >= 5) {
                itemX = day.GetItemRectangle(e.Index).Location.X + 89;
            }
            else itemX = day.GetItemRectangle(e.Index).Location.X + 107;
            g.DrawString(text, e.Font, new SolidBrush(Color.White), itemX, day.GetItemRectangle(e.Index).Location.Y + 2, drawFormat);
            e.DrawFocusRectangle();
        }

        private void day1_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day2_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day3_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day4_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day5_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day6_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day7_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day9_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day10_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day11_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day12_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day13_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day14_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day15_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day16_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day17_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day18_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day20_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day21_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day22_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day23_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day24_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day25_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day26_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day27_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day28_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day29_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day30_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }

        private void day31_DrawItem(object sender, DrawItemEventArgs e)
        {
            SetColor(sender as ListBox, e);
        }
    }
}
