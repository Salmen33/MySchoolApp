using System;
using System.Collections.Generic;
using System.Text;

namespace MySchoolApp.Model
{
    public class Administrations
    {
        public int id { get; set; }

        public string title { get; set; }

        public int userId { get; set; }

        public string paidOff { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", title, bool.Parse(paidOff) ? "Lunas" : "Belum Lunas");
        }

    }
}
