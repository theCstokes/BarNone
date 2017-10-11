using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataAdapter.Core;
using TheRack.DataTransfer;
using TheRack.DataTransfer.Lift;
using TheRack.DomainModel;
using static TheRack.DataAdapter.Core.DataAdapterActions;

namespace TheRack.DataAdapter
{
    public class LiftDataAdapter : BaseDataAdapter<LiftDTO, Lift>
    {
        public override Dictionary<BasePropertyEnum, DTORead<LiftDTO>> DTOReadMap { get; }
            = new Dictionary<BasePropertyEnum, DTORead<LiftDTO>>
            {
                [BasePropertyEnum.ID] = (dto) => dto.ID,
                [LiftPropertyEnum.Name] = (dto) => dto.Name,
                [LiftPropertyEnum.ParentID] = (dto) => dto.ParentID,
                [LiftPropertyEnum.Parent] = (dto) => dto.Details?.Parent
            };

        public override Dictionary<BasePropertyEnum, DTOWrite<LiftDTO>> DTOWriteMap { get; }
             = new Dictionary<BasePropertyEnum, DTOWrite<LiftDTO>>
             {
                 [BasePropertyEnum.ID] = (dto, value) => dto.ID = value,
                 [LiftPropertyEnum.Name] = (dto, value) => dto.Name = value,
                 [LiftPropertyEnum.ParentID] = (dto, value) => dto.ParentID = value,
                 [LiftPropertyEnum.Parent] = (dto, value) =>
                 {
                     if (dto.Details == null) dto.Details = new LiftDetailDTO();
                     dto.Details.Parent = value;
                 }
             };

        public override Dictionary<BasePropertyEnum, DomainModelRead<Lift>> DomainModelReadMap { get; }
            = new Dictionary<BasePropertyEnum, DomainModelRead<Lift>>
            {
                [BasePropertyEnum.ID] = (entity) => entity.ID,
                [LiftPropertyEnum.Name] = (entity) => entity.Name,
                [LiftPropertyEnum.ParentID] = (entity) => entity.ParentID,
                [LiftPropertyEnum.Parent] = (entity) => entity.Parent
            };

        public override Dictionary<BasePropertyEnum, DomainModelWrite<Lift>> DomainModelWriteMap { get; }
            = new Dictionary<BasePropertyEnum, DomainModelWrite<Lift>>
            {
                [BasePropertyEnum.ID] = (entity, value) => entity.ID = value,
                [LiftPropertyEnum.Name] = (entity, value) => entity.Name = value,
                [LiftPropertyEnum.ParentID] = (entity, value) => entity.ParentID = value,
                [LiftPropertyEnum.Parent] = (entity, value) => entity.Parent = value
            };
    }
}
