using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;

namespace BarNone.TheRack.DomainModel.Core
{
    public interface IDetailDomainModel : IDomainModel
    {
    }

    public interface IDetailDomainModel<TDomainModel, TDTO, TDetailDTO> : IDetailDomainModel, IDomainModel<TDomainModel, TDTO>, IDetailDTOTransformable<TDTO, TDetailDTO>
        where TDomainModel : class, IDomainModel<TDomainModel, TDTO>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
    }
}
