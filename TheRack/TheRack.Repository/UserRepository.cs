using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheRack.DataAccess;
using TheRack.DomainModel;

namespace TheRack.Repository
{
    public class UserRepository
    {
        public static List<User> Get()
        {
            using (var context = new DomainContext())
            {
                return context.User.ToList();
            }
        }

        public static User Login(string userName, string password)
        {
            using (var context = new DomainContext())
            {
                var result = context.User.Where(c => c.UserName == userName).First();

                if (result == null) return null;

                context.SaveChanges();

                if (result.Password == password) return result;

                return null;
            }
        }

        public static User Get(int id)
        {
            using (var context = new DomainContext())
            {
                var result = context.User.Where(c => c.ID == id).First();

                context.SaveChanges();

                return result;
            }
        }

        public static User Create(User entity)
        {
            using (var context = new DomainContext())
            {
                var result = context.User.Add(entity);

                context.SaveChanges();

                return result.Entity;
            }
        }

        public static User Update(int id, User entity)
        {
            using (var context = new DomainContext())
            {
                entity.ID = id;
                var result = context.User.Update(entity);

                context.SaveChanges();

                return result.Entity;
            }
        }

        public static User Remove(int id)
        {
            using (var context = new DomainContext())
            {
                var result = context.User.Remove(new User
                {
                    ID = id
                });

                context.SaveChanges();

                return result.Entity;
            }
        }
    }
}
