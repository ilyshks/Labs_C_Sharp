using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Edition
    {
        protected string title;
        protected DateTime release;
        protected int circulation;
        public Edition(string title, DateTime release, int circulation)
        {
            this.title = title;
            this.release = release;
            this.circulation = circulation;
        }
        public Edition() : this("Anonymous", new DateTime(1, 1, 1), 0) { }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public DateTime Release
        {
            get { return release; }
            set { release = value; }
        }
        public int Circulation
        {
            get { return circulation; }
            set
            {
                if (value < 0) throw new FormatException("Тираж должен быть положительным числом!");
                circulation = value;
            }
        }
        public virtual object DeepCopy()
        {
            return new Edition(title, release, circulation);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return title == ((Edition)obj).title && release == ((Edition)obj).release && circulation == ((Edition)obj).circulation;
        }
        public static bool operator ==(Edition lhs, Edition rhs) { return lhs.Equals(rhs); }
        public static bool operator !=(Edition lhs, Edition rhs) { return !lhs.Equals(rhs); }
        public override int GetHashCode()
        {
            return (title + release + circulation).GetHashCode();
        }
        public override string ToString()
        {
            return title + " " + release + " " + circulation;
        }
    }
}
