using MySchoolApp.Data;
using MySchoolApp.Data.AdministrationDatabase;
using MySchoolApp.Data.LocalDatabase;
using MySchoolApp.Data.UserServiceDatabase;
using MySchoolApp.Model;
using MySchoolApp.Pages;
using MySchoolApp.Services.Authenticate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySchoolApp
{
    public partial class App : Application
    {

        User user { get; set; }

        static LocalDatabase database;

        public static LocalDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocalDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sessions.db3"));
                }
                return database;
            }
        }

        public Task InitializationUser { get; private set; }
        public static UserDatabaseManager userDatabaseManager { get; private set; }


        public static AdministrationManager adminDatabases { get; set; }
        public static INavigation navigation { get; private set; }
        public App()
        {
            InitializeComponent();

            adminDatabases = new AdministrationManager(new AdministrationDatabase());

            userDatabaseManager = new UserDatabaseManager(new UserDatabase());

            MainPage = new Loading();


        }

        protected override async void OnStart()
        {
            try
            {
                //user = await App.userDatabaseManager.SearchUserByUsername("Email", Session.Email);
                MainPage = new NavigationPage(new MainPage());
                MainPage.BindingContext = new HomeViewModel();
            } catch (Exception e)
            {
                MainPage = new LoginPage(e.Message);
            }
            /*try
            {
                user = await App.userDatabaseManager.SearchUserByUsername("Email", "Holilah@gmail.com");

                MainPage = new NavigationPage(new MainPage());
                MainPage.BindingContext = new HomeViewModel(user);
                
            } catch(Exception e)
            {
                
                MainPage = new LoginPage(e.Message);
            }*/
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
