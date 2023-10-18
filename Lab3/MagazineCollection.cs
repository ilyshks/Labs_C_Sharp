using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    delegate TKey KeySelector<TKey>(Magazine mg);
    internal class MagazineCollection<Tkey>
    {
        private Dictionary<Tkey, Magazine> collection;
        private KeySelector<Tkey> key;
        public MagazineCollection(KeySelector<Tkey> key) 
        {
            if (key != null) this.key = key;
        }
    }
}
