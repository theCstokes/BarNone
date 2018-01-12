using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DataConverters;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class UserRepository : DefaultRepository<User, UserDTO>
    {
        //protected override SetResolverDelegate SetResolver => (context) => context.Users;

        //protected override EntityResolverDelegate EntityResolver => (set) => set;

        //protected override ConverterResolverDelegate ConverterResolver => () => Converters.Convert.User;

        protected override ConverterResolverDelegate<User, UserDTO> DataConverter => (dto) => Converters.Convert.User.CreateDataModel(dto);

        protected override SetResolverDelegate<User> SetResolver => (context) => context.Users;

        protected override EntityResolverDelegate<User> EntityResolver => (set) => set;

        public UserRepository() : base()
        {
        }

        public UserRepository(DomainContext context) : base(context)
        {
        }

        public User Login(string userName, string password)
        {
            var result = entites.Where(c => c.UserName == userName);
            if (!result.Any()) return null;

            var user = result.FirstOrDefault();

            //context.SaveChanges();

            if (user.Password == password) return user;

            return null;
        }

        //public override List<User> Get(WhereFunc where = null)
        //{
        //    if (where != null)
        //    {
        //        return entites.Where((u) => where(u))
        //            .ToList();
        //    }
        //    return entites.ToList();
        //}

        //public override User Get(int id)
        //{
        //    var result = entites.Where(c => c.ID == id).First();

        //    //context.SaveChanges();

        //    return result;
        //}

        //public override User GetWithDetails(int id)
        //{
        //    var result = entites.Where(c => c.ID == id).First();

        //    //context.SaveChanges();

        //    return result;
        //}

        //public override User Create(UserDTO dto)
        //{
        //    var entity = Converters.Convert.User.CreateDataModel(dto);
        //    var result = Create(entity);

        //    //context.SaveChanges();

        //    return result;
        //}

        //public override User Update(int id, UserDTO dto)
        //{
        //    var entity = Converters.Convert.User.CreateDataModel(dto);
        //    entity.ID = id;
        //    var result = Update(entity);

        //    //context.SaveChanges();

        //    return result;
        //}

        //public override User Remove(int id)
        //{
        //    var result = Remove(new User
        //    {
        //        ID = id
        //    });

        //    //context.SaveChanges();

        //    return result;
        //}
    }
}
