using Android.Widget;
using MySchoolApp.Pages;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace MySchoolApp.ViewModel
{
    public class CameraViewModel : INotifyPropertyChanged
    {
        ImageSource photoSource;
        Location locationUser;

        public Location LocationUser
        {
            get { return locationUser; }
            set { locationUser = value; }
        }
        public ImageSource PhotoSource
        { 
            get { return photoSource; } 
            set { photoSource = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public CameraViewModel()
        {
            _TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            _RequestPermissionCommand = new Command(async () => await checkPermission());
            _TestCommand = new Command(async ()=> await MakeToast());


            
        }

        private Command _TakePhotoCommand;
        public Command TakePhotoCommand
        {
            get { return this._TakePhotoCommand; }
            set { this._TakePhotoCommand = value; }
        }

        private Command _RequestPermissionCommand;
        public Command RequestPermissionCommand
        {
            get { return this._RequestPermissionCommand; }
            set { this._RequestPermissionCommand = value; }
        }

        private Command _TestCommand;
        public Command TestCommand
        {
            get { return this._TestCommand; }
            set { this._TestCommand = value; }
        }


        async Task MakeToast()
        {
            await GetLocation();
            

            Toast.MakeText(Android.App.Application.Context, locationUser?.ToString(), ToastLength.Long).Show();
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        async Task<bool> checkPermission()
        {
            var CameraPermission = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var WriteStorage = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            var ReadStorage = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var LocationPermission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (CameraPermission != PermissionStatus.Granted || WriteStorage != PermissionStatus.Granted || 
                ReadStorage != PermissionStatus.Granted || LocationPermission != PermissionStatus.Granted)
            {
                CameraPermission =  await Permissions.RequestAsync<Permissions.Camera>();
                WriteStorage =  await Permissions.RequestAsync<Permissions.StorageRead>();
                ReadStorage = await Permissions.RequestAsync<Permissions.StorageWrite>();
                LocationPermission = await Permissions.RequestAsync<Permissions.LocationAlways>();
            }

            return true;

        }

        async Task TakePhotoAsync()
        {
            var permissionGranted = await checkPermission();

            if(!permissionGranted)
            {
                return;
            }

            if (!CrossMedia.Current.IsCameraAvailable)
                return;

            try
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(
                    new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                    {
                        DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                        Directory = "Absen",
                        SaveToAlbum = true

                    });

                if (photo != null)
                {
                    var stream = photo.GetStream();
                    byte[] bytes = ConvertToBytes(stream);

                    photoSource = ImageSource.FromStream(() => new MemoryStream(bytes));

                    var photoPage = new Page1(bytes, locationUser);
                    await Application.Current.MainPage.Navigation.PushAsync(photoPage);
                }
                return;
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            byte[] ConvertToBytes(Stream stream)
            {
                byte[] bytes;
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
        }

        async Task GetLocation()
        {
            var permissionGranted = await checkPermission();

            if (!permissionGranted) return;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);


                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    locationUser = location;
                }

                
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                return;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                return;
            }
            catch (PermissionException pEx)
            {
                return;
            }
            catch (Exception ex)
            {
                return;
            }
            return;
        }

    }
}
