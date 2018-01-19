using BarNone.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataConverter.Core
{
    /// <summary>
    /// Conversion tracking object.
    /// </summary>
    public struct Convertion {
        public ITrackable DataModel;
        public ITrackable DTO;
    }

    /// <summary>
    /// Share data converter cache.
    /// </summary>
    public class ShareDataConverterCache
    {
        /// <summary>
        /// Gets the coverter cache.
        /// </summary>
        /// <value>
        /// The coverter cache.
        /// </value>
        public Dictionary<Type, List<Convertion>> _coverterCache { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShareDataConverterCache"/> class.
        /// </summary>
        public ShareDataConverterCache()
        {
            _coverterCache = new Dictionary<Type, List<Convertion>>();
        }

        /// <summary>
        /// Gets the dto convertion mapped to type.
        /// </summary>
        /// <param name="converterType">Type of the converter.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public ITrackable GetDTOConvertion(Type converterType, ITrackable dto)
        {
            if (!_coverterCache.ContainsKey(converterType)) return null;
            return _coverterCache[converterType]
                .Where(c => c.DTO == dto)
                .Select(c => c.DataModel)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the data model convertion mapped to type.
        /// </summary>
        /// <param name="converterType">Type of the converter.</param>
        /// <param name="dataModel">The data model.</param>
        /// <returns></returns>
        public ITrackable GetDataModelConvertion(Type converterType, ITrackable dataModel)
        {
            if (!_coverterCache.ContainsKey(converterType)) return null;
            return _coverterCache[converterType]
                .Where(c => c.DataModel == dataModel)
                .Select(c => c.DTO)
                .FirstOrDefault();
        }

        /// <summary>
        /// Adds the convertion map.
        /// </summary>
        /// <param name="converterType">Type of the converter.</param>
        /// <param name="dataModel">The data model.</param>
        /// <param name="dto">The dto.</param>
        public void AddConvertion(Type converterType, ITrackable dataModel, ITrackable dto)
        {
            if (!_coverterCache.ContainsKey(converterType)) _coverterCache[converterType] = new List<Convertion>();
            _coverterCache[converterType].Add(new Convertion
            {
                DataModel = dataModel,
                DTO = dto
            });
        }
    }
}
