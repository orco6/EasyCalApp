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
    public partial class Form9 : Form
    {

        public Form9()
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

        private void Form9_Load(object sender, EventArgs e)
        {
            label2.Text = Form2.EventName;
            this.Text = label2.Text;
            var EventList = GetEventList();
            var ClickedEvent = EventList.Where(p => p.EventName == label2.Text).Single();
            textBox2.Text = ClickedEvent.StartTime.ToString();
            textBox3.Text = ClickedEvent.EndTime.ToString();
            textBox4.Text = ClickedEvent.Description.ToString();
            dateTimePicker1.Value = DateTime.Parse(ClickedEvent.Date.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח שברצונך למחוק אירוע זה?", "מחיקת אירוע", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var eventList = GetEventList();
                var ItemToRemove = eventList.Where(p => p.EventName == label2.Text).Single();
                using (var EasyCal = new EasyCalEntities1())
                {
                    EasyCal.Events1.Attach(ItemToRemove);
                    EasyCal.Events1.Remove(ItemToRemove);
                    EasyCal.SaveChanges();
                }
                this.Hide();
            }
            else return;
        }
    }
}
