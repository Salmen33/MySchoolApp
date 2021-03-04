using MySchoolApp.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MySchoolApp.Data
{
    public class UserDatabase : IUserDatabae
    {
        HttpClient client;
        public List<User> _user { get; private set; }
        public UserDatabase()
        {
            client = new HttpClient(new AndroidClientHandler());
            client.Timeout = TimeSpan.FromSeconds(20);
        }

        public async Task<Exception> UpdateBalance(string id, User user)
        {
            if (int.Parse(id) != user.ID) return new Exception("please enter a valid data");

            Uri uri;
            HttpResponseMessage response;

            uri = new Uri(string.Format(Constans.RestURL, string.Format("/{0}", id)));
            Console.WriteLine(uri);
            
            try
            {
                var userContent = JsonConvert.SerializeObject(user);

                StringContent content = new StringContent(userContent, Encoding.UTF8, "application/json");


                response = await client.PatchAsync(uri, content);
                
                return null;

            } catch (Exception e)
            {
                Console.WriteLine("ERROR", e);
                return new Exception(e.Message);
            }
        }

       
        public async Task<User> SearchUser(string searchBy, string parameter)
        {
            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(parameter)) return null;

            Uri uri = new Uri(string.Format(Constans.RestURL, string.Format("?{0}={1}", searchBy, parameter)));

            HttpResponseMessage resonse;

            try
            {
                resonse = await client.GetAsync(uri);
                resonse.EnsureSuccessStatusCode();

                if (resonse.IsSuccessStatusCode)
                {
                    var content = await resonse.Content.ReadAsStringAsync();
                    _user = JsonConvert.DeserializeObject<List<User>>(content);
                }

                return _user[0];
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Connection Timeout, {0}", e.Message));
            }
        }

        
    }
}
