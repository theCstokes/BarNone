using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    /// <summary>
    /// LiftFolder converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDetailDataConverter{BarNone.Shared.DomainModel.LiftFolder, BarNone.Shared.DataTransfer.LiftFolderDTO, BarNone.Shared.DataTransfer.LiftFolderDetailDTO, BarNone.Shared.DataConverters.Converters}" />
    public class LiftFolderConverter 
        : BaseDetailDataConverter<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public LiftFolderConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override LiftFolder OnCreateDataModel(LiftFolderDTO dto)
        {
            return new LiftFolder
            {
                ID = dto.ID,
                Name = dto.Name,
                ParentID = dto.ParentID,
                UserID = context.UserID
            };
        }

        /// <summary>
        /// Called when [create detail data model].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dto">The dto.</param>
        public override void OnCreateDetailDataModel(LiftFolder data, LiftFolderDetailDTO dto)
        {
            data.Parent = converterContext.LiftFolder.CreateDataModel(dto.Parent);
            data.Lifts = dto.Lifts.Select(l => converterContext.Lift.CreateDataModel(l)).ToList();
            data.SubFolders = dto.SubFolders.Select(f => converterContext.LiftFolder.CreateDataModel(f)).ToList();
        }

        /// <summary>
        /// Creates detail dto.
        /// Called when [create detail dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override LiftFolderDetailDTO OnCreateDetailDTO(LiftFolder data)
        {
            return new LiftFolderDetailDTO
            {
                Parent = converterContext.LiftFolder.CreateDTO(data.Parent),
                Lifts = data.Lifts?.Select(l => converterContext.Lift.CreateDTO(l)).ToList(),
                SubFolders = data.SubFolders?.Select(f => converterContext.LiftFolder.CreateDTO(f)).ToList()
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
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
