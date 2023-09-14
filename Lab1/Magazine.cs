using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Magazine
    {
        private string title;
        private Frequency frequency;
        private DateTime releaseDate;
        private int circulation;
        private Article[] articles;
        public Magazine(string title, Frequency frequency, DateTime releaseDate, int circulation)
        {
            this.title = title;
            this.frequency = frequency;
            this.releaseDate = releaseDate;
            this.circulation = circulation;
            this.articles = new Article[0];
        }
        public Magazine()
        {
            title = "Nothing";
            frequency = Frequency.Weekly;
            releaseDate = new DateTime(1, 1, 1);
            circulation = 0;
            articles = new Article[0];
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public Frequency Frequency
        { 
            get { return frequency; }
            set { frequency = value; }
        }
        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }
        public int Circulation
        {
            get { return circulation; }
            set {circulation = value;}
        }
        public Article[] Articles
        {
            get { return articles; }
            set 
            {
                articles = new Article[value.Length];
                for (int i = 0; i < value.Length; i++)
                    articles[i] = value[i]; 
            }
        }
        public double AverageValue
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < articles.Length; i++)
                {
                    sum += articles[i].Estimate;
                }
                return sum / articles.Length;
            }
        }
        public bool this[Frequency index]
        {
            get{ return frequency == index; }
        }
        public void AddArticles(Article[] new_articles)
        {
            Article[] tmp = articles;
            articles = new Article[articles.Length + new_articles.Length];
            for(int i = 0; i < articles.Length; i++)
            {
                if (i < tmp.Length) articles[i] = tmp[i];
                else articles[i] = new_articles[i - tmp.Length];
            }
        }
        public override string ToString()
        {
            string data = $"{title}\nПереодичность: {frequency}\nДата публикации: {releaseDate}\nТираж: {circulation}\nСтатьи:\n";
            foreach (Article article in articles)
                data += article.ToString() + "\n";
            return data;
        }
        public virtual string ToShortString()
        {
            return $"{title}\nПереодичность: {frequency}\nДата публикации: {releaseDate}\nТираж: {circulation}\nСредний рейтинг статей: {AverageValue}";
        }

    }
}
