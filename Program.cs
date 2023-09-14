using System.Diagnostics;

namespace Lab1
{
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
            // #1
            Magazine magazine = new Magazine("MIET NEWS", Frequency.Weekly, new DateTime(2000, 1, 1), 1000);
            Article[] artcls = new Article[3];
            artcls[0] = new Article(new Person("Сергей", "Петров", new DateTime(1987, 3, 4)), "Fisrt", 5.0);
            artcls[1] = new Article(new Person("Иван", "Сидоров", new DateTime(1986, 7, 6)), "Second", 4.6);
            artcls[2] = new Article(new Person("Мария", "Иванова", new DateTime(1990, 11, 26)), "Third", 4.8);
            magazine.Articles = artcls;
            Console.WriteLine(magazine.ToShortString());
            Console.WriteLine();

            // #2
            Console.WriteLine(magazine[Frequency.Weekly] + " " + magazine[Frequency.Monthly] + " " + magazine[Frequency.Yearly]);
            Console.WriteLine();

            // #3
            Console.WriteLine(magazine.ToString());
            Console.WriteLine();

            // #4
            Article[] new_articles = new Article[2];
            new_articles[0] = new Article(new Person("Антон", "Михайлов", new DateTime(2001, 5, 7)), "Forth", 5.0);
            new_articles[1] = new Article(new Person("Андрей", "Богданов", new DateTime(2002, 4, 12)), "Fifth", 4.7);
            magazine.AddArticles(new_articles);
            Console.WriteLine(magazine.ToString());

            // #5
            string chars = "/ |";
            Console.WriteLine("Начинаем работу с массивами, введите два целых числа через разделитель.");
            Console.WriteLine($"Разделителем является любой символ из набора: '{chars}'.");
            Console.Write("Введите числа:\t");
            string nums = Console.ReadLine();
            char symbol = ' ';
            foreach(var item in chars)
            {
                foreach(var elem in nums)
                {
                    if (elem == item)
                    {
                        symbol = (char)item;
                        break;
                    }
                }
            }
            int nrow = int.Parse(nums.Split(symbol)[0]);
            int ncolumn = int.Parse(nums.Split(symbol)[1]);

            Article[] arr1D = new Article[nrow*ncolumn];
            Article[,] arr2D = new Article[nrow, ncolumn];
            Article[][] arrst = new Article[nrow][];

            for (int i = 0; i < nrow * ncolumn; i++)
                arr1D[i] = new Article();

            for (int i = 0; i < nrow; i++)
                for (int j = 0; j < ncolumn; j++)
                    arr2D[i,j] = new Article();

            int cnt = 0;
            for (int i = 0; i < nrow; i++)
            {
                if (i == nrow - 1)
                {
                    arrst[i] = new Article[nrow*ncolumn - cnt];
                    for (int j = 0; j < nrow * ncolumn - cnt; j++)
                        arrst[i][j] = new Article();
                    continue;
                }
                arrst[i] = new Article[ncolumn - i];
                cnt += ncolumn - i;
                for (int j = 0; j < ncolumn - i; j++)
                    arrst[i][j] = new Article();
            }

            var timer = new Stopwatch();
            int y = 0; int x = 0;
            timer.Start();
            for (y = 0; y < nrow*ncolumn; y++)
                arr1D[y].Title = "Smth";
            timer.Stop();

            Console.WriteLine($"Время для одномерного массива:\t{timer.Elapsed}");

            timer.Restart();
            for (y = 0; y < nrow; y++)
                for (x = 0; x < ncolumn; x++)
                    arr2D[y, x].Title = "Smth";
            timer.Stop();

            Console.WriteLine($"Время для двумерного массива:\t{timer.Elapsed}");

            timer.Restart();
            for (y = 0; y < nrow; y++)
                for (x = 0; x < arrst[y].Length; x++)
                    arrst[y][x].Title = "Smth";
            timer.Stop();

            Console.WriteLine($"Время для ступенчатого массива:\t{timer.Elapsed}");
        }
    }
}