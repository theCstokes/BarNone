using BarNone.Shared.DataTransfer.Core;
using System;

namespace BarNone.Shared.DTOTransformable.Core
{
    public abstract class BaseDataModel<TDataModel, TDTO, TDetailDTO>
        : DetailDTOTransformable<TDataModel, TDTO, TDetailDTO>, IDetailDTOTransformable<TDTO, TDetailDTO>
        where TDataModel : BaseDataModel<TDataModel, TDTO, TDetailDTO>, IDetailDTOTransformable<TDTO, TDetailDTO>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    { 

        #region Public Static Member(s).
        ///// <summary>
        ///// Creates Domain Model from DTO.
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        public static TDataModel CreateFromDTO(TDTO dto)
        {
            var dm = new TDataModel();
            dm.PopulateFromDTO(dto);
            return dm;
        }
        
        #endregion

    }
}
