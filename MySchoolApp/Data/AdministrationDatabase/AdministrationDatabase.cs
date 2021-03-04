using MySchoolApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace MySchoolApp.Data.AdministrationDatabase
{
    public class AdministrationDatabase : Database
    {
        List<Administrations> _administrations { get; set; }
        HttpClient client;
        public AdministrationDatabase()
        {
            client = new HttpClient(new AndroidClientHandler());
            client.Timeout = TimeSpan.FromSeconds(20);
        }

        public async override Task<List<T>> GetAllItem<T>()
        {
            Uri uri = new Uri(string.Format(Constans.AdministrationURL, ""));

            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(uri);
                    
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();

                    _administrations = JsonConvert.DeserializeObject<List<Administrations>>(content);
                }
                return _administrations as List<T>;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Connection Timeout, {0}", e.Message));
            }
        }

       

        public override Task SaveItem<Administrasions>(bool isNewItem)
        {
            
            throw new NotImplementedException();
        }
    }
}
