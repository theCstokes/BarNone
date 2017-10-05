using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.Core
{
    public class RequestParameters : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> _parameters;

        public RequestParameters()
        {
            _parameters = new Dictionary<string, string>();
        }

        public string this[string key]
        {
            get
            {
                return _parameters[key];
            }
            set
            {
                _parameters[key] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }
    }
}
