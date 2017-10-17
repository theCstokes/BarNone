using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DomainModel.Core
{
    public abstract class BaseChildDomainModel<TDomainModel, TDTO, TParentDomainModel, TParentDTO>
        : BaseDomainModel<TDomainModel, TDTO>
        where TDomainModel : BaseChildDomainModel<TDomainModel, TDTO, TParentDomainModel, TParentDTO>, new()
        where TDTO : BaseDTO<TDTO>, new()
        where TParentDomainModel : BaseDomainModel<TParentDomainModel, TParentDTO>, new()
        where TParentDTO : BaseDTO<TParentDTO>, new()
    {

        #region BaseDomainModel Partial Implementation.
        public override TDTO BuildDTO()
        {
            return BuildDTO(null);
        }

        public override void PopulateFromDTO(TDTO dto)
        {
            PopulateFromDTO(dto, null);
        }
        #endregion

        #region Public Abstract Member(s).
        public abstract TDTO BuildDTO(TParentDTO parentDTO);

        public abstract void PopulateFromDTO(TDTO dto, TParentDomainModel parent);
        #endregion

        #region Public Static Member(s).
        /// <summary>
        /// Creates Domain Model from DTO.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TDomainModel CreateFromDTO(TDTO dto, TParentDomainModel parent)
        {
            var dm = new TDomainModel();
            dm.PopulateFromDTO(dto, parent);
            return dm;
        }
        #endregion

    }
}
