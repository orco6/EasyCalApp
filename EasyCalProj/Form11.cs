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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        public static void EventAddToDataBase(EasyCalProj.EasyCalDB.Activity newEvent)
        {
            using (var EasyCal = new EasyCalEntities1())
            {
                EasyCal.Activities.Add(newEvent);
                EasyCal.SaveChanges();
            }
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

        public IEnumerable<EasyCalProj.EasyCalDB.Marathone> GetMarathonesList()
        {
            var MarathoneList = new List<EasyCalProj.EasyCalDB.Marathone>();

            using (var EasyCal = new EasyCalEntities1())
            {
                MarathoneList = EasyCal.Marathones.ToList();
            }
            return MarathoneList;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void Form11_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ClickedEvent = new EasyCalProj.EasyCalDB.Activity
            {
                ActivityName = textBox1.Text,
                Date = dateTimePicker1.Value,
                StartTime = comboBox1.Text,
                Endtime = comboBox2.Text,
                Location = textBox2.Text,
                Price = textBox3.Text,
                Description = textBox4.Text
            };
            EventAddToDataBase(ClickedEvent);
            MessageBox.Show("פעילות נוספה בהצלחה");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        public static void EventAddToDataBase(EasyCalProj.EasyCalDB.Marathone newEvent)
        {
            using (var EasyCal = new EasyCalEntities1())
            {
                EasyCal.Marathones.Add(newEvent);
                EasyCal.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ClickedEvent = new EasyCalProj.EasyCalDB.Marathone
            {
                Name = textBox5.Text,
                LecturerName = textBox6.Text,
                StartTime = comboBox3.Text,
                EndTime = comboBox4.Text,
                StartDate = DateTime.Parse(dateTimePicker2.Text),
                EndDate = DateTime.Parse(dateTimePicker3.Text),
                Location = textBox7.Text,
                Price = textBox8.Text, 
            };
            EventAddToDataBase(ClickedEvent);
            MessageBox.Show("פעילות נוספה בהצלחה");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Marthonlist = GetMarathonesList();
            dataGridView1.DataSource = Marthonlist;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var ActivitiesList = GetActivityList();
            dataGridView1.DataSource = ActivitiesList;
        }
    }
}
