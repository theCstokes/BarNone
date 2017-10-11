using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataTransfer.Core;
using TheRack.DomainModel;

namespace TheRack.DataAdapter.Core
{
    public class DetailPropertyMapper<TDTO, TDomainModel> : IPropertyMapper
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        private BaseDataAdapter<TDTO, TDomainModel> _adapter;

        public DetailPropertyMapper(BaseDataAdapter<TDTO, TDomainModel> adapter)
        {
            this._adapter = adapter;
        }

        public dynamic ToDTO(dynamic value)
        {
            return _adapter.GetDTO(value);
        }

        public dynamic ToDomainModel(dynamic value)
        {
            return _adapter.GetDomainModel(value);
        }
    }
}
