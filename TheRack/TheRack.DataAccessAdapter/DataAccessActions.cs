using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DomainModel;

namespace TheRack.DataAccessAdapter
{
    public class DataAccessActions
    {
        public delegate dynamic ReadAction<TSource>(TSource dto);

        public delegate void WriteAction<TSource>(TSource model, dynamic value)
            where TSource : BaseDomainModel<TSource>, new();

        public delegate TResult MapToDTO<TSource, TResult>(TSource model)
            where TSource : BaseDomainModel<TSource>, new();

        public delegate TResult MapToDomainModel<TSource, TResult>(TSource dto)
            where TResult : BaseDomainModel<TResult>, new();
    }
}
