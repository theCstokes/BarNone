using BarNone.Shared.DataTransfer.Core;

namespace BarNone.DataLift.DomainModel.Core
{
    public abstract class BaseDomainModel<TDTO, TDetailDTO>
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        /// <summary>
        /// Construct a DTO of type TDTO for this Domain Model
        /// </summary>
        /// <returns>The Domain Model as a TDTO</returns>
        public abstract TDTO BuildDTO();

    }
}
