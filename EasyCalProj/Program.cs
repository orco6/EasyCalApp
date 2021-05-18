using EasyCalProj.EasyCalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace EasyCalProj
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void EventAddToDataBase(EasyCalProj.EasyCalDB.Events1 newEvent)
        {
            using (EasyCalEntities1 EasyCal = new EasyCalEntities1())
            {
                EasyCal.Events1.Add(newEvent);
                EasyCal.SaveChanges();
            }
        }

        public static void ExamAddToDataBase(EasyCalProj.EasyCalDB.Exams1 newExam)
        {
            using (EasyCalEntities1 EasyCal = new EasyCalEntities1())
            {
                EasyCal.Exams1.Add(newExam);
                EasyCal.SaveChanges();
            }
        }
    }

    
}
