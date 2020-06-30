using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Linq;
using Xamarin.Forms;

namespace App8
{
    public class ComentRepository
    {
        SQLiteConnection database;
        public ComentRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Comentar>();

        }
        public IEnumerable<Comentar> GetItems()
        {
            return (from i in database.Table<Comentar>() select i).ToList();

        }
        public Comentar GetItem(int id)
        {
            return database.Get<Comentar>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Comentar>(id);
        }
        public int SaveItem(Comentar item)
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
