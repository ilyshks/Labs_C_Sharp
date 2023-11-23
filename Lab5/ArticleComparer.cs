//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lab5
//{
//    class ArticleComparer : IComparer<Article>
//    {
//        public int Compare(Article? x, Article? y)
//        {
//            if (x == null || y == null) return 1;
//            return x.Estimate.CompareTo(y.Estimate);
//        }
//        public int CompareTitle(Article? x, Article? y)
//        {
//            if (x is null || y is null) return 1;
//            return x.Title.CompareTo(y.Title);
//        }
//        public int CompareAuthorSurname(Article? x, Article? y)
//        {
//            if (x is null || y is null) return 1;
//            return x.Author.Surname.CompareTo(y.Author.Surname);
//        }
//    }
//}
