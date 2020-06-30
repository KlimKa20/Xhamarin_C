using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class account : ContentPage
    {
        public account()
        {
            InitializeComponent();
            ////WebView webView = new WebView();
            ////UrlWebViewSource urlSource = new UrlWebViewSource();
            ////urlSource.Url = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), "index.html");
            ////webView.Source = urlSource;
            ////this.Content = webView;
            
            Button takePhotoBtn = new Button { Text = "Сделать фото" };
            Button getPhotoBtn = new Button { Text = "Выбрать фото" };
            Image img = new Image();

            // выбор фото
            getPhotoBtn.Clicked += async (o, e) =>
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    img.Source = ImageSource.FromFile(photo.Path);
                }
            };

            // съемка фото
            takePhotoBtn.Clicked += async (o, e) =>
            {
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                        Directory = "Sample",
                        Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                    });

                    if (file == null)
                        return;

                    img.Source = ImageSource.FromFile(file.Path);
                }
            };
            //Content = new StackLayout
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    Children = {
            //        new StackLayout
            //        {
            //             Children = {takePhotoBtn, getPhotoBtn},
            //             Orientation =StackOrientation.Horizontal,
            //             HorizontalOptions = LayoutOptions.CenterAndExpand
            //        },
            //        img
            //    }
            //};
        }
        public interface IBaseUrl { string Get(); }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert(Resource1.Alert, Resource1.Are_you_sure_, Resource1.Yes, Resource1.No);
            if (result)
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    phot.ImageSource = ImageSource.FromFile(photo.Path);
                }
            }
            else
            {
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                        Directory = "Sample",
                        Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                    });

                    if (file == null)
                        return;

                    phot.ImageSource = ImageSource.FromFile(file.Path);
                }
            }
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( picker.Items[picker.SelectedIndex]== "Русский")
                Resource1.Culture = new CultureInfo("ru");
            else
                Resource1.Culture = new CultureInfo("en");
        }
    }
}