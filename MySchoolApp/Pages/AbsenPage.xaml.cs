using MySchoolApp.ViewModel;
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
    public partial class AbsenPage : ContentPage
    {
        public static INavigation navigation { get; private set; }
        public AbsenPage()
        {
            InitializeComponent();
            //BindingContext = new CameraViewModel();

            
        }



        
    }
}