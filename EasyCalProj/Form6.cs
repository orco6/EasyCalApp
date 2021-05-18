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
    public partial class Form6 : Form
    {
        public Form6()
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


        private void button1_Click(object sender, EventArgs e)
        {
            var taskList = GetTaskList();
            dataGridView1.DataSource = taskList;

            /*if (comboBox1.Text == "בוצע")
            {
                var DoneTasks = taskList.Where(p => p.Status == "בוצע").ToList();
                dataGridView1.DataSource = DoneTasks;
            }
            else if (comboBox1.Text == "לא בוצע")
            {
                var NotDoneTasks = taskList.Where(p => p.Status == "לא בוצע").ToList();
                dataGridView1.DataSource = NotDoneTasks;
            }
            else if (comboBox1.Text == "בתהליך")
            {
                var InProcess = taskList.Where(p => p.Status == "בתהליך").ToList();
                dataGridView1.DataSource = InProcess;
            }*/


        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells != null && dataGridView1.SelectedCells.Count == 1)
            {
                int item;
                int.TryParse(dataGridView1.CurrentRow.Cells["SerialNum"].Value.ToString(), out item);

                var eventList = GetTaskList();

                var itemToRemove = eventList.FirstOrDefault(p => p.SerialNum == item);

                using (var EasyCal = new EasyCalEntities1())
                {
                    EasyCal.Tasks1.Attach(itemToRemove);
                    EasyCal.Tasks1.Remove(itemToRemove);
                    EasyCal.SaveChanges();
                }
                dataGridView1.DataSource = GetTaskList();
            }
        }
    }
}
