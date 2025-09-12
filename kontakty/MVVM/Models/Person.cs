using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontakty.MVVM.Models
{
    public class Person
    {
        private string _name;
        private string _surname;
        private int _id;
        public string name { get { return _name; } set { _name = value; } }
        public string surname { get { return _surname; } set { _surname = value; } }
        public int id { get { return _id; } set { _id = value; } }
    }
}
