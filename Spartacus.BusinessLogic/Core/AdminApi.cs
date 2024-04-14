﻿using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Core
{
    public class AdminApi
    {
        public void AddUser(UDbTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Users.Add(user);
                debil.SaveChanges();
            }
        }

        public List<UDbTable> ReadUser()
        {
            using (var debil = new UserContext())
            {
                var userContext = debil.Users.ToList();
                return userContext;
            }
        }

        public UDbTable GetUserByUsername(string Username)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.SingleOrDefault(u => u.Username == Username);
                return user;
            }
        }

        public bool UpdateUser(UDbTable user, int Id)
        {
            using (var debil = new UserContext())
            {
                var data = debil.Users.FirstOrDefault(x => x.Id == user.Id);

                if (data != null)
                {
                    data.Username = user.Username;
                    data.Password = user.Password;
                    data.Firstname = user.Firstname;
                    data.Lastname = user.Lastname;  
                    data.Email = user.Email;
                    data.LastLogin = user.LastLogin;
                    data.LastIp = user.LastIp;
                    data.Id = user.Id;
                    data.Level = user.Level;
                    debil.SaveChanges();

                    return true;
                }

            }

            return false;
        }

    }
}

/*
 
 fnjsdfks dsjfksdf dkfksdf kdsf dsfksd fsd fksd f

 
 
 
 
 
 
 */