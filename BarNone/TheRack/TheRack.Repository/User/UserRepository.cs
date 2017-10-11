using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using TheRack.DataAccess;
using TheRack.DataAdapter;
using TheRack.DataTransfer;
using TheRack.DomainModel;
using TheRack.Repository.Core;

namespace TheRack.Repository
{
    public class UserRepository : IRepository<UserDTO, User>
    {
        private static UserDataAdapter _adapter = new UserDataAdapter();

        public User Login(string userName, string password)
        {
            using (var context = new DomainContext())
            {
                var result = context.Users.Where(c => c.UserName == userName).First();

                if (result == null) return null;

                context.SaveChanges();

                if (result.Password == password) return result;

                return null;
            }
        }

        public List<User> Get()
        {
            using (var context = new DomainContext())
            {
                return context.Users.ToList();
            }
        }

        public User Get(int id)
        {
            using (var context = new DomainContext())
            {
                var result = context.Users.Where(c => c.ID == id).First();

                context.SaveChanges();

                return result;
            }
        }

        public User GetWithDetails(int id)
        {
            using (var context = new DomainContext())
            {
                var result = context
                    .Users
                    .Include(u => u.Account)
                    .Where(c => c.ID == id).First();

                context.SaveChanges();

                return result;
            }
        }

        public User Create(UserDTO dto)
        {
            using (var context = new DomainContext())
            {
                var entity = _adapter.GetDomainModel(dto);
                var result = context.Users.Add(entity);

                context.SaveChanges();

                return result.Entity;
            }
        }

        public User Update(int id, UserDTO dto)
        {
            using (var context = new DomainContext())
            {
                var entity = _adapter.GetDomainModel(dto);
                entity.ID = id;
                var result = context.Users.Update(entity);

                context.SaveChanges();

                return result.Entity;
            }
        }

        public User Remove(int id)
        {
            using (var context = new DomainContext())
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
}
