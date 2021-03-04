using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySchoolApp
{
    public class AdministrasionList : IEquatable<AdministrasionList>
    {

        

        public int ID { get;set; }
        public string Title { get; set; }
        public string dateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            AdministrasionList list = obj as AdministrasionList;
            if (list == null) return false;
            else return Equals(list);
        }

        public override int GetHashCode()
        {
            return ID;
        }
        public bool Equals(AdministrasionList other)
        {

            if (other == null) return false;
            return ID.Equals(ID);
        }
    }

    public class TheList 
    {
        DateTime time = DateTime.Now;
        public List<AdministrasionList> lastList()
        {
            List<AdministrasionList> admin = new List<AdministrasionList>();

            admin.Add(new AdministrasionList() { ID = 1, Title = "Uts Juli", dateTime = time.ToString() });
            admin.Add(new AdministrasionList() { ID = 2, Title = "Uts Agustus", dateTime = time.ToString() });
            admin.Add(new AdministrasionList() { ID = 3, Title = "Uts September", dateTime = time.ToString() });
            admin.Add(new AdministrasionList() { ID = 4, Title = "Uts Oktober", dateTime = time.ToString() });
            admin.Add(new AdministrasionList() { ID = 5, Title = "Uts November", dateTime = time.ToString() });
            admin.Add(new AdministrasionList() { ID = 6, Title = "Uts Desember", dateTime = time.ToString() });
            admin.Add(new AdministrasionList() { ID = 7, Title = "Uts Januari", dateTime = time.ToString() });
            return admin;
        }
    }
}
