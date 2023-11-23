using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> magazines;
        private KeySelector<TKey> keySelector;
        public MagazineCollection(KeySelector<TKey> keySelector)
        {
            this.keySelector = keySelector;
            magazines = new Dictionary<TKey, Magazine>();
        }
        public void AddDefaults()
        {
            Magazine m = new Magazine();
            magazines.Add(keySelector(m), m);
        }
        public void AddMagazines(params Magazine[] value)
        {
            foreach (Magazine m in value)
            {
                magazines.Add(keySelector(m), m);
                MagazinePropertyChanged(Update.Add, "Метод AddMagazines", keySelector(m));// вызов события
                magazines[keySelector(m)].PropertyChanged += HandlerEvent;
            }
        }
        public override string ToString()
        {
            string line = "";
            foreach (KeyValuePair<TKey, Magazine> KV in magazines)
            {
                if (KV.Key != null) line += KV.Key.ToString() + "  " + KV.Value.ToString() + "  " + "\n";
            }
            return line;
        }
        public string ToShortString()
        {
            string line = "";
            foreach (KeyValuePair<TKey, Magazine> KV in magazines)
            {
                if (KV.Key != null) line += KV.Key.ToString() + "  " + KV.Value.ToShortString() + "  " + "\n";
            }
            return line;
        }
        public double MaxAverageValue
        {
            get
            {
                if (magazines == null || magazines.Count == 0) return 0;
                List<double> doubles = new List<double>();
                foreach (KeyValuePair<TKey, Magazine> KV in magazines)
                {
                    doubles.Add(KV.Value.AverageValue);
                }

                return doubles.Max();

            }
        }
        public string NameCollection { get; set; }

        public event MagazinesChangedHandler<TKey> MagazinesChanged;
        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            IEnumerable<KeyValuePair<TKey, Magazine>> small_set = magazines.Where(keys => keys.Value.Frequency == value);
            return small_set;
        }
        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> ToGroup
        {
            get
            {
                return magazines.GroupBy(ex => ex.Value.Frequency);
            }
        }

        public bool Replace(Magazine mold, Magazine mnew)
        {
            bool Flag = false;
            foreach (KeyValuePair<TKey, Magazine> KV in magazines)
            {
                if (KV.Value == mold)
                {
                    MagazinePropertyChanged(Update.Replace, "Метод Replace", keySelector(mold));// вызов события
                    magazines[KV.Key] = mnew;
                    Flag = true;
                }
            }
            return Flag;
        }

        //Методы для обработки событий
        private void MagazinePropertyChanged(Update action, string name, TKey key)
        {
            MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(NameCollection, action, name, key));
        }
        private void HandlerEvent(object sender, EventArgs e)
        {
            var it = (PropertyChangedEventArgs)e;
            var mg = (Magazine)sender;
            var key = keySelector(mg);
            MagazinePropertyChanged(Update.Property, it.PropertyName, key);
        }
        private void PropertyChangeded(object obj, EventArgs args)
        {
            MagazinePropertyChanged(Update.Property, (args as MagazinesChangedEventArgs<string>).PropertyName, keySelector((Magazine)obj));
        }
    }
}