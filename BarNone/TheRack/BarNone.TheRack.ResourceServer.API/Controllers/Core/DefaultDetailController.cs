using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using BarNone.TheRack.Repository.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Core
{
    public class DefaultDetailController<TDTO, TDomainModel, TRepo> : DetailController<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel, TDTO>, new()
        where TRepo : BaseRepository<TDTO, TDomainModel>
    {

        #region Public Delegate Definition(s).
        public delegate TRepo RepoBuilder();
        #endregion

        #region Private Field(s).
        private RepoBuilder _builder;
        #endregion

        #region Public Constructor(s).
        public DefaultDetailController(RepoBuilder builder)
        {
            _builder = builder;
        }
        #endregion

        #region Public DetailController Implementation.
        public override IActionResult Delete(int id)
        {
            using (var repo = _builder())
            {
                return EntityResponse.Response(repo.Remove(id));
            }
        }

        public override IActionResult GetAll()
        {
            using (var repo = _builder())
            {
                return EntityResponse.Enumerable(repo.Get());
            }
        }

        public override IActionResult GetByID(int id)
        {
            using (var repo = _builder())
            {
                return EntityResponse.Response(repo.Get(id));
            }
        }

        public override IActionResult GetWithDetailsByID(int id)
        {
            using (var repo = _builder())
            {
                return EntityResponse.DetailResponse(repo.GetWithDetails(id));
            }
        }

        public override IActionResult Post([FromBody]TDTO dto)
        {
            using (var repo = _builder())
            {
                return EntityResponse.Response(repo.Create(dto));
            }
        }

        public override IActionResult Put(int id, TDTO dto)
        {
            using (var repo = _builder())
            {
                return EntityResponse.Response(repo.Update(id, dto));
            }
        } 
        #endregion
    }
}
