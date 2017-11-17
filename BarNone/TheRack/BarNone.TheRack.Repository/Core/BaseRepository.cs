﻿using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.Repository.Core
{
    public abstract class BaseRepository<TDTO, TDomainModel> : IRepository<TDTO, TDomainModel>,
        IDisposable
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
    {
        #region Protected Read Only Field(s).
        protected readonly DomainContext context;
        #endregion

        #region Public Constructor(s).
        public BaseRepository(DomainContext context)
        {
            this.context = context;
        }
        #endregion

        #region Public IDisposable Implementation.
        public void Dispose()
        {
            context.Dispose();
        } 
        #endregion

        #region Public Abstract IRepository Implementation.
        public abstract TDomainModel Create(TDTO dto);
        public abstract List<TDomainModel> Get(FilterDTO.WhereFunc where = null);
        public abstract TDomainModel Get(int id);
        public abstract TDomainModel GetWithDetails(int id);
        public abstract TDomainModel Remove(int id);
        public abstract TDomainModel Update(int id, TDTO dto);
        #endregion

        #region Public IRepository Implementation.
        List<IDTOTransformable> IRepository.Get(FilterDTO.WhereFunc where)
        {
            return Get(where).Select(v => v as IDTOTransformable).ToList();
        }

        IDTOTransformable IRepository.Get(int id)
        {
            return Get(id);
        }

        IDTOTransformable IRepository.GetWithDetails(int id)
        {
            return GetWithDetails(id);
        }

        //IDomainModel Create(dynamic dto)
        //{
        //    return Create(dto as TDTO);
        //}

        //IDomainModel Update(int id, dynamic dto)
        //{
        //    return Update(id, dto as TDTO);
        //}

        IDTOTransformable IRepository.Remove(int id)
        {
            return Remove(id);
        }

        IDTOTransformable IRepository.Create(dynamic dto)
        {
            return Create(dto as TDTO);
        }

        IDTOTransformable IRepository.Update(int id, dynamic dto)
        {
            return Update(id, dto as TDTO);
        }
        #endregion
    }
}
