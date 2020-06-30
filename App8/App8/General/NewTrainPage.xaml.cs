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
    public partial class NewTrainPage : ContentPage
    {
        string typevag;
        Dictionary<string, int> rw = new Dictionary<string, int>();
        public NewTrainPage()
        {
            InitializeComponent();
            
            cran.RowDefinitions.Add(new RowDefinition { Height = 40});
            cran.RowDefinitions.Add(new RowDefinition { Height = 40 });
            cran.RowDefinitions.Add(new RowDefinition { Height = 40 });
            cran.RowDefinitions.Add(new RowDefinition { Height = 40 });
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 18, Text = Resource1.Type_of_car, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, 0, 0);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 18, Text = Resource1.Number_of_tickets, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center  } }, 1, 0);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 18, Text = Resource1.cost, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center} }, 2, 0);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { Text = "", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center} }, 3, 0);
            Button button1 = new Button { Text = Resource1.Buy, BackgroundColor = Color.LightYellow };
            Button button2 = new Button { Text = Resource1.Buy, BackgroundColor = Color.LightYellow };
            Button button3 = new Button { Text = Resource1.Buy, BackgroundColor = Color.LightYellow };
            Button button4 = new Button { Text = Resource1.Buy, BackgroundColor = Color.LightYellow };
            button1.Clicked += OnButtonClicked;
            button2.Clicked += OnButtonClicked1;
            button3.Clicked += OnButtonClicked2;
            button4.Clicked += OnButtonClicked3;
            int ui = 0, ii = 1;
            foreach (TYPEplace tYP in newpreview.tran.Trains)
            {
                ui = 0;
                cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label {FontSize=18, Text = tYP.Typeplace, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                cran.Children.Add(new Frame { BackgroundColor = Color.LightYellow, Content = new Label { Text = tYP.Count, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                cran.Children.Add(new Frame { BackgroundColor = Color.LightYellow, Content = new Label { Text = tYP.Price, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);

                switch (tYP.Typeplace)
                {
                    case "Плацкартный":
                        {
                            f1.IsVisible = true;
                            f2.IsVisible = true;
                            rw.Add(Resource1.Reserved_seat, ii);
                            cran.Children.Add(button1, ui++, ii++);
                            break;
                        }
                    case "Сидячий":
                        {
                            f7.IsVisible = true;
                            f8.IsVisible = true;
                            rw.Add(Resource1.Sedentary, ii);
                            cran.Children.Add(button2, ui++, ii++);
                            break;
                        }
                    case "СВ":
                        {
                            f3.IsVisible = true;
                            f4.IsVisible = true;
                            rw.Add(Resource1.SV, ii);
                            cran.Children.Add(button3, ui++, ii++);
                            break;
                        }
                    case "Купе":
                        {
                            f5.IsVisible = true;
                            rw.Add(Resource1.Coupe, ii);
                            cran.Children.Add(button4, ui++, ii++);
                            break;
                        }
                }

            }
        }
        void OnButtonClicked(object sender, System.EventArgs e)
        {
            typevag = "Плацкартный";
            OnButtonClicked0();
        }
        void OnButtonClicked1(object sender, System.EventArgs e)
        {
            typevag = "Сидячий";
            OnButtonClicked0();
        }
        void OnButtonClicked2(object sender, System.EventArgs e)
        {
            typevag = "СВ";
            OnButtonClicked0();
        }
        void OnButtonClicked3(object sender, System.EventArgs e)
        {
            typevag = "Купе";
            OnButtonClicked0();
        }
        async void OnButtonClicked0()
        {
            if (MainPage.list == 2)
            {
                var action = await DisplayActionSheet("Колличество билетов", "Отмена", "Подтвердить", "1", "2", "3");
                if (action == "1" || action == "2" || action == "3")
                {
                    foreach (TYPEplace tYP in newpreview.tran.Trains)
                    {
                        if (Int32.Parse(tYP.Count) > 0 && tYP.Typeplace == typevag)
                        {
                            var UserN = from us in App.Databaseuser.GetItems() where us.Id == MainPage.IDNOW select us;
                            foreach (User user in UserN)
                            {
                                string ticket = user.Trains;
                                if (ticket == null)
                                    user.Trains = newpreview.tran.Id + "/" + action + "/" + typevag + "/"+newpreview.tran.Starttime+"/" + newpreview.tran.Startdate + "/";
                                else
                                {
                                    user.Trains = ticket + newpreview.tran.Id + "/" + action + "/" + typevag + "/" + newpreview.tran.Starttime + "/" + newpreview.tran.Startdate + "/";
                                }
                                App.Databaseuser.SaveItem(user);

                            }

                            tYP.Count = (Int32.Parse(tYP.Count) - Int32.Parse(action)).ToString();
                            cran.Children.Add(new Label { Text = tYP.Count, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, BackgroundColor = Color.LightYellow }, 1, rw[typevag]);
                            await DisplayAlert("Предупреждение", "Покупка совершена успешно", "OK");
                            await this.Navigation.PopModalAsync();
                        }
                    }
                }

            }
            else
            {
                MainPage.when = 1;
                await Navigation.PushAsync(new LogIn()
            {
                BackgroundColor = Color.FromHex(MainPage.when2)
            });
                await DisplayAlert("Предупреждение", "Покупка невозможна,совершите вход в аккаунт", "OK");

            }

        }
        
    }
}