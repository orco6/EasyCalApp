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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        public IEnumerable<EasyCalProj.EasyCalDB.Activity> GetActivitiesList()
        {
            var ActivityList = new List<EasyCalProj.EasyCalDB.Activity>();

            using (var EasyCal = new EasyCalEntities1())
            {
                ActivityList = EasyCal.Activities.ToList();
            }
            return ActivityList;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            var activityList = GetActivitiesList();
            var MonthList = activityList.Where(p => p.Date <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) && p.Date >= DateTime.Now.Date).ToList();
            dataGridView2.DataSource = activityList;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
