using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{

    class MagazinesChangedEventArgs<TKey> : EventArgs
    {
        public string NameCollection { get; set; }
        public Update Action { get; set; }
        public string PropertyName { get; set; }
        public TKey Key { get; set; }

        public MagazinesChangedEventArgs(string nameCollection, Update action, string propertyName, TKey key)
        {
            NameCollection = nameCollection;
            Action = action;
            PropertyName = propertyName;
            Key = key;
        }
        public override string ToString()
        {
            return NameCollection + " " + Action.ToString() + " " + PropertyName + " " + Key.ToString();
        }
    }
}
