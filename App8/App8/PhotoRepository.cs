using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App8
{
    public class PhotoRepository
    {
        SQLiteConnection database;
        public PhotoRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Photo>();

        }
        public IEnumerable<Photo> GetItems()
        {
            return (from i in database.Table<Photo>() select i).ToList();

        }
        public Photo GetItem(int id)
        {
            return database.Get<Photo>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Photo>(id);
        }
        public int SaveItem(Photo item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}

