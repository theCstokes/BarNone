using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;

namespace BarNone.TheRack.DomainModel.Core
{
    public interface IDetailDomainModel : IDomainModel
    {
    }

    public interface IDetailDomainModel<TDTO, TDetailDTO> : IDetailDomainModel, IDomainModel<TDTO>, IDetailDTOTransformable<TDTO, TDetailDTO>
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
    }
}
