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
    public class LiftFolderDataAdapter : BaseDataAdapter<LiftFolderDTO, LiftFolder>
    {
        public override Dictionary<BasePropertyEnum, DTORead<LiftFolderDTO>> DTOReadMap { get; }
            = new Dictionary<BasePropertyEnum, DTORead<LiftFolderDTO>>
            {
                [BasePropertyEnum.ID] = (dto) => dto.ID,
                [LiftFolderPropertyEnum.Name] = (dto) => dto.Name,
                [LiftFolderPropertyEnum.SubFolders] = (dto) => dto.Details?.SubFolders,
                //[LiftFolderPropertyEnum.Lifts] = (dto) => dto.Details?.Lifts
            };

        public override Dictionary<BasePropertyEnum, DTOWrite<LiftFolderDTO>> DTOWriteMap { get; }
             = new Dictionary<BasePropertyEnum, DTOWrite<LiftFolderDTO>>
             {
                 [BasePropertyEnum.ID] = (dto, value) => dto.ID = value,
                 [LiftFolderPropertyEnum.Name] = (dto, value) => dto.Name = value,
                 [LiftFolderPropertyEnum.SubFolders] = (dto, value) =>
                 {
                     if (dto.Details == null) dto.Details = new LiftFolderDetailDTO();
                     dto.Details.SubFolders = value;
                 },
                 //[LiftFolderPropertyEnum.Lifts] = (dto, value) =>
                 //{
                 //    if (dto.Details == null) dto.Details = new LiftFolderDetailDTO();
                 //    dto.Details.Lifts = value;
                 //}
             };

        public override Dictionary<BasePropertyEnum, DomainModelRead<LiftFolder>> DomainModelReadMap { get; }
            = new Dictionary<BasePropertyEnum, DomainModelRead<LiftFolder>>
            {
                [BasePropertyEnum.ID] = (entity) => entity.ID,
                [LiftFolderPropertyEnum.Name] = (entity) => entity.Name,
                [LiftFolderPropertyEnum.SubFolders] = (entity) => entity.SubFolders,
                //[LiftFolderPropertyEnum.Lifts] = (entity) => entity.Lifts
            };

        public override Dictionary<BasePropertyEnum, DomainModelWrite<LiftFolder>> DomainModelWriteMap { get; }
            = new Dictionary<BasePropertyEnum, DomainModelWrite<LiftFolder>>
            {
                [BasePropertyEnum.ID] = (entity, value) => entity.ID = value,
                [LiftFolderPropertyEnum.Name] = (entity, value) => entity.Name = value,
                [LiftFolderPropertyEnum.SubFolders] = (entity, value) => entity.SubFolders = value,
                //[LiftFolderPropertyEnum.Lifts] = (entity, value) => entity.Lifts = value
            };
    }
}
