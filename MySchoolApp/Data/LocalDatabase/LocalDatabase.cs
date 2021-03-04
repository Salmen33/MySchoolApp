using MySchoolApp.Model;
using MySchoolApp.Model.DTO;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySchoolApp.Data.LocalDatabase
{
    public class LocalDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserDTO>().Wait();
        }

        public async Task SaveSession(UserDTO userDTO)
        {
            if(userDTO.ID != 0)
            {
                return;
            }

            await _database.InsertAsync(userDTO);

        }

        public async Task<UserDTO> GetSession()
        {
            var session = await _database.Table<UserDTO>().FirstOrDefaultAsync();

            if(session == null)
            {
                return null;
            }

            var result = DateTime.Now.Day - session.LastLogin.Day;

            if(result > 3)
            {
                await DeleteSession(session.ID);
                return null;
            }
            return session;
        }

        public async Task DeleteSession(int id)
        {
            await _database.Table<UserDTO>().Where(e => e.ID == id).DeleteAsync();
        }
    }
}
