using BarNone.Shared.DTOTransformable.Core;
using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.Core;

namespace BarNone.TheRack.DomainModel.Core
{
    //public interface IActivatable<TSource> where TSource : new()
    //{

    //}

    //public interface IDomainModel : IDTOTransformable
    //{
    //    int ID { get; set; }
    //}

    //public interface IDomainModel<TDomainModel, TDTO> : IDomainModel, IDTOTransformable<TDTO>
    //    where TDomainModel : class, IDomainModel<TDomainModel, TDTO>, new()
    //    where TDTO : BaseDTO<TDTO>, new()
    //{
    //}

    public interface IDomainModel : ITrackable
    {

    }

    public interface IDomainModel<TDomainModel> : IDomainModel, ITrackable<TDomainModel>
        where TDomainModel : IDomainModel<TDomainModel>, new()
    {

    }
}
