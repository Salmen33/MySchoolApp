using MySchoolApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySchoolApp.Data
{
    public interface IUserDatabae
    {

        Task<User> SearchUser(string searchBy, string parameter);

        Task<Exception> UpdateBalance(string id, User user);


    }
}
