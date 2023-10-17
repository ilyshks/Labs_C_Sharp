using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class ArticleEstimateComparer : IComparer<Article>
    {
        public int Compare(Article? x, Article? y)
        {
            if (x == null || y == null) return 1;
            return x.Estimate.CompareTo(y.Estimate);
        }
    }
}
