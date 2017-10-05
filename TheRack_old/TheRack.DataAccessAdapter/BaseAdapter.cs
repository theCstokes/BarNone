using TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheRack.DataAccessAdapter.DataAccessActions;

namespace TheRack.DataAccessAdapter
{
    public abstract class BaseAdapter<TDTO, TDomainModel> 
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        public abstract MapToDTO<TDomainModel, TDTO> ToDTO { get; }
        public abstract MapToDomainModel<TDTO, TDomainModel> ToDomainModel { get; }
        public abstract Dictionary<string, ReadAction<TDTO>> ReadMap { get; }
        public abstract Dictionary<string, WriteAction<TDomainModel>> WriteMap { get; }

        public WriteAction<TDomainModel> IDWriteAction = (model, value) => model.ID = value;

        public abstract string SchemaName { get; }
        public abstract string TableName { get; }

        public Dictionary<string, WriteAction<TDomainModel>> FullWriteMap
        {
            get
            {
                var map = new Dictionary<string, WriteAction<TDomainModel>>(WriteMap);
                map.Add("ID", IDWriteAction);
                return map;
            }
        }

        public List<string> Properties
        {
            get
            {
                var properties = new List<string>(WriteMap.Keys);
                properties.Add("ID");
                return properties;

            }
        }

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
            dataMap["ID"] = null;
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

        //public List<TResult> BuildResults(List<SqlResult>)

        //public void Populate(TDomainModel model, IEnumerable<KeyValuePair<string, object>> data)
        //{
        //    foreach (var pair in data)
        //    {
        //        if (!WriteMap.ContainsKey(pair.Key)) continue;
        //        WriteMap[pair.Key](model, pair.Value);
        //    }
        //}
    }
}
