using System;
using Xamarin.Forms;
using System.IO;
using App8.iOS;
[assembly: Dependency(typeof(SQLiteApp))]
namespace App8.iOS
{
    class SQLiteApp: ISQLite
    { public SQLiteApp() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            return path;
        }
    }
}