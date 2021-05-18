using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyCalProj.EasyCalDB;

namespace EasyCalProj
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public IEnumerable<EasyCalProj.EasyCalDB.Events1> GetEventList()
        {
            var EventList = new List<EasyCalProj.EasyCalDB.Events1>();

            using (var EasyCal = new EasyCalEntities1())
            {
                EventList = EasyCal.Events1.ToList();
            }
            return EventList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var eventList = GetEventList();
            if(comboBox1.Text == "יומית")
            {
                var DayList = eventList.Where(p => p.Date == DateTime.Now.Date).ToList();
                dataGridView1.DataSource = DayList;
            }
            else if(comboBox1.Text == "שבועית")
            {
                var WeekList = eventList.Where(p => p.Date <= DateTime.Now.AddDays(7).Date && p.Date >= DateTime.Now.Date).ToList();
                dataGridView1.DataSource = WeekList;
            }
            else if(comboBox1.Text == "חודשית")
            {
                var MonthList = eventList.Where(p => p.Date <=new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month)) && p.Date >= DateTime.Now.Date).ToList();
                dataGridView1.DataSource = MonthList;
            }
                
            
          
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            var eventList = GetEventList();
            var MonthList = eventList.Where(p => p.Date <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) && p.Date >= DateTime.Now.Date).ToList();
            dataGridView1.DataSource = MonthList;
        }
    }
}
