using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataModel
{
    public class MetaDataManager
    {
        public static MetaData GetMetaData<TModel>()
        {
            return new MetaData();
        }
    }
}
