using MySchoolApp.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySchoolApp.Services.Authenticate
{
    public class IsAuthenticated
    {
        User user { get; set; }
        public string Email;
        public string Password;
        public IsAuthenticated()
        {
            user = new User();
        }


        public async Task<User> AuthUser()
        {
            if (string.IsNullOrEmpty(Email) || !Email.Contains('@') || string.IsNullOrWhiteSpace(Password))
            {
                throw new Exception("Please enter a valid data");
            }

            user = await App.userDatabaseManager.SearchUserByUsername("Email", Email);

            if(user == null)
            {
                
                throw new Exception("User Not Found");
            }

            if (Email != user.Email || Password != user.Password)
            {
                throw new Exception("Email or Password is incorrect");
            }
            

            return user;
        } 


        public async Task HashPassword(byte[] password, byte[] salt, int iterations, int length)
        {
            /*RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();*/
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
               deriveBytes.GetBytes(length).ToString();
            }

        }

        byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

    }
}
