using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Article : IRateAndCopy
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Estimate { get; set; }

        public double Rating {
            get { return Estimate; }
        }

        public Article(Person author, string title, double estimate)
        {
            Author = author;
            Title = title;
            Estimate = estimate;
        }
        public Article()
        {
            Author = new Person();
            Title = "Anonimous";
            Estimate = 0;
        }
        public override string ToString()
        {
            return Author.ToShortString() + "\t" + Title + "\t" + Estimate;
        }
        public virtual object DeepCopy()
        {
            Person new_autor = (Person)Author.DeepCopy();
            return new Article(new_autor, Title, Estimate);
        }
    }
}
