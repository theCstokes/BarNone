using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataTransfer;
using TheRack.DataTransfer.Core;
using TheRack.DomainModel;
using static TheRack.DataAdapter.Core.DataAdapterActions;

namespace TheRack.DataAdapter.Core
{
    public abstract class BaseDataAdapter<TDTO, TDomainModel>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        public abstract Dictionary<BasePropertyEnum, DTORead<TDTO>> DTOReadMap { get; }

        public abstract Dictionary<BasePropertyEnum, DTOWrite<TDTO>> DTOWriteMap { get; }

        public abstract Dictionary<BasePropertyEnum, DomainModelRead<TDomainModel>> DomainModelReadMap { get; }

        public abstract Dictionary<BasePropertyEnum, DomainModelWrite<TDomainModel>> DomainModelWriteMap { get; }

        public TDomainModel GetDomainModel(TDTO dto)
        {
            var entity = new TDomainModel();

            foreach(var pair in DTOReadMap)
            {
                if (DomainModelWriteMap.ContainsKey(pair.Key))
                {
                    var propertyMapperType = pair.Key;
                    var dtoValue = pair.Value(dto);
                    if (dtoValue == null) continue;

                    if (dto.UpdateFilter != null && dto.UpdateFilter.Contains(propertyMapperType.Name)) continue;
                    var domainModelValue = propertyMapperType.Mapper.ToDomainModel(dtoValue);

                    DomainModelWriteMap[pair.Key](entity, domainModelValue);
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
                    var propertyMapperType = pair.Key;
                    var domainModelValue = pair.Value(entity);
                    if (domainModelValue == null) continue;

                    var dtoValue = propertyMapperType.Mapper.ToDTO(domainModelValue);

                    DTOWriteMap[pair.Key](dto, dtoValue);
                }
            }

            return dto;
        }
    }
}
