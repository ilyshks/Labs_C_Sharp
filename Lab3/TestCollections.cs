using System.Diagnostics;

namespace Lab3
{
    internal class TestCollections<TKey, TValue>
    {
        private List<TKey> List_Tkey;
        private List<string> List_String;
        private Dictionary<TKey, TValue> Dictionary_TT;
        private Dictionary<string, TValue> Dictionary_ST;
        private GenerateElement<TKey, TValue> Generate_Element;

        public TestCollections(int size, GenerateElement<TKey, TValue> j)
        {
            List_Tkey = new List<TKey>();
            List_String = new List<string>();
            Dictionary_TT = new Dictionary<TKey, TValue>();
            Dictionary_ST = new Dictionary<string, TValue>();
            Generate_Element = j;
            for (int i = 0; i < size; i++)
            {
                var elem = Generate_Element(i);
                Dictionary_TT.Add(elem.Key, elem.Value);
                Dictionary_ST.Add(elem.Key.ToString(), elem.Value);
                List_Tkey.Add(elem.Key);
                List_String.Add(elem.Key.ToString());
            }
        }
        public static KeyValuePair<Edition, Magazine> GenerateElement(int j)
        {
            Edition key = new Edition("Title " + j, new DateTime(2000 + j % 30, 1 + j % 12, 1 + j % 30), Math.Abs(j));
            Person editor = new Person("Name " + j, "Surname " + j, new DateTime(2000 + j % 30, 1 + j % 12, 1 + j % 30));
            Article article = new Article(editor, "Article " + j, j % 5 + 1);
            List<Person> L_P = new List<Person>{ editor }; 
            List<Article> L_A = new List<Article> { article };
            Magazine value = new Magazine("Magazine " + j, (Frequency)(j % 3), new DateTime(2000 + j % 30, 1 + j % 12, 1 + j % 30), Math.Abs(j));
            value.Editors = L_P;
            value.Articles= L_A;
            return new KeyValuePair<Edition, Magazine>(key, value);
        }


        public void searchListOfTKeys()
        {
            TKey firstElement = List_Tkey.First();
            TKey middleElement = List_Tkey.ToArray()[List_Tkey.Count / 2];
            TKey lastElement = List_Tkey.ToArray()[List_Tkey.Count - 1];
            TKey noneElement = Generate_Element(List_Tkey.Count + 1).Key;

            Console.WriteLine($"\nВремя list of TKeys");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            List_Tkey.Contains(firstElement);
            sw.Stop();
            Console.WriteLine($"Время первого элемента: {sw.Elapsed}");
            sw.Restart();


            sw.Start();
            List_Tkey.Contains(middleElement);
            sw.Stop();
            Console.WriteLine($"Время центрального элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            List_Tkey.Contains(lastElement);
            sw.Stop();
            Console.WriteLine($"Время последнего элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            List_Tkey.Contains(noneElement);
            Console.WriteLine($"Время элемента не входящего в коллекцию: {sw.Elapsed}");
            sw.Stop();
        }
        public void searchListOfString()
        {
            string firstElement = List_String.First();
            string middleElement = List_String.ToArray()[List_String.Count / 2];
            string lastElement = List_String.ToArray()[List_String.Count - 1];
            string noneElement = Generate_Element(List_Tkey.Count + 1).Key.ToString();

            Console.WriteLine($"\nВремя list of string");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            List_String.Contains(firstElement);
            sw.Stop();
            Console.WriteLine($"Время первого элемента: {sw.Elapsed}");
            sw.Restart();


            sw.Start();
            List_String.Contains(middleElement);
            sw.Stop();
            Console.WriteLine($"Время центрального элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            List_String.Contains(lastElement);
            sw.Stop();
            Console.WriteLine($"Время последнего элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            List_String.Contains(noneElement);
            Console.WriteLine($"Время элемента не входящего в коллекцию: {sw.Elapsed}");
            sw.Stop();
        }
        public void searchKeyInDictOfTKeys()
        {
            TKey firstElement = Dictionary_TT.ElementAt(0).Key;
            TKey middleElement = Dictionary_TT.ElementAt(Dictionary_TT.Count / 2).Key;
            TKey lastElement = Dictionary_TT.ElementAt(Dictionary_TT.Count - 1).Key;
            TKey noneElement = Generate_Element(List_Tkey.Count + 1).Key;

            Console.WriteLine($"\nВоемя поиска ключей dictionary of TKeys");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Dictionary_TT.ContainsKey(firstElement);
            sw.Stop();
            Console.WriteLine($"Время первого элемента: {sw.Elapsed}");
            sw.Restart();


            sw.Start();
            Dictionary_TT.ContainsKey(middleElement);
            sw.Stop();
            Console.WriteLine($"Время центрального элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_TT.ContainsKey(lastElement);
            sw.Stop();
            Console.WriteLine($"Время последнего элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_TT.ContainsKey(noneElement);
            Console.WriteLine($"Время элемента не входящего в коллекцию: {sw.Elapsed}");
            sw.Stop();
        }
        public void searchKeyInDictOfString()
        {
            string firstElement = Dictionary_ST.ElementAt(0).Key;
            string middleElement = Dictionary_ST.ElementAt(Dictionary_ST.Count / 2).Key;
            string lastElement = Dictionary_ST.ElementAt(Dictionary_ST.Count - 1).Key;
            string noneElement = Generate_Element(List_Tkey.Count + 1).Key.ToString();

            Console.WriteLine($"\nВремя поиска ключей в dictionary of String");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Dictionary_ST.ContainsKey(firstElement);
            sw.Stop();
            Console.WriteLine($"Время первого элемента: {sw.Elapsed}");
            sw.Restart();


            sw.Start();
            Dictionary_ST.ContainsKey(middleElement);
            sw.Stop();
            Console.WriteLine($"Время центрального элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_ST.ContainsKey(lastElement);
            sw.Stop();
            Console.WriteLine($"Время последнего элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_ST.ContainsKey(noneElement);
            Console.WriteLine($"Время элемента не входящего в коллекцию: {sw.Elapsed}");
            sw.Stop();
        }
        public void searchValueInDictOfTKeys()
        {
            TValue firstElement = Dictionary_TT.ElementAt(0).Value;
            TValue middleElement = Dictionary_TT.ElementAt(Dictionary_TT.Count / 2).Value;
            TValue lastElement = Dictionary_TT.ElementAt(Dictionary_TT.Count - 1).Value;
            TValue noneElement = Generate_Element(List_Tkey.Count + 1).Value;

            Console.WriteLine($"\nВремя поиска значений в dictionary of TKey");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Dictionary_TT.ContainsValue(firstElement);
            sw.Stop();
            Console.WriteLine($"Время первого элемента: {sw.Elapsed}");
            sw.Restart();


            sw.Start();
            Dictionary_TT.ContainsValue(middleElement);
            sw.Stop();
            Console.WriteLine($"Время центрального элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_TT.ContainsValue(lastElement);
            sw.Stop();
            Console.WriteLine($"Время последнего элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_TT.ContainsValue(noneElement);
            Console.WriteLine($"Время элемента не входящего в коллекцию: {sw.Elapsed}");
            sw.Stop();
        }
        public void searchValueInDictOfString()
        {
            TValue firstElement = Dictionary_ST.ElementAt(0).Value;
            TValue middleElement = Dictionary_ST.ElementAt(Dictionary_ST.Count / 2).Value;
            TValue lastElement = Dictionary_ST.ElementAt(Dictionary_ST.Count - 1).Value;
            TValue noneElement = Generate_Element(List_Tkey.Count + 1).Value;

            Console.WriteLine($"\nВремя поиска значений в dictionary of String");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Dictionary_ST.ContainsValue(firstElement);
            sw.Stop();
            Console.WriteLine($"Время первого элемента: {sw.Elapsed}");
            sw.Restart();


            sw.Start();
            Dictionary_ST.ContainsValue(middleElement);
            sw.Stop();
            Console.WriteLine($"Время центрального элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_ST.ContainsValue(lastElement);
            sw.Stop();
            Console.WriteLine($"Время последнего элемента: {sw.Elapsed}");
            sw.Restart();

            sw.Start();
            Dictionary_ST.ContainsValue(noneElement);
            Console.WriteLine($"Время элемента не входящего в коллекцию: {sw.Elapsed}");
            sw.Stop();
        }
    }
}
