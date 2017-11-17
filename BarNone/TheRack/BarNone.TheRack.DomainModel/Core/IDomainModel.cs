using BarNone.Shared.DTOTransformable.Core;
using BarNone.Shared.DataTransfer.Core;

namespace BarNone.TheRack.DomainModel.Core
{
    public interface IActivatable<TSource> where TSource : new()
    {

    }

    public interface IDomainModel : IDTOTransformable
    {
        int ID { get; set; }
    }

    public interface IDomainModel<TDomainModel, TDTO> : IDomainModel, IDTOTransformable<TDTO>
        where TDomainModel : class, IDomainModel<TDomainModel, TDTO>, new()
        where TDTO : BaseDTO<TDTO>, new()
    {
    }
}
