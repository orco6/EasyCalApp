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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public static void EventAddToDataBase(EasyCalProj.EasyCalDB.Events1 newEvent)
        {
            using (var EasyCal = new EasyCalEntities1())
            {
                EasyCal.Events1.Add(newEvent);
                EasyCal.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ClickedEvent = new EasyCalProj.EasyCalDB.Events1
            {
                EventName = textBox1.Text,
                Date = dateTimePicker1.Value,
                StartTime = comboBox1.Text,
                EndTime = comboBox2.Text,
                Description = textBox2.Text
            };
            EventAddToDataBase(ClickedEvent);
            MessageBox.Show("אירוע נוסף בהצלחה");
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
