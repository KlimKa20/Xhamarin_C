using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Forms.Xaml;
using SQLite;


namespace App8
{
    
    public partial class informpas : ContentPage
    {
        public informpas()
        {
            InitializeComponent();
        }
    
        protected override void OnAppearing()
        {
            TrainsList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();

        }
        // обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            Train selectedTrain = (Train)e.SelectedItem;
            NavigationPage  TrainPage = new NavigationPage(new TrainPage());
            TrainPage.BindingContext = selectedTrain;
            TrainsList.ItemsSource = App.Database.GetItems();
            await  Navigation.PushModalAsync(TrainPage);
        }
        private void SearchUsers(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TrainsList.ItemsSource = (from t in App.Database.GetItems() where t.Number.Contains(text) select t);
            }
            else
            {
                TrainsList.ItemsSource = App.Database.GetItems();
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            TrainsList.ItemsSource = App.Database.GetItems();
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            TrainsList.ItemsSource = (from num in App.Database.GetItems() select num).Reverse();
        }

        private void MenuItem_Clicked_2(object sender, EventArgs e)
        {
            TrainsList.ItemsSource = (from t in App.Database.GetItems() where t.Count>0 select t);
        }
    }
  
}