using BarNone.Shared.DataTransfer.Core.Filter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataTransfer.Core
{
    public class FilterDTO
    {
        #region Public Delegate Definition(s).
        public delegate bool WhereFunc(object obj); 
        #endregion

        #region Private Static Read-only Field(s).
        private static readonly JsonSerializer serializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }; 
        #endregion

        #region Public Property(s).
        public List<WhereFilter> Where { get; set; } 
        #endregion

        #region Public Member(s).
        public WhereFunc GetWhere()
        {
            var d = Delegate.Combine(Where.Select(filter =>
            {
                return CreateWhere(filter);
            }).ToArray());

            return (obj) => Convert.ToBoolean(d.DynamicInvoke(obj));
        } 
        #endregion

        #region Private Member(s).
        private WhereFunc CreateWhere(WhereFilter filter)
        {
            return (obj) =>
            {
                var j = JObject.FromObject(obj, serializer);
                var p = j[filter.Property];
                if (filter.Value == null)
                {
                    return (p.ToObject<object>() == null);
                }
                if (p == null) return false;
                return (p.ToObject(filter.Value.GetType()).Equals(filter.Value));
            };
        } 
        #endregion
    }
}
