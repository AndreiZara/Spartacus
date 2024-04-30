using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class AdminApi
    {
        public void AddUserAction(UTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Users.Add(user);
                debil.SaveChanges();
            }
        }

        public List<UTable> GetUsersAction()
        {
            List<UTable> users;
            using (var debil = new UserContext())
            {
                users = debil.Users.Include(u => u.Membership).ToList();
            }
            return users;
        }

        public UTable GetUserByIdAction(int id)
        {
            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.SingleOrDefault(u => u.Id == id);
            }
            return user;
        }

        public bool UpdateUserAction(UTable data)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(x => x.Id == data.Id);

                if (user == null) return false;

                user.Username = data.Username;
                user.Firstname = data.Firstname;
                user.Lastname = data.Lastname;
                user.Email = data.Email;
                user.Password = data.Password;
                user.LastIp = data.LastIp;
                user.LastLogin = data.LastLogin;
                user.Level = data.Level;

                debil.SaveChanges();
            }
            return true;

        }

        public bool DeleteUserByIdAction(int id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Find(id);
                if (user == null) return false;
                debil.Users.Remove(user);
                debil.SaveChanges();
            }
            return true;
        }

        public bool UpdateCategory(CatTable data)
        {
            using (var debil = new CategoryContext())
            {
                var cat = debil.Categories.FirstOrDefault(x => x.Id == data.Id);

                if (cat == null) return false;
            
                cat.Title = data.Title;
                cat.Description = data.Description;
                cat.PriceOneYear = data.PriceOneYear;
                cat.PriceSixMonths = data.PriceSixMonths;
                cat.PriceThreeMonths = data.PriceThreeMonths;
                cat.PriceOneMonth = data.PriceOneMonth;

                debil.SaveChanges();
            }
            return true;
        }

        public void AddCategory(CatTable table)
        {
            using (var debil = new CategoryContext())
            {
                debil.Categories.Add(table);
                debil.SaveChanges();
            }
        }

        public List<CatTable> GetCategories()
        {
            List<CatTable> cats;
            using (var debil = new CategoryContext())
            {
                cats = debil.Categories.ToList();
            }
            return cats;
        }

        public CatTable GetCategoryById(int id)
        {
            CatTable cat;
            using (var debil = new CategoryContext())
            {
                cat = debil.Categories.FirstOrDefault(c => c.Id == id);
            }
            return cat;
        }
    }
}
