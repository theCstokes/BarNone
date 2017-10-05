using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class DataEntity : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> _data;

        public DataEntity(Dictionary<string, string> data)
        {
            _data = data;
        }
        public DataEntity(int id, Dictionary<string, string> data)
        {
            ID = id;
            _data = data;
        }

        #region Public Accessor(s).
        public string this[string key]
        {
            get
            {
                return _data[key];
            }
            set
            {
                _data[key] = value;
            }
        }
        #endregion

        #region Public Property(s).
        public int ID { get; set; }
        #endregion

        #region Public Member(s).
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
        #endregion
    }
}
