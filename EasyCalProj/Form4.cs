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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        public static void EventAddToDataBase(EasyCalProj.EasyCalDB.Tasks1 newEvent)
        {
            using (var EasyCal = new EasyCalEntities1())
            {
                EasyCal.Tasks1.Add(newEvent);
                EasyCal.SaveChanges();
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            var ClickedEvent = new EasyCalProj.EasyCalDB.Tasks1
            {
                TaskName = textBox4.Text,
                Course = textBox3.Text,
                Date = dateTimePicker2.Value,
                SubmitTime = TimeSpan.Parse(comboBox3.Text),
                Status = "לא בוצע"
            };
            EventAddToDataBase(ClickedEvent);
            MessageBox.Show("מטלה נוספה בהצלחה");
            this.Close();
        }
    }
}
