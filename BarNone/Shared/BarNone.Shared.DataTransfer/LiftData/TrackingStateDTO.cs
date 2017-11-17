using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DataTransfer.LiftData
{
    public class TrackingStateDTO : BaseTypeDTO<TrackingStateDTO>
    {
        #region Public Property(s).
        public override int ID { get; set; }

        public override int Value { get; set; }

        public override string Name { get; set; }
        #endregion
    }
}
