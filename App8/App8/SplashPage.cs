using App8;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App8
{
    class SplashPage:ContentPage
    {
        Image SplashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            SplashImage = new Image
            {
                Source = "br.png",
            };

            AbsoluteLayout.SetLayoutFlags(SplashImage, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(SplashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(SplashImage);
            this.BackgroundColor = Color.White;
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SplashImage.ScaleTo(1, 2000);
            await SplashImage.ScaleTo(10,1500,Easing.BounceIn);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
