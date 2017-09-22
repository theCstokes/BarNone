using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class SQLResults : IEnumerable<KeyValuePair<string, object>>
    {
        private Dictionary<string, object> results;

        public SQLResults()
        {
            results = new Dictionary<string, object>();
        }

        public SQLResults(Dictionary<string, object> results)
        {
            this.results = results;
        }

        public object this[string name]
        {
            get
            {
                return results[name];
            }
            set
            {
                results[name] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
