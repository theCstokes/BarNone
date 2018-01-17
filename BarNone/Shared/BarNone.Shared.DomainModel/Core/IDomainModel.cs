using BarNone.Shared.Core;
using BarNone.Shared.DomainModel.Core;

namespace BarNone.Shared.DomainModel.Core
{
    public interface IDomainModel : ITrackable
    {

    }

    public interface IDomainModel<TDomainModel> : IDomainModel, ITrackable<TDomainModel>
        where TDomainModel : IDomainModel<TDomainModel>, new()
    {

    }
}
