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
    /// <summary>
    /// Filter dto.
    /// </summary>
    public class FilterDTO
    {
        #region Public Delegate Definition(s).        
        /// <summary>
        /// Entity filter delegate.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public delegate bool WhereFunc(object obj);
        #endregion

        #region Private Static Read-only Field(s).        
        /// <summary>
        /// The serializer for dtos to json.
        /// </summary>
        private static readonly JsonSerializer serializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        #endregion

        #region Public Property(s).        
        /// <summary>
        /// Gets or sets the where.
        /// </summary>
        /// <value>
        /// The where.
        /// </value>
        public List<WhereFilter> Where { get; set; }
        #endregion

        #region Public Member(s).        
        /// <summary>
        /// Gets the where.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Creates the where.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
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
                if (p.ToObject<object>() == null) return false;
                return (p.ToObject(filter.Value.GetType()).Equals(filter.Value));
            };
        } 
        #endregion
    }
}
