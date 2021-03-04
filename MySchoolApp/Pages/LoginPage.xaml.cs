using Android.Widget;
using MySchoolApp.Model.DTO;
using MySchoolApp.Services.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySchoolApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string ErrorMsg;

        protected IsAuthenticated isAuthenticated { get; set; }
        public LoginPage(string message)
        {
            this.ErrorMsg = message;
            isAuthenticated = new IsAuthenticated();
            InitializeComponent();
            BindingContext = this;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            isAuthenticated.Password = PasswordField.Text;
            isAuthenticated.Email = EmailField.Text;
            UserDTO userdto = new UserDTO()
            {
                Email = EmailField.Text,
                LastLogin = DateTime.Now
            };
            try
            {
                var user = await isAuthenticated.AuthUser();

                await App.Database.SaveSession(userdto);

                var rootPage = new MainPage();
                rootPage.BindingContext = new HomeViewModel();

                Application.Current.MainPage = new NavigationPage(rootPage);

            } catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
                return;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ErrorMsg != null)
                DisplayAlert("Error", ErrorMsg, "Close");
        }

    }
}