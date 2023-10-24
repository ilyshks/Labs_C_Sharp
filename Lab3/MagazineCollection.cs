using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
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
    }
}