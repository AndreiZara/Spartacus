using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Core
{
    public class AdminApi
    {
        internal void AddUserAction(UTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Users.Add(user);
                debil.SaveChanges();
            }
        }

        internal List<UTable> ReadUserAction()
        {
            using (var debil = new UserContext())
            {
                var userContext = debil.Users.ToList();
                return userContext;
            }
        }


        internal bool UpdateUserAction(UTable user, int Id)
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
                    data.File = user.File;
                    data.FileName = user.FileName;
                    debil.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        internal bool DeleteUserAction(int Id)
        {
            using(var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Id == Id);
                if(user != null)
                {
                    debil.Users.Remove(user);
                    debil.SaveChanges();   
                    return true;
                }
                return false;
            }
        }

        internal UTable GetUserByIdAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Where(u => u.Id == Id).SingleOrDefault();
                return user;
            }
        }

        internal UTable GetUserByUsernameAction(string Username)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Where(u => u.Username == Username).SingleOrDefault();
                return user;
            }
        }

        internal UTable GetUserByEmailAction(string Email)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Where(u => u.Email == Email).SingleOrDefault();
                return user;
            }
        }

        internal UTable GetParticularUserByIdAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Id == Id);
                return user;
            }
        }


        internal void AddCategoryAction(CategoryTable table)
        {
            using (var debil = new CatContext())
            {
                debil.Categories.Add(table);
                debil.SaveChanges();
            }
        }

        internal bool UpdateCategoryAction(CategoryTable category, int Id)
        {
            using (var debil = new CatContext())
            {
                var newCat = debil.Categories.FirstOrDefault(x => x.Id == category.Id);

                if (newCat != null)
                {
                    newCat.Title = category.Title;
                    newCat.Description = category.Description;
                    newCat.Price_12 = category.Price_12;
                    newCat.Price_6 = category.Price_6;
                    newCat.Price_3 = category.Price_3;
                    newCat.Price_1 = category.Price_1;

                    debil.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        internal List<CategoryTable> ReadCategoryAction()
        {
            using (var debil = new CatContext())
            {
                var catContext = debil.Categories.ToList();
                return catContext;
            }
        }

        internal CategoryTable GetCategoryByIdAction(int Id)
        {
            using (var debil = new CatContext())
            {
                var category = debil.Categories.Where(cat => cat.Id == Id).SingleOrDefault();
                return category;
            }
        }

        internal CategoryTable GetParticularCategoryByIdAction(int Id)
        {
            using (var debil = new CatContext())
            {
                var category = debil.Categories.FirstOrDefault(cat => cat.Id == Id);
                return category;
            }
        }

        
    }
}
