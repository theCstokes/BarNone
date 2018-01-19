using BarNone.Shared.Core;
using BarNone.Shared.DomainModel.Core;

namespace BarNone.Shared.DomainModel.Core
{
    /// <summary>
    /// Domain Model
    /// </summary>
    /// <seealso cref="BarNone.Shared.Core.ITrackable" />
    public interface IDomainModel : ITrackable
    {

    }

    /// <summary>
    /// Generic domain model
    /// </summary>
    /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
    /// <seealso cref="BarNone.Shared.Core.ITrackable" />
    public interface IDomainModel<TDomainModel> : IDomainModel, ITrackable<TDomainModel>
        where TDomainModel : IDomainModel<TDomainModel>, new()
    {

    }
}
