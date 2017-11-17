﻿using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.Repository.Core
{
    public class DefaultDetailRepository<TDTO, TDomainModel> : BaseRepository<TDTO, TDomainModel>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : DomainModel<TDomainModel, TDTO>, new()
    {

        #region Public Delegate Definition(s).
        public delegate DbSet<TDomainModel> DbSetResolver(DomainContext context);

        public delegate IQueryable<TDomainModel> DetailResolver(IQueryable<TDomainModel> set);
        #endregion

        #region Private Field(s).
        private DbSetResolver _resolver;
        private DetailResolver[] _detailResolvers;
        #endregion

        #region Public Constructor(s).
        public DefaultDetailRepository(DbSetResolver resolver, params DetailResolver[] detailResolvers)
            : base(new DomainContext())
        {
            _resolver = resolver;
            _detailResolvers = detailResolvers;
        }

        public DefaultDetailRepository(DomainContext context, DbSetResolver resolver,
            params DetailResolver[] detailResolvers)
            : base(context)
        {
            _resolver = resolver;
            _detailResolvers = detailResolvers;
        }
        #endregion

        public override TDomainModel Create(TDTO dto)
        {
            var dm = DomainModel<TDomainModel, TDTO>.CreateFromDTO(dto);
            var result = _resolver(context).Add(dm);

            context.SaveChanges();
            return result.Entity;
        }

        public override List<TDomainModel> Get(FilterDTO.WhereFunc where = null)
        {
            if (where != null)
            {
                return _resolver(context)
                    .Where(b => where(b))
                    .ToList();
            }
            return _resolver(context).ToList();
        }

        public override TDomainModel Get(int id)
        {
            return _resolver(context).Where(b => b.ID == id).FirstOrDefault();
        }

        public override TDomainModel GetWithDetails(int id)
        {
            var dataSet = _resolver(context);

            var q = dataSet.AsQueryable();

            if (_detailResolvers.Count() > 0)
            {
                q = _detailResolvers.Aggregate(q, (result, resolver) =>
                {
                    return resolver(result);
                });
            }

            return q
                .Where(b => b.ID == id)
                .FirstOrDefault();
        }

        public override TDomainModel Remove(int id)
        {
            var TDomainModel = new TDomainModel
            {
                ID = id
            };

            var result = context.Remove(TDomainModel);
            return result.Entity;
        }

        public override TDomainModel Update(int id, TDTO dto)
        {

            dto.ID = id;

            var dm = DomainModel<TDomainModel, TDTO>.CreateFromDTO(dto);
            var result = _resolver(context).Update(dm);

            context.SaveChanges();
            return result.Entity;
        }
    }
}