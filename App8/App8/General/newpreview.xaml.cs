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


using System.Net;
using System.IO;
using System.Threading;

namespace App8
{
    public class newtrain
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Starttime { get; set; }
        public string Startdate { get; set; }
        public string Finishtime { get; set; }
        public string Finishdate { get; set; }
        public string Totaltime { get; set; }
        public string check { get; set; }
        public ObservableCollection<TYPEplace> Trains { get; set; }

    }
    public class TYPEplace
    {
        public string Typeplace { get; set; }
        public string Count { get; set; }
        public string Price { get; set; }
    }
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class newpreview : ContentPage
    {
        
        public static newtrain tran;
        string date;
        public ObservableCollection<newtrain> li { get; set; }
        public newpreview()
        {
            ///////////////////////////////////////////////////ds/////////////////////////
            InitializeComponent();
            startpl.Placeholder = Resource1.Point_of_departure;
            endpl.Placeholder = Resource1.Point_of_arrival;
            Find.Text = Resource1.Find;
            Find.BorderColor = Color.FromHex(MainPage.when1);
            timer.MinimumDate = DateTime.Now;
            timer.MaximumDate = DateTime.Now.AddDays(30);
            date= DateTime.Now.Year.ToString()+'-'+ DateTime.Now.Month.ToString()+'-'+ DateTime.Now.Day.ToString();
        }
        static string find(ref string pain, string strbegin, string strend)
        {
            string temp = "";
            int n = pain.IndexOf(strbegin);
            if (n > pain.IndexOf("</tr><!-- // b-train -->") && strend == "</li>")
            {
                string b = "</tr><!-- // b-train -->";
                pain = pain.Remove(pain.IndexOf(b), b.Length);
                return "-1";
            }
            if (n == -1)
                return "-1";
            if (strbegin == "&car_type=")
                pain = pain.Remove(0, n + strbegin.Length + 3);
            else
                pain = pain.Remove(0, n + strbegin.Length);
            n = 0;
            while (pain[n] != strend[0])
            {
                temp += pain[n];
                n++;
            }
            pain = pain.Remove(n, strend.Length);
            return temp;
        }
        private static string GET(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url + "?" + Data);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            li = new ObservableCollection<newtrain>(li.OrderBy(i => i.Id));
            TrainsList.ItemsSource = li;
            
        }
        protected override void OnAppearing()
        {
            TrainsList.ItemsSource = li;
            base.OnAppearing();
        }
        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            li = new ObservableCollection<newtrain>(li.OrderByDescending(i => i.Id));
            TrainsList.ItemsSource = li;
        }

        private void MenuItem_Clicked_2(object sender, EventArgs e)
        {
            li = new ObservableCollection<newtrain>(li.OrderBy(i => i.Totaltime));
            TrainsList.ItemsSource = li;
        }

        private void MenuItem_Clicked_3(object sender, EventArgs e)
        {
            li = new ObservableCollection<newtrain>(li.OrderBy(i => i.Starttime));
            TrainsList.ItemsSource = li;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await br.FadeTo(-1, 8000);
            await Task.Run(() => po());
            TrainsList.ItemsSource = li;

        }

        private void po()
        {
            li = new ObservableCollection<newtrain>();
            string pain;
            if (Resource1.Yes == "Yes")
                pain = GET("https://rasp.rw.by/en/route/", "from=" + startpl.Text + "&to=" + endpl.Text + "&date=" + date);
            else
                pain = GET("https://rasp.rw.by/ru/route/", "from=" + startpl.Text + "&to=" + endpl.Text + "&date=" + date);
            task(ref pain);
            
        }
        private  void task(ref string pain)
        {

            string id, type, starttime, startdate, finishtime, finishdate, totaltime, typeplace, count, price,Check="В наличии";
            ObservableCollection<TYPEplace> trainss;
            while (true)
            {
                Check = "В наличии";
                id = find(ref pain, "<small class=\"train_id\">", "</small>");
                if (id == "-1")
                    break;
                type = find(ref pain, "<div class=\"train_description\">", "</div> ");
                starttime = find(ref pain, "<b class=\"train_start-time\">", "</b>");
                startdate = date;
                finishtime = find(ref pain, "<b class=\"train_end-time\">", "</b>");
                finishdate = find(ref pain, "&date=", "\" class=\"");
                totaltime = find(ref pain, "<span class=\"train_time-total\">", "</span>");
                trainss = new ObservableCollection<TYPEplace>();
                while (true)
                { 

                    count = "0";
                    price = "0";
                    typeplace = find(ref pain, "<li class=\"train_note\">", "</li>");
                    if (typeplace == "-1")
                        break;
                    count = find(ref pain, "&car_type=", "</a>");
                    price = find(ref pain, "class=\"train_price\"><span>", "<span>");
                    trainss.Add(new TYPEplace { Typeplace = typeplace, Count = count, Price = price });
                }
                string[] rt = starttime.Split(new char[] { ':' });
                string[] tt = timer.Date.ToString().Split(new char[] { '.' });
                if (trainss.Count == 0)
                    Check = " Билетов Нет в наличии";
                if (Int32.Parse(tt[0])== DateTime.Now.Day && Int32.Parse(rt[0]) < DateTime.Now.Hour ||(Int32.Parse(rt[0]) == DateTime.Now.Hour&& Int32.Parse(rt[1]) < DateTime.Now.Minute))
                    Check = "Отправился";
                li.Add(new newtrain { Id = id, Type = type, Starttime = starttime, Startdate = startdate, Finishdate = finishdate, Finishtime = finishtime, Totaltime = totaltime, Trains = trainss,check=Check }); 
            }
        }
        private void Timer_DateSelected(object sender, DateChangedEventArgs e)
        {
            date = e.NewDate.ToString("yyyy/MM/dd");
            date = date.Replace('.', '-');
        }

        private void MenuItem_Clicked_4(object sender, EventArgs e)
        {
            li = new ObservableCollection<newtrain>(li.OrderBy(i => i.Totaltime));
            TrainsList.BindingContext = li;
            
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            tran = (newtrain)e.SelectedItem;
            if (tran.check == "Отправился" || tran.check == " Билетов Нет в наличии")
                await DisplayAlert(Resource1.Alert, Resource1.No_purchase_possible, Resource1.OK);
            else
            {
                NavigationPage TrainPage = new NavigationPage(new NewTrainPage());
                TrainsList.BindingContext = li;
                await Navigation.PushModalAsync(TrainPage);
            }
        }
        private void SearchUsers(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TrainsList.ItemsSource = (from t in li where t.Id.Contains(text) select t);
            }
            else
            {
                TrainsList.ItemsSource = li;
            }
        }

        
    }
}