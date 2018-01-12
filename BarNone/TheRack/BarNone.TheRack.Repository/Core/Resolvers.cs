using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.Repository.Core
{
    public static class Resolvers
    {
        public delegate ConvertConfig ConfigResolver();

        //public delegate BaseDataConverter<TDomainModel, TDTO, TConverters> ConverterResolverDelegate<TDomainModel, TDTO, TConverters>()
        //    where TDomainModel : ITrackable<TDomainModel>, new()
        //    where TDTO : ITrackableDTO<TDTO>, new()
        //    where TConverters : IConverter;

        public delegate TDomainModel ConverterResolverDelegate<TDomainModel, TDTO>(TDTO dto)
            where TDomainModel : ITrackable<TDomainModel>, new()
            where TDTO : ITrackableDTO<TDTO>, new();

        public delegate DbSet<TDomainModel> SetResolverDelegate<TDomainModel>(DomainContext context)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();

        //public delegate BaseDetailDataConverter<TDomainModel, TDTO, TDetailDTO, TConverters>
        //    DetailConverterResolverDelegate<TDomainModel, TDTO, TDetailDTO, TConverters>()
        //    where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        //    where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        //    where TDomainModel : class, IDomainModel<TDomainModel>, new()
        //    where TConverters : IConverter;

        public delegate IQueryable<TDomainModel> EntityResolverDelegate<TDomainModel>(DbSet<TDomainModel> set)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();

        public delegate IQueryable<TDomainModel> DetailResolverDelegate<TDomainModel>(IQueryable<TDomainModel> set)
            where TDomainModel : class, IDomainModel<TDomainModel>, new();
    }
}
