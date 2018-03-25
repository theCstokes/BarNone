using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public class DataAccessAuthenticator
    {
        public static User Login(string userName, string password)
        {
            using (var dc = new UserCredentialDomainContext())
            {
                var userResult = dc.Users.Where(c => c.UserName == userName);
                if (!userResult.Any()) return null;

                var user = userResult.FirstOrDefault();
                var credentialResult = dc.UserCredentials.Where(c => c.UserID == user.ID);
                if (!credentialResult.Any()) return null;

                var credential = credentialResult.FirstOrDefault();
                if (credential.Password == password)
                {
                    return user;
                }

            }
            return null;
        }

        public static User Create(User user, string salt, string password)
        {
            using (var dc = new UserCredentialDomainContext())
            {
                var userEntity = dc.Users.Add(user);
                dc.SaveChanges();
                var credentialEntity = dc.UserCredentials.Add(new UserCredential
                {
                    UserID = userEntity.Entity.ID,
                    Salt = salt,
                    Password = password
                });
                dc.SaveChanges();
                return userEntity.Entity;
            }
        }
    }
}
