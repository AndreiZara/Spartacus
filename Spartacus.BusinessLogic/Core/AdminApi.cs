using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.Services;
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


        internal void AddDetailAction(MenDetTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Details.Add(user);
                debil.SaveChanges();
            }
        }

        internal List<MenDetTable> ReadDetailAction()
        {
            using (var debil = new UserContext())
            {
                var userContext = debil.Details.ToList();
                return userContext;
            }
        }


        internal bool UpdateDetailAction(MenDetTable user, int Id)
        {
            using (var debil = new UserContext())
            {
                var data = debil.Details.FirstOrDefault(x => x.Id == user.Id);

                if (data != null)
                {
                    data.Username = user.Username;
                    data.Activity = user.Activity;
                    data.Description = user.Description;
                    debil.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        internal bool DeleteDetailAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Details.FirstOrDefault(u => u.Id == Id);
                if (user != null)
                {
                    debil.Details.Remove(user);
                    debil.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        internal MenDetTable GetDetailByUsernameAction(string Username)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Details.Where(u => u.Username == Username).FirstOrDefault();
                return user;
            }
        }

        internal MenDetTable GetDetailByIdAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Details.Where(u => u.Id == Id).SingleOrDefault();
                return user;
            }
        }

        internal MenDetTable GetDetailByActivityAction(string Activity)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Details.Where(u => u.Activity == Activity).SingleOrDefault();
                return user;
            }
        }


        internal void AddServiceAction(SerTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Services.Add(user);
                debil.SaveChanges();
            }
        }

        internal List<SerTable> ReadServiceAction()
        {
            using (var debil = new UserContext())
            {
                var userContext = debil.Services.ToList();
                return userContext;
            }
        }


        internal bool UpdateServiceAction(SerTable user, int Id)
        {
            using (var debil = new UserContext())
            {
                var data = debil.Services.FirstOrDefault(x => x.ServiceId == user.ServiceId);

                if (data != null)
                {
                    data.Title = user.Title;
                    data.Description = user.Description;
                    data.File = user.File;
                    debil.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        internal bool DeleteServiceAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Services.FirstOrDefault(u => u.ServiceId == Id);
                if (user != null)
                {
                    debil.Services.Remove(user);
                    debil.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        internal SerTable GetServiceByTitleAction(string Title)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Services.Where(u => u.Title == Title).SingleOrDefault();
                return user;
            }
        }


        internal SerTable GetServiceByIdAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Services.Where(u => u.ServiceId == Id).SingleOrDefault();
                return user;
            }
        }

        //Feedback functionality

        internal void AddFeedbackAction(FBTable table)
        {
            using (var debil = new FeedbackContext())
            {
                debil.FBTables.Add(table);
                debil.SaveChanges();
            }
        }

        internal List<FBTable> ReadFeedbackAction()
        {
            using (var debil = new FeedbackContext())
            {
                var userContext = debil.FBTables.ToList();
                return userContext;
            }
        }


        internal bool UpdateFeedbackAction(FBTable table, int Id)
        {
            using (var debil = new FeedbackContext())
            {
                var data = debil.FBTables.FirstOrDefault(x => x.Id == Id);

                if (data != null)
                {
                    data.Username = table.Username;
                    data.Email = table.Email;
                    data.Subject = table.Subject;
                    data.Message = table.Message;

                    debil.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        internal bool DeleteFeedbackAction(int Id)
        {
            using (var debil = new FeedbackContext())
            {
                var user = debil.FBTables.FirstOrDefault(u => u.Id == Id);
                if (user != null)
                {
                    debil.FBTables.Remove(user);
                    debil.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        internal FBTable GetFeedbackByIdAction(int Id)
        {
            using (var debil = new FeedbackContext())
            {
                var user = debil.FBTables.Where(u => u.Id == Id).SingleOrDefault();
                return user;
            }
        }


        internal FBTable GetFeedbackByEmailAction(string Email)
        {
            using (var debil = new FeedbackContext())
            {
                var user = debil.FBTables.Where(u => u.Email == Email).SingleOrDefault();
                return user;
            }
        }
    }
}
