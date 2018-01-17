using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    public class LiftFolderConverter 
        : BaseDetailDataConverter<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO, Converters>
    {
        public LiftFolderConverter(Converters converterContext) : base(converterContext)
        {
        }

        public override LiftFolder OnCreateDataModel(LiftFolderDTO dto)
        {
            return new LiftFolder
            {
                ID = dto.ID,
                Name = dto.Name,
                ParentID = dto.ParentID
            };
        }

        public override void OnCreateDetailDataModel(LiftFolder data, LiftFolderDetailDTO dto)
        {
            data.Parent = converterContext.LiftFolder.CreateDataModel(dto.Parent);
            data.Lifts = dto.Lifts.Select(l => converterContext.Lift.CreateDataModel(l)).ToList();
            data.SubFolders = dto.SubFolders.Select(f => converterContext.LiftFolder.CreateDataModel(f)).ToList();
        }

        public override LiftFolderDetailDTO OnCreateDetailDTO(LiftFolder data)
        {
            return new LiftFolderDetailDTO
            {
                Parent = converterContext.LiftFolder.CreateDTO(data.Parent),
                Lifts = data.Lifts?.Select(l => converterContext.Lift.CreateDTO(l)).ToList(),
                SubFolders = data.SubFolders?.Select(f => converterContext.LiftFolder.CreateDTO(f)).ToList()
            };
        }

        public override LiftFolderDTO OnCreateDTO(LiftFolder data)
        {
            return new LiftFolderDTO
            {
                ID = data.ID,
                Name = data.Name,
                ParentID = data.ParentID
            };
        }
    }
}
