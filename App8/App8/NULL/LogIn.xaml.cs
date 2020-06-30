using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        public static int value;
        static int port = 6062; // порт сервера
        static string address = "18.197.38.229";
        string mailpost;

        public LogIn()
        {
            InitializeComponent();
            set.IsVisible = false;
            log.IsVisible = true;
            Log.Placeholder = Resource1.Log;
            Password.Placeholder = Resource1.Password;
            Loggo.Text = Resource1.Loggo;
            Redistration.Text = Resource1.Redistration;
            Forget.Text = Resource1.Forget;
            Loggo.BorderColor = Color.FromHex(MainPage.when1);
            Redistration.BorderColor = Color.FromHex(MainPage.when1);
            Forget.BorderColor = Color.FromHex(MainPage.when1);
            gg.BorderColor = Color.FromHex(MainPage.when1);
            senderkey.BorderColor = Color.FromHex(MainPage.when1);
            sendermail.BorderColor = Color.FromHex(MainPage.when1);
            mailenter.Placeholder = Resource1.mailenter;
            sendermail.Text = Resource1.sendermail;
            keyenter.Placeholder = Resource1.keyenter;
            senderkey.Text = Resource1.sendermail;
            Password1.Placeholder = Resource1.Password1;
            Password2.Placeholder = Resource1.Password2;
            DisplayTime();
        }
        private async void Loginto(object sender, EventArgs e)
        {

            var vhod = App.Databaseuser.GetItems();
            bool rezult = true;
            foreach(User user in vhod)
            {
                if (user.Login == Log.Text&& user.Login=="Admin" && user.Password == Password.Text&&MainPage.when==0)
                {
                    MainPage.list = 1;
                    await Navigation.PushModalAsync(new MainAdmin());
                    rezult = false;
                    break;
                }
                else if (user.Login == Log.Text && user.Password == Password.Text && MainPage.when == 0)
                {
                    MainPage.IDNOW = user.Id;
                    MainPage.list = 2;
                    await Navigation.PushModalAsync(new MainUser());
                    rezult = false;
                    break;
                }
                else if(user.Login == Log.Text && user.Password == Password.Text && MainPage.when == 1)
                {
                    MainPage.IDNOW = user.Id;
                    MainPage.list = 2;
                    MainPage.when = 0;
                    await Navigation.PopAsync();
                    rezult = false;
                    break;
                }
                    
            }
            if(rezult)
                await DisplayAlert(Resource1.Alert, Resource1.User_was_not_found, Resource1.OK);
            Log.Text = "";
            Password.Text = "";
            
        }
        private async void DisplayTime()
        {
            await br.FadeTo(-1, 1);
            while (true)
            {
                await Task.Delay(5000);
                
                await br.TranslateTo(-300, 0, 1000);
                await br.FadeTo(1, 100);
                await br.TranslateTo(1100, 0, 10000);
                await br.FadeTo(-1, 1);
                await br.TranslateTo(-1100, 0, 1);
            }
        }
        private void Registration(object sender, EventArgs e)
        {
            User user = new User();
            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.BindingContext = user;
            Navigation.PushAsync(registrationPage);
        }

        private void Forget_Clicked(object sender, EventArgs e)
        {
            set.IsVisible = true;
            log.IsVisible = false;
            

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var ds = App.Databaseuser.GetItems();
            var vhod = from t in App.Databaseuser.GetItems() where t.Mail == mailenter.Text select t;
            if (vhod == null)
            {
                await DisplayAlert(Resource1.Alert, Resource1.User_was_not_found, Resource1.OK);
                return;
            }
            mailpost = mailenter.Text;
            foreach (User us in vhod)
            {
                try
                {
                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);
                    Random rnd = new Random();

                    //Получить случайное число (в диапазоне от 0 до 10)
                    value = rnd.Next(0, 10000);
                    string message = mailenter.Text + '/' + value.ToString();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    socket.Send(data);
                    mailenter.IsVisible = false;
                    sendermail.IsVisible = false;
                    keyenter.IsVisible = true;
                    senderkey.IsVisible = true;
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void Senderkey_Clicked(object sender, EventArgs e)
        {
            
            if (Int32.Parse(keyenter.Text) == value)
            {
                mailenter.IsVisible = false;
                sendermail.IsVisible = false;
                keyenter.IsVisible = true;
                senderkey.IsVisible = true;
                set.IsVisible = false;
                update.IsVisible = true;
            }
            else
                await DisplayAlert(Resource1.Alert, Resource1.This_code_is_not_valid, Resource1.OK);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if(Password1.Text==Password2.Text)
            {
                log.IsVisible = true;
                update.IsVisible = false;
                var vhod = from t in App.Databaseuser.GetItems() where t.Mail == mailpost select t;
                foreach (User us in vhod)
                {
                    us.Password = Password1.Text;
                }
                    await DisplayAlert(Resource1.Alert, Resource1.the_password_change_is_successful, Resource1.OK);
            }
            else
                await DisplayAlert(Resource1.Alert, Resource1.Password_value_does_not_match, Resource1.OK);
        }
    }
}