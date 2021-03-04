using MySchoolApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySchoolApp.Data.UserServiceDatabase
{
    public class UserDatabaseManager
    {
        IUserDatabae _userDatabase;

        public UserDatabaseManager(IUserDatabae userDatabae)
        {
            _userDatabase = userDatabae;
        }

        public async Task<User> SearchUserByUsername(string searchBy, string parameter)
        {
            return await _userDatabase.SearchUser(searchBy, parameter);
        }

        public async Task<Exception> UpdateUser(string id, User user)
        {
            return await _userDatabase.UpdateBalance(id, user);
        }
    }
}
