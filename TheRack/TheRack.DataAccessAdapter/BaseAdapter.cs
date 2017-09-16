using TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccessAdapter
{
    public abstract class BaseAdapter<TDTO, TDomainModel> where TDomainModel : BaseDomainModel
    {
        public delegate dynamic ReadAction(TDTO dto);
        public delegate void WriteAction(TDomainModel model, dynamic value);
        public delegate TDTO MapToDTO(TDomainModel model);
        public delegate TDomainModel MapToDomainModel(TDTO dto);

        public abstract MapToDTO ToDTO { get; }
        public abstract MapToDomainModel ToDomainModel { get; }
        public abstract Dictionary<string, ReadAction> ReadMap { get; }
        public abstract Dictionary<string, WriteAction> WriteMap { get; }

        public Dictionary<string, string> CreateReadMap(TDTO dto)
        {
            var dataMap = new Dictionary<string, string>();
            foreach(var pair in ReadMap)
            {
                dataMap[pair.Key] = Convert.ToString(pair.Value(dto));
            }
            return dataMap;
        }

        public Dictionary<string, string> CreateMapForGet()
        {
            var dataMap = new Dictionary<string, string>();
            //dataMap["ID"] = null;
            foreach (var pair in ReadMap)
            {
                dataMap[pair.Key] = null;
            }
            return dataMap;
        }

        //public Dictionary<string, string> CreateWriteMap(TDTO dto)
        //{
        //    var dataMap = new Dictionary<string, string>();
        //    foreach (var pair in ReadMap)
        //    {
        //        dataMap[pair.Key] = pair.Value(dto);
        //    }
        //    return dataMap;
        //}

        public void Populate(TDomainModel model, IEnumerable<KeyValuePair<string, object>> data)
        {
            foreach (var pair in data)
            {
                if (!WriteMap.ContainsKey(pair.Key)) continue;
                WriteMap[pair.Key](model, pair.Value);
            }
        }
    }
}
