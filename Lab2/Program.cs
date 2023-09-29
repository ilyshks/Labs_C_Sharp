using System.Collections;
using System.Data.SqlTypes;

namespace Lab2
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
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1
            Edition ed1 = new Edition("Инверсия", new DateTime(2004, 3, 5), 1000);
            Edition ed2 = new Edition("Инверсия", new DateTime(2004, 3, 5), 1000);
            Console.WriteLine("Равны ли ссылки?\t" + ReferenceEquals(ed1, ed2));
            Console.WriteLine("Равны ли объекты?\t" + (ed1 == ed2));
            Console.WriteLine("Значения хеш-кодов: " + ed1.GetHashCode() + "\t" + ed2.GetHashCode() + "\n");

            // 2
            try
            {
                ed1.Circulation = -23;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message + "\n");
            }

            // 3
            Magazine magazine = new Magazine("MIET NEWS", Frequency.Weekly, new DateTime(2004, 5, 5), 2345);
            ArrayList editors = new ArrayList();
            ArrayList articles = new ArrayList();

            editors.Add(new Person("Сергей", "Петров", new DateTime(1998, 6, 7)));
            editors.Add(new Person("Иван", "Сидоров", new DateTime(1997, 10, 8)));

            articles.Add(new Article(new Person("Андрей", "Богданов", new DateTime(2001, 2, 12)), "Школа Актива", 4.9));
            articles.Add(new Article(new Person("Егор", "Зябликов", new DateTime(2001, 3, 15)), "Неделя IT", 5.0));
            articles.Add(new Article(new Person("Фёдор", "Васильев", new DateTime(2002, 1, 10)), "Ярмарка со вкусом", 4.8));

            magazine.Editors = editors;
            magazine.Articles = articles;

            Console.WriteLine(magazine);

            // 4
            Console.WriteLine("Свойство типа Edition:");
            Console.WriteLine(magazine.EditionProperty);

            // 5
            Console.WriteLine();
            Magazine magazine_copy = (Magazine)magazine.DeepCopy();
            magazine.Articles.Add(new Article(new Person("Пётр", "Сверидов", new DateTime(2000, 3, 4)), "ДоброЦентр", 5.0));
            Console.WriteLine(magazine_copy);
            Console.WriteLine(magazine);

            // 6
            Console.WriteLine("Номер 6");
            foreach (var item in magazine.GetEnumerator(4.8))
                Console.WriteLine(item);
            Console.WriteLine();

            // 7
            Console.WriteLine("Номер 7");
            foreach (var item in magazine.GetEnumerator("Ярмарка"))
                Console.WriteLine(item);
            Console.WriteLine();

            // 8
            Console.WriteLine("Номер 8");
            Console.WriteLine("Статьи, авторы которых не редакторы:");
            foreach (var item in magazine)
                Console.WriteLine(item);
            Console.WriteLine();

            // 9
            Console.WriteLine("Номер 9");
            Console.WriteLine("Статьи, авторы которых и редакторы одновременно:");
            int cnt = 0;
            foreach (var item in magazine.GetAuthorsAndEditors())
            {
                Console.WriteLine(item);
                cnt++;
            }
            if (cnt == 0) Console.WriteLine("Таких нет!");
            Console.WriteLine();

            // 10
            Console.WriteLine("Номер 10");
            Console.WriteLine("Только редакторы:");
            cnt = 0;
            foreach (var item in magazine.GetOnlyEditors())
            {
                Console.WriteLine(item);
                cnt++;
            }
            if (cnt == 0) Console.WriteLine("Таких нет!");
            Console.WriteLine();
        }
    }
}