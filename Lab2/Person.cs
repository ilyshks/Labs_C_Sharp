using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Person
    {
        private string name;
        private string surname;
        private DateTime birthday;
        public Person(string name, string surname, DateTime birthday)
        {
            this.name = name;
            this.surname = surname;
            this.birthday = birthday;
        }
        public Person() : this("Иван", "Иванов", new DateTime(2004, 3, 1)) { }

        static bool CheckFio(string str)
        {
            string invalid_symbols = "0123456789!,.?/%$#@*()-+=";

            if (str.Length != 0 || (str[0] >= 'A' && str[0] <= 'Z') || (str[0] >= 'А' && str[0] <= 'Я'))
            {

                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = 0; j < invalid_symbols.Length; j++)
                        if (str[i] == str[j]) return false;
                }
                return true;

            }
            else return false;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (CheckFio(value)) this.name = value;
                else throw new FormatException("Неверный формат данных!");
            }
        }
        public string Surname
        {
            get { return this.surname; }
            set
            {
                if (CheckFio(value)) this.surname = value;
                else throw new FormatException("Неверный формат данных!");
            }
        }
        public DateTime Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }

        public int ChangeYear
        {
            get { return birthday.Year; }
            set { birthday = new DateTime(value, birthday.Month, birthday.Day); }
        }
        public override string ToString()
        {
            return name + " " + surname + "\t" + birthday.ToShortDateString();
        }
        public virtual string ToShortString()
        {
            return name + " " + surname;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (name.Equals(((Person)obj).Name) && surname.Equals(((Person)obj).Surname) && birthday.Equals(((Person)obj).Birthday)) 
                return true;
            return false;
        }
        public static bool operator ==(Person left, Person right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !left.Equals(right);
        }
        public override int GetHashCode()
        {
            return (name + surname + birthday.ToString()).GetHashCode();
        }
        public virtual object DeepCopy()
        {
            return new Person(name[..], surname[..], birthday);
        }
    }
}
