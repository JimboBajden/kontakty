using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Collections.ObjectModel;


namespace kontakty.MVVM.Models
{
    internal class Baza
    {
        public SQLiteConnection aaaaa = new SQLiteConnection($"Data Source= C:\\Users\\jimbo\\Desktop\\baza.db; version=3");

        public ObservableCollection<Person> tmp()
        {
            
                aaaaa.Open();
                SQLiteCommand cmd = aaaaa.CreateCommand();
                cmd.CommandText = "SELECT * from osoby";

                SQLiteDataReader tmp = cmd.ExecuteReader();
                ObservableCollection<Person> People = new ObservableCollection<Person>();
                
                    while (tmp.Read())
                    {
                        People.Add(new Person { name = tmp[1].ToString(), surname = tmp[2].ToString() });
                    }
                return People;

        }
    }
}
