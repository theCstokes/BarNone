using Newtonsoft.Json;

namespace BarNone.Shared.DataTransfer.Core
{
    /// <summary>
    /// Base parent dto.
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <typeparam name="TDetailDTO">The type of the detail dto.</typeparam>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDTO{TDTO}" />
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.IParentDTO{TDetailDTO}" />
    public abstract class BaseParentDTO<TDTO, TDetailDTO> : BaseDTO<TDTO>, IParentDTO<TDetailDTO>
        where TDTO : BaseDTO<TDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        [JsonProperty(Order = int.MaxValue, NullValueHandling = NullValueHandling.Ignore)]
        public TDetailDTO Details { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        dynamic IParentDTO.Details
        {
            get
            {
                return Details;
            }
            set
            {
                Details = value;
            }
        }
    }
}
