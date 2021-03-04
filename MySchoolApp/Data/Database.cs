using MySchoolApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace MySchoolApp.Data
{
    abstract public class Database
    {
        abstract public Task<List<T>> GetAllItem<T>();

        abstract public Task SaveItem<T>(bool isNewItem);

        
    }
}
