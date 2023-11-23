using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Listener
    {
        private List<ListEntry> Entries = new List<ListEntry>();

        public void MagazinesChanged(object obj, EventArgs args)
        {
            var AllChnges = args as MagazinesChangedEventArgs<string>;
            Entries.Add(new ListEntry(AllChnges.NameCollection, AllChnges.Action, AllChnges.PropertyName, AllChnges.Key));
        }
        public override string ToString()
        {
            string result = "";
            foreach (var elem in Entries)
            {
                result += "\nИзменена коллекция: " + elem.NameCollection + " \nТип изменения: " + elem.Action + "\nИзменения: " + elem.PropertyName + "\nКлюч: " + elem.Key;
            }
            return result;
        }
    }
}
