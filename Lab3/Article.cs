
namespace Lab3
{
    internal class Article : IRateAndCopy, IComparable, IComparer<Article>
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Estimate { get; set; }

        public double Rating
        {
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

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            Article? other = obj as Article;
            if (other != null)
                return this.Title.CompareTo(other.Title);
            else
                return 1;
        }

        public int Compare(Article? x, Article? y)
        {
            if (x == null || y == null) return 1;
            return x.Author.Surname.CompareTo(y.Author.Surname);
        }
    }
}
