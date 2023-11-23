namespace Lab3
{
    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
    enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }
    delegate void MagazinesChangedHandler<TKey>
        (object source, MagazinesChangedEventArgs<TKey> args);

    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    delegate TKey KeySelector<TKey>(Magazine mg);
    internal class Program
    {
        public static string createKey(Magazine mg)
        {
            return mg.ToShortString();
        }
        public static int inputPositiveInt()
        {
            while (true)
            {
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    if (num > 0) return num;
                    throw new FormatException();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите заново положительное целое число: ");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("Ex #1\n");

            Person person1 = new Person("Ilya", "Gorunov", new DateTime(2004, 4, 5));
            Person person2 = new Person("Alexey", "Kondratenko", new DateTime(2004, 6, 6));
            Person person3 = new Person("Pavel", "Ershov", new DateTime(2004, 12, 9));
            Person person4 = new Person("Ivan", "Ivanov", new DateTime(2004, 10, 10));
            Person person5 = new Person("Fedor", "Sidorov", new DateTime(2004, 1, 2));
            Person person6 = new Person("Semen", "Petrov", new DateTime(2004, 3, 6));

            Article article1 = new Article(person1, "Article #1", 8);
            Article article2 = new Article(person2, "Article #2", 7);
            Article article3 = new Article(person2, "Article #3", 5);
            Article article4 = new Article(person3, "Article #4", 4);
            Article article5 = new Article(person3, "Article #5", 10);

            List<Person> editors1 = new List<Person>() { person4, person5 };
            List<Person> editors2 = new List<Person>() { person1, person6 };
            List<Person> editors3 = new List<Person>() { person1, person5 };
            List<Person> editors4 = new List<Person>() { person2, person3 };

            List<Article> articleList1 = new List<Article>() { article1, article2, article3 };
            List<Article> articleList2 = new List<Article>() { article4, article5};
            List<Article> articleList3 = new List<Article>() { article1, article3 };
            List<Article> articleList4 = new List<Article>() { article1, article2, article5 };

            Magazine magazine1 = new Magazine("Magazine #1", Frequency.Weekly, new DateTime(2023, 8, 10), 1000);
            magazine1.Editors = editors1;
            magazine1.Articles = articleList1;

            Magazine magazine2 = new Magazine("Magazine #2", Frequency.Monthly, new DateTime(2023, 10, 15), 100);
            magazine2.Editors = editors2;
            magazine2.Articles = articleList2;

            Magazine magazine3 = new Magazine("Magazine #3", Frequency.Yearly, new DateTime(2023, 11, 15), 10);
            magazine3.Editors = editors3;
            magazine3.Articles = articleList3;

            Magazine magazine4 = new Magazine("Magazine #4", Frequency.Weekly, new DateTime(2023, 5, 4), 11);
            magazine4.Editors = editors4;
            magazine4.Articles = articleList4;

            KeySelector<string> keySelector = new KeySelector<string>(createKey);

            // Создание двух коллекций
            MagazineCollection<string> MagazineCollection1 = new MagazineCollection<string>(keySelector);
            MagazineCollection1.AddMagazines(magazine1);
            MagazineCollection1.AddMagazines(magazine2);
            Console.WriteLine("Коллекция журналов: " + "\n" + MagazineCollection1.ToString());
            Console.WriteLine("\n");

            MagazineCollection<string> MagazineCollection2 = new MagazineCollection<string>(keySelector);
            MagazineCollection2.AddMagazines(magazine3);
            MagazineCollection2.AddMagazines(magazine4);
            Console.WriteLine("Коллекция журналов: " + "\n" + MagazineCollection2.ToString());



            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("Ex #2\n");
            Listener listener = new Listener();

            MagazineCollection1.MagazinesChanged += listener.MagazinesChanged;
            MagazineCollection2.MagazinesChanged += listener.MagazinesChanged;
            Console.WriteLine("Подписали Listener на события");

            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("Ex #3\n");

            Console.WriteLine("Вносим изменения в MagazineCollection<string>...");
            MagazineCollection1.AddMagazines(magazine3);
            MagazineCollection2.AddMagazines(magazine1, magazine2);

            magazine4.Release = new DateTime(2023, 1, 1);
            magazine3.Circulation = 2000;
            magazine2.AddArticles(new Article[] { article3, article2});
            magazine1.Circulation++;

            MagazineCollection1.Replace(magazine1, magazine4);

            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("Ex #4\n");
            Console.WriteLine(listener.ToString());

            Console.Read();
        }
    }
}
