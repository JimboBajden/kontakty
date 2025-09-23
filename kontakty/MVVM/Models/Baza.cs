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
       
        private SQLiteConnection conucter = new SQLiteConnection($"Data Source=C:\\Users\\jimbo\\Desktop\\maui\\kontakty\\kontakty\\baza\\baza.db; version=3");
        //C:\\Users\\jimbo\\Desktop\\maui\\kontakty\\kontakty\\baza\\baza.db
        //C:\\Users\\5TP_Curzydlo_Przemek\\Source\\Repos\\kontakty\\kontakty\\baza\\baza.db
        public Baza() { conucter.Open(); }
        public int PageCount()
        {
            SQLiteCommand cmd = conucter.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM osoby";
            int test = System.Convert.ToInt32(cmd.ExecuteScalar());
            return (test+4)/5;
        }
        public ObservableCollection<Person> Search(string text)
        {
            if (string.IsNullOrEmpty(text)) { return null; }
            SQLiteCommand cmd = conucter.CreateCommand();
            cmd.CommandText = $"SELECT * FROM osoby WHERE (name || surname) LIKE '%{text}%'";
            SQLiteDataReader tmp = cmd.ExecuteReader();
            ObservableCollection<Person> People = new ObservableCollection<Person>();

            while (tmp.Read())
            {
                People.Add(new Person { id = System.Convert.ToInt32(tmp[0]), name = tmp[1].ToString(), surname = tmp[2].ToString() });
            }
            return People;
            
        }
        public void DeleteMultiple(List<int> id)
        {
            SQLiteCommand cmd = conucter.CreateCommand();
            string idList = string.Join(",", id);
            cmd.CommandText = $"DELETE FROM osoby WHERE id IN ({idList})";
            cmd.ExecuteNonQuery();
        }
        public void update(int id , string name , string surname)
        {
            SQLiteCommand cmd = conucter.CreateCommand();
            cmd.CommandText = $"UPDATE osoby SET name='{name}' , surname='{surname}' WHERE id = '{id}'";
            cmd.ExecuteNonQuery();  
        }
        public ObservableCollection<Person> tmp()
        {

            conucter.Open();
            SQLiteCommand cmd = conucter.CreateCommand();
                cmd.CommandText = "SELECT * from osoby";

                SQLiteDataReader tmp = cmd.ExecuteReader();
                ObservableCollection<Person> People = new ObservableCollection<Person>();
                
                while (tmp.Read())
                {
                    People.Add(new Person { name = tmp[1].ToString(), surname = tmp[2].ToString() });
                }
                return People;

        }

        public void DeleteById(int id)
        {
            SQLiteCommand cmd = conucter.CreateCommand();
            cmd.CommandText = $"DELETE FROM osoby WHERE id = {id}";
            cmd.ExecuteNonQuery();
        }
        public void Add(string name , string surname)
        {
            SQLiteCommand cmd = conucter.CreateCommand();
            cmd.CommandText = $"INSERT INTO osoby (name , surname) VALUES ('{name}' , '{surname}')";
            cmd.ExecuteNonQuery();
        }
        public ObservableCollection<Person> GetPeople(int c) 
        {
            SQLiteCommand cmd = conucter.CreateCommand();
            cmd.CommandText = $"SELECT * FROM osoby LIMIT 5 OFFSET {(c-1) * 5}";
            SQLiteDataReader tmp = cmd.ExecuteReader();
            ObservableCollection<Person> People = new ObservableCollection<Person>();

            while (tmp.Read())
            {
                People.Add(new Person { id = System.Convert.ToInt32( tmp[0]) ,  name = tmp[1].ToString(), surname = tmp[2].ToString() });
            }
            return People;

        }
        //public ObservableCollection<Person>
    }
}
