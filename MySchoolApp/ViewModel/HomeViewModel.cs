using MySchoolApp.ViewModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;
using MySchoolApp.Model;
using Android.Widget;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MySchoolApp.Pages
{
    public class HomeViewModel : INotifyPropertyChanged
    {


         ObservableCollection<User> _user { get;set; }



        string name;
        string nisn;
        string balance;
        DateTime dateTime = DateTime.Now;
        bool absen = false;
        bool isRefresing;
        public event PropertyChangedEventHandler PropertyChanged;

        public HomeViewModel()
        {
            _user = new ObservableCollection<User>();

            name = "Abang Dearen";
            nisn = "123tutupBotol";
            balance = string.Format("Rp. 300,000,000",  string.Empty);

            RefreshView refreshView = new RefreshView();
            refreshView.Command = RefreshHomePageCommand;


            PayCommand = new Command(async ()=> await UpdateBalance());
            AdministrasionCommand = new Command(async ()=> await AdministrasionPage());
            AbsenCommand = new Command(async ()=> await AttendPage());
            RechargeCommand = new Command(async () => await RechargeBalance());
            RefreshHomePageCommand = new Command( () =>
            {
                IsRefresing = true; 
                Task.Run(ReInitialUserData);

                IsRefresing = false;
            });
        }

        private async Task ReInitialUserData()
        {
            
            var _users = await App.userDatabaseManager.SearchUserByUsername("Email", _user[0].Email);

            
            _user.Clear();
            _user.Add(_users);
            balance = string.Format("Rp. {0}", _user[0].Balance);
            Balance = balance;

            return;
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        public string Amount
        {
            get; set;
        }

        public string Name
        {
            get { return name; }
            set { name = value;}
        }

        public string NISN
        {
            get { return nisn; }
            set { nisn = value; }
        }
        public string Balance
        {
            get { return balance; }
            set {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public DateTime Today
        {
            get { return dateTime; }
            set 
            { 
                dateTime = value;
            }
        }

        public bool Absen
        {
            get { return absen; }
            set 
            { 
                absen = value;
            }
        }

        public bool IsRefresing
        {
            get { return isRefresing; }
            set
            {
                isRefresing = value;
                OnPropertyChanged("IsRefresing");
            }
        }

        public Command PayCommand { get; }
        public Command AdministrasionCommand { get; }
        public Command AbsenCommand { get; }
        public Command RechargeCommand { get; }
        public Command RefreshHomePageCommand { get; }


        async Task AdministrasionPage()
        {

            List<Administrations> al = new List<Administrations>();

            try
            {
                al = await App.adminDatabases.AllItem();


                var administrasionListPage = new AdministrasionListPage(al);

                await Application.Current.MainPage.Navigation.PushAsync(administrasionListPage);
            } catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }
        }

        async Task AttendPage()
        {
            var absenVM = new CameraViewModel();

            var absenPage = new AbsenPage();

            absenPage.BindingContext = absenVM;

            await Application.Current.MainPage.Navigation.PushAsync(absenPage);
        }

        async Task RechargeBalance()
        {
            var PayPage = new RechargeBalance();
            PayPage.BindingContext = this;

            await Application.Current.MainPage.Navigation.PushAsync(PayPage);
            
        }


        async Task UpdateBalance()
        {
           /* if (!Amount.Contains("1234567890"))
                return;*/

            User userToUpdate = new User()
            {
                ID = _user[0].ID,
                Fullname = _user[0].Fullname,
                NISN = _user[0].NISN,
                Balance = _user[0].Balance += int.Parse(Amount),
                Email = _user[0].Email,
                Password = _user[0].Password
            };

            await App.userDatabaseManager.UpdateUser(_user[0].ID.ToString(), userToUpdate);


            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
