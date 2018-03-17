using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Flex;

namespace BarNone.DataLift.APIRequest
{
    public class DataManager
    {
        #region Endpoint Type(s).
        /// <summary>
        /// Endpoint for User data
        /// </summary>
        public static Endpoint<UserDTO> Users { get; } =
            new Endpoint<UserDTO>("User");

        /// <summary>
        /// Endpoint for lift data
        /// </summary>
        public static Endpoint<LiftDTO> Lifts { get; } =
            new Endpoint<LiftDTO>("Lift");

        /// <summary>
        /// Endpoint for Lift Flex data
        /// </summary>
        public static Endpoint<LiftDTO> LiftFlex { get; } =
            new Endpoint<LiftDTO>("Flex");

        /// <summary>
        /// Endpint for Flex data server
        /// </summary>
        public static Endpoint<FlexDTO> Flex { get; } =
            new Endpoint<FlexDTO>("Flex");

        /// <summary>
        /// Endpoint for Body data
        /// </summary>
        public static Endpoint<BodyDataDTO> Bodies { get; } =
            new Endpoint<BodyDataDTO>("BodyData");

        //public static Endpoint<>
        #endregion
    }
}
