using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;

namespace App8
{
   public class TrainRepository
    {
        SQLiteConnection database;
        public TrainRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Train>();

        }
        public IEnumerable<Train> GetItems()
        {
            return (from i in database.Table<Train>() select i).ToList();

        }
        public Train GetItem(int id)
        {
            return database.Get<Train>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Train>(id);
        }
        public int SaveItem(Train item)
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
