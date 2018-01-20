using BarNone.Shared.Core;

namespace BarNone.DataLift.DomainModel.Core
{
    /// <summary>
    /// Constraints for Kinect Data Models
    /// </summary>
    /// <typeparam name="TDataModel"></typeparam>
    public interface IDataModel<TDataModel> : ITrackable<TDataModel>
        where TDataModel : IDataModel<TDataModel>, new()
    {
    }
}
