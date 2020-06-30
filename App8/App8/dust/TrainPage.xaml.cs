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
    public partial class TrainPage : ContentPage
    {
        public TrainPage()
        {
            InitializeComponent();
            if (MainPage.list != 2)
            {
                ac.IsVisible = false;
                acl.IsVisible = false;
                ab.IsVisible = false;
                d.IsVisible = false;
            }
        }
        private void SaveTrain(object sender, EventArgs e)
        {
            var Train = (Train)BindingContext;
                App.Database.SaveItem(Train);
            this.Navigation.PopAsync();
        }
        private void DeleteTrain(object sender, EventArgs e)
        {
            var Train = (Train)BindingContext;
            App.Database.DeleteItem(Train.Id);
            this.Navigation.PopAsync();
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (ac.Value != 0)
            {
                if (((Train)BindingContext).Count > 0)
                {
                    var UserN = from us in App.Databaseuser.GetItems() where us.Id == MainPage.IDNOW select us;
                    foreach (User user in UserN)
                    {
                        string ticket = user.Trains;
                        if (ticket == null)
                            user.Trains = Numb.Text + "/" + ac.Value + "/";
                        else
                        {
                            user.Trains = ticket + Numb.Text + "/" + ac.Value + "/";
                        }
                        App.Databaseuser.SaveItem(user);
                    }
                    var Train = (Train)BindingContext;
                    Train.Count -= (int)ac.Value;
                    App.Database.SaveItem(Train);
                    tiket.Text = Train.Count.ToString();
                    await DisplayAlert("Предупреждение", "Покупка совершена успешно", "OK");
                    await this.Navigation.PopModalAsync();
                   
                    
                }
                else
                    await DisplayAlert("Предупреждение", "Билеты закончились ", "OK");
            }
        }
        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (acl != null)
                acl.Text = String.Format("Выбрано: {0}", e.NewValue);
        }
    }
}