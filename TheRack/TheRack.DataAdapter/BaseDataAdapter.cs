using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataTransfer;
using TheRack.DomainModel;
using static TheRack.DataAdapter.DataAdapterActions;

namespace TheRack.DataAdapter
{
    public abstract class BaseDataAdapter<TDTO, TDomainModel>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        public abstract Dictionary<string, DTORead<TDTO>> DTOReadMap { get; }

        public abstract Dictionary<string, DTOWrite<TDTO>> DTOWriteMap { get; }

        public abstract Dictionary<string, DomainModelRead<TDomainModel>> DomainModelReadMap { get; }

        public abstract Dictionary<string, DomainModelWrite<TDomainModel>> DomainModelWriteMap { get; }

        public TDomainModel GetDomainModel(TDTO dto)
        {
            var entity = new TDomainModel();

            foreach(var pair in DTOReadMap)
            {
                if (DomainModelWriteMap.ContainsKey(pair.Key))
                {
                    var value = pair.Value(dto);
                    if (value == null) continue;
                    if (dto.UpdateFilter != null && dto.UpdateFilter.Contains(pair.Key)) continue;
                    var f = DomainModelWriteMap[pair.Key];
                    f.Invoke(entity, value);
                }
            }

            return entity;
        }

        public TDTO GetDTO(TDomainModel entity)
        {
            var dto = new TDTO();

            foreach (var pair in DomainModelReadMap)
            {
                if (DomainModelWriteMap.ContainsKey(pair.Key))
                {
                    var value = pair.Value(entity);
                    if (value == null) continue;

                    DTOWriteMap[pair.Key](dto, value);
                }
            }

            return dto;
        }
    }
}
