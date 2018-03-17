using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.Response;
using BarNone.TheRack.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    /// <summary>
    /// Class for URL endpoints on the database which can be controlled by datalift
    /// </summary>
    /// <typeparam name="TDTO"></typeparam>
    public class Endpoint<TDTO> : MapEnum<Endpoint<TDTO>>
        where TDTO : BaseDTO<TDTO>, new()
    {
        public static readonly string BASE_URL = "172.20.10.2:81";
        #region Private Field(s).
        /// <summary>
        /// URL to database table which handles the <see cref="TDTO"/> type 
        /// </summary>
        private readonly string _url;
        #endregion

        #region Public Constructor(s).
        /// <summary>
        /// Creates a new end point which handles the database structures at <paramref name="url"/>
        /// </summary>
        /// <param name="url">Location on the DB information being handled</param>
        public Endpoint(string url)
        {
            _url = url;
        }
        #endregion

        #region MapEnum Implementation.
        /// <summary>
        /// Linking used in <see cref="TheRack.Core.MapEnum{TElement}"/>
        /// </summary>
        protected override Endpoint<TDTO> Instance => this;
        #endregion

        #region Public Member(s).
        /// <summary>
        /// Fetches the TDTO with <paramref name="id"/> without it's details from this endpoint
        /// </summary>
        /// <param name="id">Id of the object on the server</param>
        /// <returns>The TDTO object with <paramref name="id"/> and null if it does not exist</returns>
        public async Task<TDTO> Get(int id)
        {
            var result = await DataRequest.Get<ResponseEntityDTO<TDTO>>(CreateURL($"{_url}/{id}"));
            return result?.Entity;
        }

        /// <summary>
        /// Fetches the TDTO with <paramref name="id"/> with it's details from this endpoint
        /// </summary>
        /// <param name="id">Id of the object on the server</param>
        /// <returns>The TDTO object and the objects details with <paramref name="id"/> and null if it does not exist</returns>
        public async Task<TDTO> GetWithDetails(int id)
        {
            var result = await DataRequest.Get<ResponseEntityDTO<TDTO>>(CreateURL($"{_url}/{id}/Detail"));
            return result?.Entity;
        }

        /// <summary>
        /// Fetches all TDTO without it's details from this endpoint
        /// </summary>
        /// <returns>The TDTO objects on the db and null if none exist</returns>
        public async Task<List<TDTO>> GetAll()
        {
            var result = await DataRequest.Get<ResponseEnumerableDTO<TDTO>>(CreateURL($"{_url}"));
            return result?.Entities.ToList();
        }

        /// <summary>
        /// Posts <paramref name="dto"/> to this endpoint
        /// </summary>
        /// <param name="dto">Dto to post</param>
        /// <returns>Result of post</returns>
        public async Task<TDTO> Post(TDTO dto)
        {
            var result = await DataRequest.Post<ResponseEntityDTO<TDTO>>(CreateURL($"{_url}"), dto);
            return result?.Entity;
        }

        /// <summary>
        /// Post a byte array to the endpoint
        /// </summary>
        /// <param name="data">Data to post</param>
        /// <returns>Void task</returns>
        public async Task Post(byte[] data)
        {
            var result = await DataRequest.Post<ResponseEntityDTO<TDTO>>(CreateURL($"{_url}"), data);
        }
        #endregion

        #region Private Member(s).
        /// <summary>
        /// Creates the full path to the table being dealt with
        /// </summary>
        /// <param name="path">Formed url to the table relative to the database root</param>
        /// <returns>The full path in the database</returns>
        private string CreateURL(string path)
        {
            return $"http://{BASE_URL}/api/v1/" + path;
        }
        #endregion
    }
}
