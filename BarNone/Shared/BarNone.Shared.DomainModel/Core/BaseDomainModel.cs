using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DomainModel.Core
{
    public abstract class BaseDomainModel<TDomainModel, TDTO> : IDomainModel
        where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
        where TDTO : BaseDTO<TDTO>, new()
    {
        #region Public Abstract Property(s).
        /// <summary>
        /// Entity ID.
        /// </summary>
        public abstract int ID { get; set; }
        #endregion

        #region Public Abstract Member(s).
        /// <summary>
        /// Construct a DTO of type TDTO for this Domain Model.
        /// </summary>
        /// <returns>The Domain Model as a TDTO</returns>
        public abstract TDTO BuildDTO();

        /// <summary>
        /// Populates Domain Model from dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public abstract void PopulateFromDTO(TDTO dto);
        #endregion

        #region Public Static Member(s).
        /// <summary>
        /// Creates Domain Model from DTO.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TDomainModel CreateFromDTO(TDTO dto)
        {
            var dm = new TDomainModel();
            dm.PopulateFromDTO(dto);
            return dm;
        }
        #endregion
    }
}
