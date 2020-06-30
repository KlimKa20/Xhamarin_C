using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace App8
{
    
    //public interface ILocalize
    //{
    //    CultureInfo GetCurrentCultureInfo();
    //}
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public static int IDNOW = 0;
        public static int list = 0;
        public static int when = 0;
        public static string when1;
        public static string when2;

        public MainPage()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            string hg;
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, "file1.json");
            string serialized = JsonConvert.SerializeObject(App.myCollection);
            when1 = "A69272FF";
            when2 = Color.White.ToString();
            if (File.Exists(path))
            {
                hg = File.ReadAllText(path);
                MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(hg);
                if (newMusic.Options.Languge == "English")
                    Resource1.Culture = new CultureInfo("en");
                when1 = newMusic.Options.ColorBar;
                when2 = newMusic.Options.Background;
            }
            sett.Text = Resource1.Settings;
            picker.Title = Resource1.lun;
            // Mainp.Text = Resource1.Mastermain;
            Trainsshedule.Text = Resource1.Masterbase;
            Help.Text = Resource1.MasterComent;
            Online.Text = Resource1.MasterTable;
            LogIn.Text = Resource1.MasterLogI;
            Detail = new NavigationPage(/*new account()*/new newpreview())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)
            };
            IsPresented = false;
        }
        public async void HH()
        {
            MainPage.IDNOW = 2;
            MainPage.list = 2;
            MainPage.when = 0;
            await Navigation.PopAsync();

        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            
           
            Detail = new NavigationPage(new info())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)
            };
            IsPresented = false;
        }

        public  void  Button_Clicked_1(object sender, EventArgs e)
        {
           
            IsPresented = false;
            Detail = new NavigationPage(new LogIn())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)
            };
            
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new newpreview())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)

            };
            IsPresented = false;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new exit())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)

            };
            IsPresented = false;
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new onlinetableview())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)
            };
            
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new main())
            {
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)
            };
            IsPresented = false;
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (picker.Items[picker.SelectedIndex] == "Русский")
                Resource1.Culture = new CultureInfo("ru");
            else
                Resource1.Culture = new CultureInfo("en");
            // Mainp.Text = Resource1.Mastermain;
            sett.Text = Resource1.Settings;
            Trainsshedule.Text = Resource1.Masterbase;
            Help.Text = Resource1.MasterComent;
            Online.Text = Resource1.MasterTable;
            LogIn.Text = Resource1.MasterLogI;
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
                newMusic.Options.Languge= Resource1.lun;
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
                BarBackgroundColor = Color.FromHex(when1),
                BackgroundColor = Color.FromHex(when2)
            };
        }
    }
}
