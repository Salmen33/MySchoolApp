using MySchoolApp;
using MySchoolApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySchoolApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdministrasionListPage : ContentPage
    {
     
        public List<Administrations> AdministrationsList { get; set; }

        public AdministrasionListPage(List<Administrations> administrationsList)
        {
            this.AdministrationsList = administrationsList;
            InitializeComponent();
            BindingContext = this;
        }
        


    }
}