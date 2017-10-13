using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.DomainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DataAdapter.Core
{
    public class DetailCollectionPropertyMapper<TDTO, TDomainModel> : IPropertyMapper
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        private BaseDataAdapter<TDTO, TDomainModel> _adapter;

        public DetailCollectionPropertyMapper(BaseDataAdapter<TDTO, TDomainModel> adapter)
        {
            this._adapter = adapter;
        }

        public dynamic ToDTO(dynamic value)
        {
            var valueList = value as List<TDomainModel>;
            _adapter.GetDTO(valueList[0]);
            return valueList.ToList().Select((item) =>
            {
                return _adapter.GetDTO(item);
            });
        }

        public dynamic ToDomainModel(dynamic value)
        {
            var valueList = value as List<TDTO>;
            return valueList.Select((item) => _adapter.GetDomainModel(item));
        }
    }
}
