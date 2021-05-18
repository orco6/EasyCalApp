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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
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

        private void Form13_Load(object sender, EventArgs e)
        {
            label2.Text = Form2.EventName;
            this.Text = label2.Text;
            var activityList = GetActivityList();
            var ClickedEvent = activityList.Where(p => p.ActivityName == label2.Text).Single();
            dateTimePicker1.Value = DateTime.Parse(ClickedEvent.Date.ToString());
            textBox1.Text = ClickedEvent.StartTime;
            textBox2.Text = ClickedEvent.Endtime;
            label7.Text = ClickedEvent.Location;
            label9.Text = ClickedEvent.Price;
            textBox3.Text = ClickedEvent.Description;
        }
    }
}
