using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DTOTransformable.Core
{
    public interface IDetailDTOTransformable : IDTOTransformable
    {
    }

    public interface IDetailDTOTransformable<TDTO, TDetailDTO> : IDetailDTOTransformable, IDTOTransformable<TDTO>
        //where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
    }
}
