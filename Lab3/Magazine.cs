using System.Collections;

namespace Lab3
{
    internal class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency frequency;
        private List<Person>? editors;
        private List<Article>? articles;
        public Magazine(string title, Frequency frequency, DateTime release, int circulation) : base(title, release, circulation)
        {
            this.frequency = frequency;
            editors = new List<Person>();
            articles = new List<Article>();
        }
        public Magazine() : base()
        {
            frequency = Frequency.Weekly;
            editors = new List<Person>();
            articles = new List<Article>();
        }
        public double AverageValue
        {
            get
            {
                if (articles == null || articles.Count == 0) return 0;
                double sum = 0; int cnt = 0;
                for (int i = 0; i < articles.Count; i++)
                {
                    if (articles[i] is Article article)
                    {
                        sum += article.Estimate;
                        cnt++;
                    }
                }
                return sum / cnt;
            }
        }
        public Frequency Frequency
        {
            get
            {
                return frequency;
            }
        }
        public List<Article>? Articles
        {
            get { return articles; }
            set
            {
                if (value != null && value is List<Article> other)
                {
                    articles = new List<Article>();
                    for (int i = 0; i < other.Count; i++)
                    {
                        articles.Add(item: (Article)other[i].DeepCopy());
                    }
                }
            }
        }
        public List<Person>? Editors
        {
            get { return editors; }
            set
            {
                if (value != null && value is List<Person> other)
                {
                    editors = new List<Person>();
                    for (int i = 0; i < other.Count; i++)
                    {
                        editors.Add(item: (Person)other[i].DeepCopy());
                    }
                }
            }
        }

        public double Rating
        {
            get { return AverageValue; }
        }
        public Edition EditionProperty
        {
            get { return new Edition(Title, Release, Circulation); }
            set
            {
                Title = value.Title;
                Release = value.Release;
                Circulation = value.Circulation;
            }
        }
        public override object DeepCopy()
        {
            Magazine magazine = new Magazine(title, frequency, release, circulation);
            magazine.Articles = new List<Article>();
            if (articles != null)
                magazine.Articles.AddRange(articles);
            magazine.Editors = new List<Person>();
            if (editors != null)
                magazine.Editors.AddRange(editors);
            return magazine;
        }

        public void AddArticles(Article[] new_articles)
        {
            if (new_articles == null) return;
            if (articles == null)
                articles = new List<Article>();
            for (int i = 0; i < new_articles.Length; i++)
            {
                if (new_articles[i] is Article article)
                    articles.Add((Article)article.DeepCopy());
            }

        }
        public void AddEditors(Person[] new_editors)
        {
            if (new_editors == null) return;
            if (editors == null)
                editors = new List<Person>();
            for (int i = 0; i < new_editors.Length; i++)
            {
                if (new_editors[i] is Person editor)
                    editors.Add((Person)editor.DeepCopy());
            }

        }
        public override string ToString()
        {
            string data = base.ToString() + "\n";
            data += "Переодичность: " + frequency + "\n";
            if (editors != null)
            {
                data += "Редакторы:\n";
                for (int i = 0; i < editors.Count; i++)
                    if (editors[i] is Person editor)
                        data += editor.ToShortString() + "\n";
            }
            if (articles != null)
            {
                data += "Авторы:\n";
                for (int i = 0; i < articles.Count; i++)
                    if (articles[i] is Article article)
                        data += article.ToString() + "\n";
            }
            return data;
        }
        public virtual string ToShortString()
        {
            string data = base.ToString() + "\n";
            data += "Переодичность: " + frequency + "\n";
            data += "Средний рейтинг статей: " + AverageValue + "\n";
            return data;
        }

        public IEnumerable GetEnumerator(double minEstimate)
        {
            if (articles != null)
            {
                for (int i = 0; i < articles.Count; i++)
                    if (articles[i] is Article article && article.Estimate > minEstimate)
                        yield return article;
            }
        }
        public IEnumerable GetEnumerator(String substring)
        {
            if (articles != null)
            {
                for (int i = 0; i < articles.Count; i++)
                    if (articles[i] is Article article && article.Title.Contains(substring))
                        yield return article;
            }
        }
        private class MagazineEnumerator : IEnumerator
        {
            public List<Article> articlelist;
            public List<Person> editorlist;
            int position = -1;

            public MagazineEnumerator(List<Article> artlist, List<Person> edlist)
            {
                articlelist = artlist;
                editorlist = edlist;
            }

            public bool MoveNext()
            {
                position++;
                if (articlelist != null && editorlist != null)
                {
                    while (position < articlelist.Count)
                    {
                        for (int i = 0; i < editorlist.Count; i++)
                        {
                            if (articlelist[position] is Article article && editorlist[i] is Person person)
                                if (article.Author != person)
                                    return true;
                        }
                        position++;
                    }
                }

                return false;
            }
            public void Reset()
            {
                position = -1;
            }
            public object? Current
            {
                get
                {
                    return articlelist[position];
                }
            }
        }

        public IEnumerator GetEnumerator()
        {

            return new MagazineEnumerator(articles, editors);
        }
        public IEnumerable GetAuthorsAndEditors()
        {
            if (articles != null && editors != null)
            {
                for (int i = 0; i < articles.Count; i++)
                    if (articles[i] is Article article)
                    {
                        for (int j = 0; j < editors.Count; j++)
                            if (editors[j] is Person person && article.Author == person)
                                yield return article;
                    }
            }
            if (articles != null && editors == null)
            {
                foreach (Article article in articles)
                    yield return article;
            }
        }
        public IEnumerable GetOnlyEditors()
        {
            if (articles != null && editors != null)
            {
                for (int i = 0; i < editors.Count; i++)
                {
                    if (editors[i] is Person person)
                    {
                        bool flag = true;
                        for (int j = 0; j < articles.Count; j++)
                            if (articles[j] is Article article && article.Author == person)
                            {
                                flag = false;
                                break;
                            }
                        if (flag) yield return person;
                    }
                }
            }
            if (articles == null && editors != null)
            {
                foreach (Person person in editors)
                    yield return person;
            }
        }
        public void SortByTitle()
        {
            if (articles != null) articles.Sort(new ArticleComparer().CompareTitle);
        }
        public void SortByAuthorSurname()
        {
            if (articles != null) articles.Sort(new ArticleComparer().CompareAuthorSurname);
        }
        public void SortByEstimate()
        {
            if (articles != null) articles.Sort(new ArticleComparer().Compare);
        }
    }
}