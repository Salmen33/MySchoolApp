using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySchoolApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        //public string luocation { get; private set; }
        public Page1(Byte[] bytes, Location location)
        {
            InitializeComponent();

            ImageAbsen.Source = ImageSource.FromStream(() => new MemoryStream(bytes));

            //luocation = location.ToString();


            BindingContext = this;
        }
    }
}