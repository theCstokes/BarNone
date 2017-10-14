using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.Response;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    public class Endpoint<TDTO> : MapEnum<Endpoint<TDTO>>
        //where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
        where TDTO : BaseDTO<TDTO>, new()
    {
        #region Private Field(s).
        private readonly string _url;
        #endregion

        #region Public Constructor(s).
        public Endpoint(string url)
        {
            _url = url;
        }
        #endregion

        #region MapEnum Implementation.
        protected override Endpoint<TDTO> Instance => this;
        #endregion

        #region Public Member(s).
        public async Task<TDTO> Get(int id)
        {
            var result = await DataRequest.Get<ResponseEntityDTO<TDTO>>(CreateURL($"{_url}/{id}"));
            return result?.Entity;
        }

        public async Task<List<TDTO>> GetAll()
        {
            var result = await DataRequest.Get<ResponseEnumerableDTO<TDTO>>(CreateURL($"{_url}"));
            return result?.Entities.ToList();
        }

        public async Task<TDTO> Post(TDTO dto)
        {
            var result = await DataRequest.Post<ResponseEntityDTO<TDTO>>(CreateURL($"{_url}"), dto);
            return result?.Entity;
        }
        #endregion

        #region Private Member(s).
        private string CreateURL(string path)
        {
            return @"http://localhost:58428/api/v1/" + path;
        }
        #endregion
    }
}
