using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            newLogin.Placeholder = Resource1.Log;
            newPaswword.Placeholder = Resource1.Password;
            newMail.Placeholder = Resource1.newMail;
            newPhone.Placeholder = Resource1.newPhone;
            Contact.Title = Resource1.Contact;
            Personal.Title = Resource1.Personal;
            Registration.Text = Resource1.Redistration;
        }

        private async void Registration_Clicked(object sender, EventArgs e)
        {
            var user = (User)BindingContext;

            bool response = await DisplayAlert(Resource1.Save_, Resource1.Save_Text, Resource1.Yes, Resource1.No);
            if (response == true)
            {
                App.Databaseuser.SaveItem(user);
                await DisplayAlert(Resource1.Alert, Resource1.save, Resource1.OK);
                await this.Navigation.PopAsync();
            }
        }
    }
}