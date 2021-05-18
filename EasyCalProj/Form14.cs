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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
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

        private void Form14_Load(object sender, EventArgs e)
        {
            label2.Text = Form2.EventName;
            this.Text = label2.Text;
            var TimeTable = GetTimeTableList();
            var ClickedEvent = TimeTable.Where(P => P.CourseName == Form2.EventName).Single();
            label8.Text = ClickedEvent.LecturerName;
            label5.Text = ClickedEvent.StartTime.ToString();
            label6.Text = ClickedEvent.EndTime.ToString();
            label10.Text = "";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
