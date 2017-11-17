using BarNone.Shared.DTOTransformable.Core;
using BarNone.Shared.DataTransfer.Core;

namespace BarNone.TheRack.DomainModel.Core
{
    public interface IDomainModel : IDTOTransformable
    {
        int ID { get; set; }
    }

    public interface IDomainModel<TDTO> : IDomainModel, IDTOTransformable<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
    {
    }
}
