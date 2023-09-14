using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Article
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Estimate { get; set; }
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
            return Author + "\t" + Title + "\t" + Estimate;
        }
    }
}
