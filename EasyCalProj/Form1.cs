using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using EasyCalProj.EasyCalDB;

namespace EasyCalProj
{
    public partial class Form1 : Form
    {

        public static String AccountName = "";

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var userList = GetUserList();
            var ChosenUser = userList.Where(p => p.UserName == textBox1.Text && p.Password == textBox2.Text).SingleOrDefault();
            if (ChosenUser == null)
                label4.Visible = true;
            else if(ChosenUser.PremCode == 3)  
            {
                AccountName = ChosenUser.FirstName + " " + ChosenUser.LastName;
                Form2 f2 = new Form2();
                this.Hide();
                f2.Show();
            }
            else if(ChosenUser.PremCode == 2)
            {
                Form11 form11 = new Form11();
                this.Hide();
                form11.Show();
            }
            else if(ChosenUser.PremCode == 1)
            {
                Form12 form12 = new Form12();
                this.Hide();
                form12.Show();
            }

        }


        public IEnumerable<EasyCalProj.EasyCalDB.User> GetUserList()
        {
            var UserList = new List<EasyCalProj.EasyCalDB.User>();

            using (var EasyCal = new EasyCalEntities1())
            {
                UserList = EasyCal.Users.ToList();
            }
            return UserList;
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    //    var ClickedEvent = new EasyCalProj.EasyCalDB.Event
        //    //    {
        //    //        Description = "",
        //    //        Date = DateTime.Now
        //    //    };
        //    //    EventAddToDataBase(ClickedEvent);
        //    // var newList = GetEventList();
        //    //var ChosenItem = newList.Where(p => p.EventName.ToLower().StartsWith("a")).SingleOrDefault();
        //    //var ChosenUser = userList.Where(p => p.PremCode == 1).ToList();
        //}
    }
}
