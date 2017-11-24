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
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Core
{
    public class DefaultDetailController<TDTO, TDomainModel, TRepo> : DetailController<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
        where TRepo : BaseRepository<TDomainModel, TDTO>
    {

        #region Public Delegate Definition(s).
        public delegate TRepo RepoBuilder();
        #endregion

        #region Private Field(s).
        private RepoBuilder _builder;

        private ConfigResolver _configResolver;
        #endregion

        #region Public Constructor(s).
        public DefaultDetailController(RepoBuilder builder, ConfigResolver configResolver = null)
        {
            _builder = builder;
            if (configResolver != null)
            {
                _configResolver = configResolver;
            } else
            {
                _configResolver = () => new ConvertConfig(1);
            }
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
                return EntityResponse.DetailResponse(repo.GetWithDetails(id), config: _configResolver());
            }
        }

        public override IActionResult Post([FromBody]TDTO dto)
        {
            using (var repo = _builder())
            {
                return EntityResponse.DetailResponse(repo.Create(dto), config: _configResolver());
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
