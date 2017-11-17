using BarNone.Shared.DTOTransformable.Core;
using BarNone.Shared.DataTransfer.Core;

namespace BarNone.TheRack.DomainModel.Core
{
    public abstract class BaseDomainModel<TDomainModel, TDTO> : DTOTransformable<TDomainModel, TDTO>, IDomainModel<TDTO> 
        where TDomainModel : DTOTransformable<TDomainModel, TDTO>, IDomainModel<TDTO>, new()
        where TDTO : BaseDTO<TDTO>, new()
    {
        public abstract int ID { get; set; }
    }

    public abstract class BaseDetailDomainModel<TDomainModel, TDTO, TDetailDTO> : DetailDTOTransformable<TDomainModel, TDTO, TDetailDTO>, IDomainModel<TDTO, TDetailDTO>
        where TDomainModel : BaseDetailDomainModel<TDomainModel, TDTO, TDetailDTO>, IDetailDomainModel<TDTO, TDetailDTO>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        public abstract int ID { get; set; }
    }

}
