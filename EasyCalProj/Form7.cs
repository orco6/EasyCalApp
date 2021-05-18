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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public IEnumerable<EasyCalProj.EasyCalDB.Exams1> GetExamList()
        {
            var ExamList = new List<EasyCalProj.EasyCalDB.Exams1>();

            using (var EasyCal = new EasyCalEntities1())
            {
                ExamList = EasyCal.Exams1.ToList();
            }
            return ExamList;
        }


        private void Form7_Load(object sender, EventArgs e)
        {
            var examList = GetExamList();
            dataGridView1.DataSource = examList;
        }
    }
}
