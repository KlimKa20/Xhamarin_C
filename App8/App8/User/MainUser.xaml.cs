using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainUser : MasterDetailPage
    {
        public MainUser()
        {
            InitializeComponent();
            string hg;
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, "file1.json");
            string serialized = JsonConvert.SerializeObject(App.myCollection);
            if (File.Exists(path))
            {
                hg = File.ReadAllText(path);
                MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(hg);
                if (newMusic.Options.Languge == "English")
                    Resource1.Culture = new CultureInfo("en");
                MainPage.when1 = newMusic.Options.ColorBar;
            }
            ex.Text = Resource1.Exit;
            picker.Title = Resource1.lun;
            Masterbase.Text = Resource1.Masterbase;
            MasterComent.Text = Resource1.MasterComent;
            sett.Text = Resource1.Settings;
            // Mastermain.Text = Resource1.Mastermain;
            MasterComent.Text = Resource1.MasterComent;
            MAsterTable.Text = Resource1.MasterTable;
            account.Text = Resource1.personal_account;
            Detail = new NavigationPage(new newpreview())
            {
                BackgroundColor = Color.FromHex(MainPage.when2),
                BarBackgroundColor = Color.FromHex(MainPage.when1)
            };
            IsPresented = false;
            //TabBar
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new info())
            {
                BackgroundColor = Color.FromHex(MainPage.when2),
                BarBackgroundColor = Color.FromHex(MainPage.when1)
            };
            IsPresented = false;
        }

        public void Button_Clicked_1(object sender, EventArgs e)
        {
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new newpreview())
            {
                BackgroundColor = Color.FromHex(MainPage.when2),
                BarBackgroundColor = Color.FromHex(MainPage.when1)
            };
            IsPresented = false;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new exit())
            {
                BackgroundColor = Color.FromHex(MainPage.when2),
                BarBackgroundColor = Color.FromHex(MainPage.when1)
            };
            IsPresented = false;
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new onlinetableview())
            {
                BackgroundColor = Color.FromHex(MainPage.when2),
                BarBackgroundColor = Color.FromHex(MainPage.when1)
            };
            IsPresented = false;
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {

        }
        public void sd()
        {

        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker.Items[picker.SelectedIndex] == "Русский")
                Resource1.Culture = new CultureInfo("ru");
            else
                Resource1.Culture = new CultureInfo("en");
            ex.Text = Resource1.Exit;
            Masterbase.Text = Resource1.Masterbase;
            MasterComent.Text = Resource1.MasterComent;
            sett.Text = Resource1.Settings;
           // Mastermain.Text = Resource1.Mastermain;
            MasterComent.Text = Resource1.MasterComent;
            MAsterTable.Text = Resource1.MasterTable;
            account.Text = Resource1.personal_account;
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, "file1.json");
            if (!File.Exists(path))
            {
                App.myCollection.Options.Languge = Resource1.lun;
                string serialized = JsonConvert.SerializeObject(App.myCollection);
                File.AppendAllText(path, serialized);
            }
            else
            {
                MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(File.ReadAllText(path));
                newMusic.Options.Languge = Resource1.lun;
                string serializednew = JsonConvert.SerializeObject(newMusic);
                File.Delete(path);
                File.AppendAllText(path, serializednew);
            }
        }
        private void Sett_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new View1())
            {
                BarBackgroundColor = Color.FromHex(MainPage.when1),
                BackgroundColor = Color.FromHex(MainPage.when2)
            };
        }

        private void Ex_Clicked(object sender, EventArgs e)
        {
            MainPage.list = 0;
            Navigation.PopModalAsync();
        }
    }
}