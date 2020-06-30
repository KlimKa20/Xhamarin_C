using System;
using App8.Droid;
using System.IO;
using Xamarin.Forms;
using Android.Content;

[assembly: Dependency(typeof(SQLite_Android))]
namespace App8.Droid
{
        public class SQLite_Android : ISQLite
        {
            public SQLite_Android() { }
            public string GetDatabasePath(string sqliteFilename)
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);
                // копирование файла из папки Assets по пути path
                if (!File.Exists(path))
                {
                    // получаем контекст приложения
                    Context context = Android.App.Application.Context;
                    var dbAssetStream = context.Assets.Open(sqliteFilename);

                    var dbFileStream = new FileStream(path, System.IO.FileMode.OpenOrCreate);
                    var buffer = new byte[1024];

                    int b = buffer.Length;
                    int length;

                    while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                    {
                        dbFileStream.Write(buffer, 0, length);
                    }

                    dbFileStream.Flush();
                    dbFileStream.Close();
                    dbAssetStream.Close();
                }

                return path;
            }
        }
    }