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
            Console.WriteLine("Task #1\n");

            // Создание объекта Magazine и сортировка
            Person person1 = new Person("Илья", "Горюнов", new DateTime(2004, 4, 5));
            Person person2 = new Person("Василий", "Сидоров", new DateTime(2004, 10, 6));
            Person person3 = new Person("Иван", "Иванов", new DateTime(2004, 12, 15));

            Article article1 = new Article(person1, "Статья 1", 4.5);
            Article article12 = new Article(person1, "Статья 2", 4.6);
            Article article2 = new Article(person2, "Статья 3", 4);
            Article article22 = new Article(person2, "Статья 4", 5.0);
            Article article3 = new Article(person3, "Статья 5", 4.7);

            List<Person> editors = new List<Person> { person1 };
            List<Article> articles = new List<Article> { article1, article12, article2, article22, article3 };

            Magazine m1 = new Magazine("Журнал 1", Frequency.Weekly, new DateTime(2020, 4, 3), 1000);
            m1.Editors = editors;
            m1.Articles = articles;

            Console.WriteLine("Журнал #1:\n" + m1.ToString());
            Console.WriteLine("========================================================================================================================");
            m1.SortByTitle();
            Console.WriteLine("Сортировка по названию статьи:\n" + m1.ToString());
            Console.WriteLine("========================================================================================================================");
            m1.SortByAuthorSurname();
            Console.WriteLine("Сортировка по фамилии автора:\n" + m1.ToString());
            Console.WriteLine("========================================================================================================================");
            m1.SortByEstimate();
            Console.WriteLine("Сортировка по рейтингу статьи:\n" + m1.ToString());

            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("Task #2\n");
            KeySelector<String> defineKey = new KeySelector<String>(createKey);
            MagazineCollection<String> magazineCollection = new MagazineCollection<String>(defineKey);

            Magazine m2 = new Magazine("Журнал 2", Frequency.Monthly, new DateTime(2020, 5, 15), 500);
            m2.Editors = new List<Person> { person1, person2 };
            m2.Articles = new List<Article> { article12, article22, article3};

            Magazine m3 = new Magazine("Журнал 3", Frequency.Yearly, new DateTime(2020, 1, 1), 100);
            m3.Editors = new List<Person> { person1, person3 };
            m3.Articles = new List<Article> { article1, article12, article3 };

            magazineCollection.AddMagazines(m1, m2, m3);
            Console.WriteLine("MagazineCollection<string>:");
            Console.WriteLine(magazineCollection.ToString());
            Console.WriteLine("========================================================================================================================");
            
            Console.WriteLine("Task #3\n");
            Console.WriteLine("Максимальное значение среднего рейтинга статей: " + magazineCollection.MaxAverageValue);

            Console.WriteLine("\nВыбор журналов с ежемесячной периодичностью выхода:");
            foreach (var item in magazineCollection.FrequencyGroup(Frequency.Monthly))
            {
                Console.WriteLine(item.Key);
                Console.WriteLine();
            }
            Console.WriteLine("Группировка элементов коллекции по периодичности выхода: ");
            foreach (var group in magazineCollection.ToGroup)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine();
            }
            Console.WriteLine("========================================================================================================================");
            
            Console.WriteLine("Task #4\n");
            Console.WriteLine("Тестируем поиск элементов в коллекциях.");
            Console.Write("Введите кол-во элементов: ");
            int size = inputPositiveInt();
            TestCollections<Edition, Magazine> testCollections = new TestCollections<Edition, Magazine>(size, TestCollections<Edition, Magazine>.GenerateElement);
            testCollections.searchListOfTKeys();
            testCollections.searchListOfString();
            testCollections.searchKeyInDictOfTKeys();
            testCollections.searchKeyInDictOfString();
            testCollections.searchValueInDictOfTKeys();
            testCollections.searchValueInDictOfString();
        }
    }
}
