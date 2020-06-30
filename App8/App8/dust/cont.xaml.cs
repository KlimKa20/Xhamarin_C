using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
    public partial class cont : ContentPage
    {
        int i = 0;
        public cont()
        {
            InitializeComponent();
            dfd.Text = Resource1.Leave_your_photo_with_our_staff_or_related_to_the_railway;
            one.BorderColor = Color.FromHex(MainPage.when1);
            two.BorderColor = Color.FromHex(MainPage.when1);
            foreach (Photo p in App.Databaseph.GetItems())
            {
                ss.Children.Add(new Image { WidthRequest = 350, HorizontalOptions = LayoutOptions.Center, Source = ImageSource.FromFile(p.ph) }, i++, 0);
            }
            
        }

        private async void One_Clicked(object sender, EventArgs e)
        {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    ss.Children.Add(new Image { WidthRequest = 350, HorizontalOptions = LayoutOptions.Center, Source = ImageSource.FromFile(photo.Path) },i++,0);
                App.Databaseph.SaveItem(new Photo { ph = photo.Path });
                }
           
        }

        private async void Two_Clicked(object sender, EventArgs e)
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
               ss.Children.Add(new Image { WidthRequest = 350,HorizontalOptions= LayoutOptions.Center, Source = ImageSource.FromFile(file.Path) },i++,0);
                App.databaseph.SaveItem(new Photo { ph = file.Path });
            }
        }
    }
}