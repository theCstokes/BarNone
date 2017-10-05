using TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccessAdapter
{
    public static class AdapterMap
    {
        public static Dictionary<Type, dynamic> _map = new Dictionary<Type, dynamic>
        {
            [typeof(User)] = new UserAdapter()
        };

        public static Dictionary<Type, dynamic> Map
        {
            get
            {
                return _map;
            }
        }
    }
}
