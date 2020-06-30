using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Plugin.Permissions;
using System.Globalization;
using Newtonsoft.Json;
namespace App8
{
    public class MyMusic
    {
        private MyMusic myMusic;
        public Option Options { get; set; }
    }

    public class Option
    {
        public string Languge { get; set; }
        public string ColorBar { get; set; }

        public string Background { get; set; }

    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Table("Photo")]
    public class Photo
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [ Column("ph")]
        public string ph { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Table("Train")]
    public class Train
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("Поезд")]
        public string Number { get; set; }
        [Column("Станцияотправления")]
        public string PlaceTr { get; set; }
        [Column("Станцияприбытия")]
        public string PlaceAr { get; set; }
        [Column("Времяприб.")]
        public string TimeAr { get; set; }
        [Column("Времяотпр.")]
        public string TimeTr { get; set; }
        [Column("Колличество")]
        public int Count { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("Login")]
        public string Login { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Trains")]
        public string Trains { get; set; }
        [Column("Mail")]
        public string Mail { get; set; }
        [Column("Telephone")]
        public string Telephone { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Table("Coment")]
    public class Comentar
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("coment")]
        public string com { get; set; }
        [Column("user")]
        public string User { get; set; }
    }

    public partial class App : Application
    {
        public static MyMusic myCollection = new MyMusic();

        public const string DATABASE_NAME_PH = "Photos.db";
        public static PhotoRepository databaseph;
        public static PhotoRepository Databaseph

        {
            get
            {
                if (databaseph == null)
                {
                    databaseph = new PhotoRepository(DATABASE_NAME_PH);
                }
                return databaseph;
            }
        }
        public const string DATABASE_NAME_COMENT = "coment.db";
        public static ComentRepository databasecoment;
        public static ComentRepository Databasecoment

        {
            get
            {
                if (databasecoment == null)
                {
                    databasecoment = new ComentRepository(DATABASE_NAME_COMENT);
                }
                return databasecoment;
            }
        }
        public const string DATABASE_NAME = "Trainf.db";
        public static TrainRepository database;
        public static TrainRepository Database

        {
            get
            {
                if (database == null)
                {
                    database = new TrainRepository(DATABASE_NAME);
                }
                return database;
            }
        }
        public const string DATABASE_NAME_USER = "Users.db";
        public static UserRepository databaseuser;
        public static UserRepository Databaseuser
        {
            get
            {
                if (databaseuser == null)
                {
                    databaseuser = new UserRepository(DATABASE_NAME_USER);
                }
                return databaseuser;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            myCollection.Options = new Option();
            
        }
        public interface bes
        {
             void FingerPrintAuthenticationExample();
        }
        public interface ILocalize
        {
            CultureInfo GetCurrentCultureInfo();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
