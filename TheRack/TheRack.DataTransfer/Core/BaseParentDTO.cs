using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DataTransfer.Core
{
    public abstract class BaseParentDTO<TDTO, TDetailDTO> : BaseDTO<TDTO>
        where TDTO : new()
        where TDetailDTO: BaseDetailDTO<TDetailDTO>, new()
    {
        //public BaseParentDTO()
        //{
        //    Details = new TDetailDTO();
        //}

        [JsonProperty(Order = int.MaxValue, NullValueHandling = NullValueHandling.Ignore)]
        public TDetailDTO Details { get; set; }
    }
}
