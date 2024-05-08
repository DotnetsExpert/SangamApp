using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLMPortal.Models
{
    public class MyBackgroundJob
    {
        public static void DailyTask()
        {
            dbHelper db=new dbHelper();
            db.ExecuteNonQuery("PKC_Shedular");
            // Your daily task logic here
        }
    }
}