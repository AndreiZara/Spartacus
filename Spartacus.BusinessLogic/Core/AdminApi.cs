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
        public void AddUser(UTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Users.Add(user);
                debil.SaveChanges();
            }
        }

        public List<UTable> ReadUser()
        {
            using (var debil = new UserContext())
            {
                var userContext = debil.Users.ToList();
                return userContext;
            }
        }

        public UTable GetUserByUsername(string Username)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.SingleOrDefault(u => u.Username == Username);
                return user;
            }
        }

        public bool UpdateUser(UTable user, int Id)
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

        public bool UpdateCategory(CategoryTable category, int Id)
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

        public void AddCategory(CategoryTable table)
        {
            using (var debil = new CatContext())
            {
                debil.Categories.Add(table);
                debil.SaveChanges();
            }
        }

        public List<CategoryTable> ReadCategory()
        {
            using (var debil = new CatContext())
            {
                var catContext = debil.Categories.ToList();
                return catContext;
            }
        }

        public CategoryTable GetCategoryById(int Id)
        {
            using (var debil = new CatContext())
            {
                var user = debil.Categories.Where(u => u.Id == Id).SingleOrDefault();
                return user;
            }
        }

        public CategoryTable GetParticularCategoryById(int Id)
        {
            using (var debil = new CatContext())
            {
                var user = debil.Categories.FirstOrDefault(u => u.Id == Id);
                return user;
            }
        }

        
    }
}
