using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MySchoolApp.Services.WebAuthenticater
{
    public static class WebAuthenticater
    {
        public static async Task<string> AuthUser()
        {
            var token = await WebAuthenticator.AuthenticateAsync(new Uri("https://10.0.2.2:5001/Api/User/Auth"), new Uri("MyApp://"));

            return token?.AccessToken;
        }
    }
}
