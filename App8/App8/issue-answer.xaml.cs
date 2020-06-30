using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
    public partial class Issue_answer : ContentPage
    {
        public static string consist;
        public Issue_answer()
        {
            if (consist == null)
                consist = "";
            InitializeComponent();
            Send.BorderColor = Color.FromHex(MainPage.when1);
            givecom.Text = Resource1.Leave_your_comment;
            Send.Text = Resource1.sendermail;
        }
        protected override void OnAppearing()
        {
            ComentList.ItemsSource = App.Databasecoment.GetItems();
            base.OnAppearing();
        }

        private async void Send_Clicked(object sender, EventArgs e)
        {
            Comentar comentar = new Comentar();
            comentar.com =consist+"\n"+ Coment.Text;
            if (MainPage.list ==2)
            {
                var UserN = from us in App.Databaseuser.GetItems() where us.Id == MainPage.IDNOW select us;
            
                foreach (User user in UserN)
                {
                    comentar.User = user.Login;
                }
            }
            else
            {
                comentar.User = "Аноним";
            }
            App.Databasecoment.SaveItem(comentar);
            ComentList.ItemsSource = App.Databasecoment.GetItems();
            Coment.Text = "";
            consist = "";
            await DisplayAlert(Resource1.Alert, Resource1.ComentUpdate, Resource1.OK);
        }
    }
}