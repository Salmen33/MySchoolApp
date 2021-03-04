using MySchoolApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySchoolApp.Data.AdministrationDatabase
{
    public class AdministrationManager
    {
        Database _database;

        public AdministrationManager(Database database)
        {
            _database = database;
        }

        public async Task<List<Administrations>> AllItem()
        {
            return await _database.GetAllItem<Administrations>();
        }
    }
}
