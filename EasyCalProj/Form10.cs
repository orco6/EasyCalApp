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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
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

        public static void EventAddToDataBase(EasyCalProj.EasyCalDB.Tasks1 newEvent)
        {
            using (var EasyCal = new EasyCalEntities1())
            {
                EasyCal.Tasks1.Add(newEvent);
                EasyCal.SaveChanges();
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            label2.Text = Form2.EventName;
            this.Text = label2.Text;
            var EventList = GetTaskList();
            var ClickedEvent = EventList.Where(p => p.TaskName == label2.Text).Single();
            textBox1.Text = ClickedEvent.Course;
            textBox2.Text = ClickedEvent.SubmitTime.ToString();
            comboBox1.Text = ClickedEvent.Status;
            dateTimePicker1.Value = DateTime.Parse(ClickedEvent.Date.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח שברצונך למחוק מטלה זו?", "מחיקת מטלה", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var eventList = GetTaskList();
                var ItemToRemove = eventList.Where(p => p.TaskName == label2.Text).Single();
                using (var EasyCal = new EasyCalEntities1())
                {
                    EasyCal.Tasks1.Attach(ItemToRemove);
                    EasyCal.Tasks1.Remove(ItemToRemove);
                    EasyCal.SaveChanges();
                }
                this.Hide();
            }
            else return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var eventList = GetTaskList();
            var ItemToUpdate = eventList.Where(p => p.TaskName == label2.Text).Single();
            using (var EasyCal = new EasyCalEntities1())
            {
                EasyCal.Tasks1.Attach(ItemToUpdate);
                EasyCal.Tasks1.Remove(ItemToUpdate);
                EasyCal.SaveChanges();
            }
            ItemToUpdate.Status = comboBox1.Text;
            EventAddToDataBase(ItemToUpdate);
            MessageBox.Show("סטטוס עודכן בהצלחה");
        }
    }
}
