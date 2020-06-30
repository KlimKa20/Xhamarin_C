using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
    public class train
    {
        public string KI { get; set; }
        public string Ntrain { get; set; }
        public string TCount { get; set; }
        public string NType { get; set; }
        public string Starttime { get; set; }
        public string Startdate { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class exit : ContentPage
    {
        int press = 0;
        string temp1, temp2, temp3, temp4, temp5;
        public ObservableCollection<train> li { get; set; }
        public ObservableCollection<train> ii { get; set; }
        private void Revie_Clicked(object sender, EventArgs e)
        {
            Issue_answer.consist = Resource1.NumberTrain + temp1 + "\n"+ Resource1.Arrivaldate + temp5 + "\n"+ Resource1.Arrivaltime + temp4;
            Navigation.PushAsync(new Issue_answer());
        }

        public exit()
        {
            li = new ObservableCollection<train>();
            InitializeComponent();
            take.BorderColor = Color.FromHex(MainPage.when1);
            revie.BorderColor = Color.FromHex(MainPage.when1);
            take.Text = Resource1.take;
            revie.Text = Resource1.revie;
            var UserN = from us in App.Databaseuser.GetItems() where us.Id == MainPage.IDNOW select us;
            foreach (User user in UserN)
            {
                if (user.Trains != null)
                {
                    foreach (char ticket in user.Trains)
                    {
                        switch (press)
                        {
                            case 0:
                                {
                                    if (ticket != '/')
                                    {
                                        temp1 += ticket;
                                    }
                                    else
                                    {
                                        press = 1;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (ticket != '/')
                                    {
                                        temp2 += ticket;
                                    }
                                    else
                                    {
                                        press = 2;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (ticket != '/')
                                    {
                                        temp3 += ticket;
                                    }
                                    else
                                    {
                                        press = 3;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (ticket != '/')
                                    {
                                        temp4 += ticket;
                                    }
                                    else
                                    {
                                        press = 4;
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    if (ticket != '/')
                                    {
                                        temp5 += ticket;
                                    }
                                    else
                                    {
                                        press = 0;
                                        li.Add(new train { Ntrain = temp1, TCount = temp2, NType = temp3, Starttime = temp4, Startdate = temp5 });
                                        temp1 = temp2 = temp3 = temp4 = temp5 = "";
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            
            ii = new ObservableCollection<train>();
            var phoneGroups = from phone in li
                              group phone by phone.NType;
            foreach (IGrouping<string, train> g in phoneGroups)
            {
                foreach (var t in g)
                    ii.Add(new train { KI = g.Key, Ntrain = t.Ntrain, TCount = t.TCount, NType = t.NType, Starttime = t.Starttime, Startdate = t.Startdate });
            }
            TrainsList.BindingContext = ii;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert(Resource1.Alert,Resource1.Are_you_sure_, Resource1.Yes, Resource1.No);
            if (result)
            {
                MainPage.IDNOW = 0;
                await Navigation.PopModalAsync();
            }
        }

        private void TrainsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            train selectedPhone = e.Item as train;
            string[] td = selectedPhone.Startdate.Split(new char[] { '-' });
            string[] tn = selectedPhone.Starttime.Split(new char[] { ':' });
            DateTime date1 = new DateTime(Int32.Parse(td[0]), Int32.Parse(td[1]), Int32.Parse(td[2]), Int32.Parse(tn[0]), Int32.Parse(tn[1]), 0);
            if ( DateTime.Now.CompareTo(date1) > 0)
            {
                take.IsVisible = false;
                revie.IsVisible = true;
            }
            else
            {
                take.IsVisible = true;
                revie.IsVisible = false;
            }

            temp1 = selectedPhone.Ntrain;
            temp2 = selectedPhone.TCount;
            temp3 = selectedPhone.NType;
            temp4 = selectedPhone.Starttime;
            temp5 = selectedPhone.Startdate;
        }
        protected override void OnAppearing()
        {
            TrainsList.ItemsSource = li;
            base.OnAppearing();
        }

        public async void lost(object sender, EventArgs e)
        {
            if (temp1 != "")
            {
                var updatetrain = (from t in App.Database.GetItems() where temp1 == t.Number select t);
                foreach (Train temptrain in updatetrain)
                {
                    temptrain.Count += int.Parse(temp2);
                    App.Database.SaveItem(temptrain);
                }
                var UserN = from us in App.Databaseuser.GetItems() where us.Id == MainPage.IDNOW select us;
                foreach (User user in UserN)
                {
                    string num = temp1 + '/' + temp2 + '/' + temp3 + '/' + temp4 + '/' + temp5 + '/';
                    int n = user.Trains.IndexOf(num);
                    user.Trains = user.Trains.Remove(n, num.Length);
                    App.Databaseuser.SaveItem(user);
                    train tr = TrainsList.SelectedItem as train;
                    li.Remove(tr);
                    TrainsList.ItemsSource = li;
                    TrainsList.SelectedItem = null;
                    await DisplayAlert(Resource1.Alert,Resource1.You_got_rid_of_the_ticket_successfully , Resource1.OK);
                }
            }
            
        }

    }
}