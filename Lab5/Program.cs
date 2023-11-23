using static System.Net.Mime.MediaTypeNames;

namespace Lab5
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
        static void Main(string[] args)
        {

            Console.WriteLine("========================================================================================================================\n");
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

            List<Article> articleList1 = new List<Article>() { article1, article2, article3, article4, article5 };

            Magazine magazine = new Magazine("Magazine #1", Frequency.Weekly, new DateTime(2023, 10, 10), 1000);
            magazine.Editors = editors1;
            magazine.Articles = articleList1;

            Console.WriteLine(magazine.ToString());

            Magazine magazine2 = magazine.DeepCopySerialize();
            Console.WriteLine("Копия: " + magazine2.ToString());

            Console.WriteLine("========================================================================================================================\n");
            Console.WriteLine("Ex #2\n");

            string filename;
            string filepath = "D:\\Visual Studio\\C# labs\\Lab5\\files\\";
            while (true)
            {
                Console.WriteLine("Введите имя файла: ");
                filename = Console.ReadLine();
                if (filename == null || filename.Length == 0)
                {
                    Console.WriteLine("имя файла должно быть не пустым");
                    continue;
                }
                else
                {
                    break;
                }
            }
            if (File.Exists(filepath + filename))
            {
                Console.WriteLine("Такой файл уже существует");
                Console.WriteLine(magazine.Load(filepath + filename));
                Console.WriteLine(magazine.ToString());
            }
            else
            {
                Console.WriteLine("Такого файла нет. Он будет создан.");
            }

            Console.WriteLine("========================================================================================================================\n");
            Console.WriteLine("Ex #3\n");
            Magazine s3 = new Magazine();
            Console.WriteLine("Студент новый:" + s3.ToString());
            Console.WriteLine("========================================================================================================================\n");
            Console.WriteLine("Ex #4\n");
            s3.AddFromConsole();
            Console.WriteLine(s3.Save(filepath + filename));
            Console.WriteLine(s3.ToString());

            Console.WriteLine("========================================================================================================================\n");
            Console.WriteLine("Ex #5\n");
            Console.WriteLine(Magazine.Load(filepath + filename, s3));
            s3.AddFromConsole();
            Console.WriteLine(Magazine.Save(filepath + filename, s3));
            Console.WriteLine(s3.ToString());


            Console.Read();
        }
    }
}