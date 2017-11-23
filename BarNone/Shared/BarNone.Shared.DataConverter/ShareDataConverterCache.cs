using BarNone.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataConverter
{
    public struct Convertion {
        public ITrackable DataModel;
        public ITrackable DTO;
    }

    public class ShareDataConverterCache
    {
        public Dictionary<Type, List<Convertion>> _coverterCache { get; private set; }

        //public Dictionary<Type, List<Convertion>> DataModelCoverterCache { get; private set; }

        public ShareDataConverterCache()
        {
            _coverterCache = new Dictionary<Type, List<Convertion>>();
            //DataModelCoverterCache = new Dictionary<Type, List<Convertion>>();
        }

        public ITrackable GetDTOConvertion(Type converterType, ITrackable dto)
        {
            if (!_coverterCache.ContainsKey(converterType)) return null;
            return _coverterCache[converterType]
                .Where(c => c.DTO.ID == dto.ID)
                .Select(c => c.DataModel)
                .FirstOrDefault();
        }

        public ITrackable GetDataModelConvertion(Type converterType, ITrackable dataModel)
        {
            if (!_coverterCache.ContainsKey(converterType)) return null;
            return _coverterCache[converterType]
                .Where(c => c.DataModel.ID == dataModel.ID)
                .Select(c => c.DTO)
                .FirstOrDefault();
        }

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
