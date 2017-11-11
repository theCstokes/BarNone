using Newtonsoft.Json;

namespace BarNone.Shared.DataTransfer.Core
{
    public abstract class BaseParentDTO<TDTO, TDetailDTO> : BaseDTO<TDTO>, IParentDTO<TDetailDTO>
        where TDTO : new()
        where TDetailDTO: BaseDetailDTO<TDetailDTO>, new()
    {
        //public BaseParentDTO()
        //{
        //    Details = new TDetailDTO();
        //}

        [JsonProperty(Order = int.MaxValue, NullValueHandling = NullValueHandling.Ignore)]
        public TDetailDTO Details { get; set; }


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
