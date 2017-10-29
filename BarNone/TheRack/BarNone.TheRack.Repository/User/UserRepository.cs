using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

namespace BarNone.TheRack.Repository
{
    public class UserRepository : BaseRepository<UserDTO, User>
    {
        public UserRepository() : base(new DomainContext())
        {
        }

        public UserRepository(DomainContext context) : base(context)
        {

        }

        public User Login(string userName, string password)
        {
            var result = context.Users.Where(c => c.UserName == userName).First();

            if (result == null) return null;

            context.SaveChanges();

            if (result.Password == password) return result;

            return null;
        }

        public override List<User> Get(WhereFunc where = null)
        {
            if (where != null)
            {
                return context.Users
                    .Where((u) => where(u))
                    .ToList();
            }
            return context.Users.ToList();
        }

        public override User Get(int id)
        {
            var result = context.Users.Where(c => c.ID == id).First();

            context.SaveChanges();

            return result;
        }

        public override User GetWithDetails(int id)
        {
            var result = context
                .Users
                //.Include(u => u.Account)
                .Where(c => c.ID == id).First();

            context.SaveChanges();

            return result;
        }

        public override User Create(UserDTO dto)
        {
            var entity = User.CreateFromDTO(dto);
            var result = context.Users.Add(entity);

            context.SaveChanges();

            return result.Entity;
        }

        public override User Update(int id, UserDTO dto)
        {
            var entity = User.CreateFromDTO(dto);
            entity.ID = id;
            var result = context.Users.Update(entity);

            context.SaveChanges();

            return result.Entity;
        }

        public override User Remove(int id)
        {
            var result = context.Users.Remove(new User
            {
                ID = id
            });

            context.SaveChanges();

            return result.Entity;
        }
    }
}
