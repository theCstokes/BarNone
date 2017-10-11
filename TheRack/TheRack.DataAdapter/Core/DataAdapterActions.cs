using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataTransfer;
using TheRack.DataTransfer.Core;
using TheRack.DomainModel;

namespace TheRack.DataAdapter.Core
{
    public static class DataAdapterActions
    {
        public delegate dynamic DTORead<TDTO>(TDTO dto)
            where TDTO : BaseDTO<TDTO>, new();

        public delegate void DTOWrite<TDTO>(TDTO dto, dynamic value)
            where TDTO : BaseDTO<TDTO>, new();

        public delegate dynamic DomainModelRead<TDomainModel>(TDomainModel entity)
            where TDomainModel : BaseDomainModel<TDomainModel>, new();

        public delegate void DomainModelWrite<TDomainModel>(TDomainModel entity, dynamic value)
            where TDomainModel : BaseDomainModel<TDomainModel>, new();
    }
}
