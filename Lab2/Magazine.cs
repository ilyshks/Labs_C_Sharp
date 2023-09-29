using System;
using System.Collections;

namespace Lab2
{
    internal class Magazine: Edition, IRateAndCopy, IEnumerable
    {
        private Frequency frequency;
        private ArrayList? editors;
        private ArrayList? articles;
        public Magazine(string title, Frequency frequency, DateTime release, int circulation) : base(title, release, circulation)
        {
            this.frequency = frequency;
            editors = new ArrayList();
            articles = new ArrayList();
        }
        public Magazine() : base()
        {
            frequency = Frequency.Weekly;
            editors = new ArrayList();
            articles = new ArrayList();
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
        public ArrayList? Articles
        {
            get { return articles; }
            set 
            {
                if (value != null && value is ArrayList other)
                {
                    articles = new ArrayList();
                    for (int i = 0; i < other.Count; i++)
                    {
                        if (other[i] is Article article)
                            articles.Add(article.DeepCopy());
                    }
                }
            }
        }
        public ArrayList? Editors
        {
            get { return editors; }
            set
            {
                if (value != null && value is ArrayList other)
                {
                    editors = new ArrayList();
                    for (int i = 0; i < other.Count; i++)
                    {
                        if (other[i] is Person editor)
                            editors.Add(editor.DeepCopy());
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
            magazine.Articles = articles;
            magazine.Editors = editors;
            return magazine;
        }

        public void AddArticles(Article[] new_articles)
        {
            if (new_articles == null) return;
            if (articles == null)
                articles = new ArrayList();
            for (int i = 0; i < new_articles.Length; i++)
            {
                if (new_articles[i] is Article article)
                    articles.Add(article.DeepCopy());
            }

        }
        public void AddEditors(Person[] new_editors)
        {
            if (new_editors == null) return;
            if (editors == null)
                editors = new ArrayList();
            for (int i = 0; i < new_editors.Length; i++)
            {
                if (new_editors[i] is Person editor)
                    editors.Add(editor.DeepCopy());
            }

        }
        public override string ToString()
        {
            string data = base.ToString() + "\n";
            data += "Переодичность: " + frequency + "\n";
            if (editors != null) {
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
            if (articles != null) {
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
            public ArrayList articlelist;
            public ArrayList editorlist;
            int position = -1;

            public MagazineEnumerator(ArrayList artlist, ArrayList edlist)
            {
                articlelist = artlist;
                editorlist = edlist;
            }
            private IEnumerator getEnumerator()
            {
                return this;
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
                Reset();
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
            if (articles == null) return (IEnumerator) new ArrayList();
            if (editors == null) return (IEnumerator) articles;
            return new MagazineEnumerator(artlist: articles, editors);
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
    }
}
