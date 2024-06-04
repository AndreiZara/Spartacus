using AutoMapper;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Spartacus.BusinessLogic.Core
{
    public class UserMgmtApi
    {
        internal bool AddUserAction(UTable data)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Username == data.Username);
                if (user != null) return false;

                debil.Users.Add(data);
                debil.SaveChanges();
            }
            return true;
        }

        internal List<UTable> GetUsersAction()
        {
            using (var debil = new UserContext())
            {
                var users = debil.Users.Include(u => u.Membership).ToList();
                return users;
            }
        }

        internal UTable GetUserByIdAction(int id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Include(u => u.Membership).SingleOrDefault(u => u.Id == id);
                return user;
            }
        }

        internal SaveProfResp UpdateUserAction(UTable data, HttpPostedFileBase image)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(x => x.Id == data.Id);
                if (user == null) return SaveProfResp.Failed;

                if (data.Username != user.Username)
                {
                    var exists = debil.Users.FirstOrDefault(u => u.Username == data.Username);
                    if (exists != null) return SaveProfResp.FailedUsername;

                    using (var debil1 = new SessionContext())
                    {
                        var current = debil1.Sessions.FirstOrDefault(s => s.Username == user.Username);
                        if (current != null)
                        {
                            debil1.Sessions.Remove(current);
                            debil1.SaveChanges();
                        }
                    }
                    user.Username = data.Username;
                }

                if (image != null)
                {
                    var newFileName = MediaHelper.SaveImageByUser(image, user);
                    if (newFileName == null) return SaveProfResp.FailedImage;
                    user.FileName = newFileName;
                }

                var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UTable>()
                    .ForMember(u => u.FileName, opt => opt.Ignore())
                    .ForMember(u => u.Password, opt => opt.Ignore())
                    .ForMember(u => u.Membership, opt => opt.Ignore())
                    .ForMember(u => u.Period, opt => opt.Ignore()));
                config.CreateMapper().Map(data, user);
                debil.SaveChanges();
            }
            return SaveProfResp.Success;
        }

        internal bool DeleteUserByIdAction(int id)
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
    }
}
