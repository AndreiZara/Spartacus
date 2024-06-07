using AutoMapper;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.Trainer;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using System;
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
                var user = debil.Users.FirstOrDefault(u => u.Username == data.Username || u.Email == data.Email);
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
            using var debil = new UserContext();
            var user = debil.Users.Include(u => u.Membership).Include(u => u.Trainer).SingleOrDefault(u => u.Id == id);
            return user;
        }

        internal SaveProfResp UpdateUserAction(UProfData data, HttpPostedFileBase image)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(x => x.Email == data.Email);
                if (user == null) return SaveProfResp.Failed;

                if (data.Username != user.Username)
                {
                    var exists = debil.Users.FirstOrDefault(u => u.Username == data.Username) != null;
                    if (exists) return SaveProfResp.FailedUsername;

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

                var config = new MapperConfiguration(cfg => cfg.CreateMap<UProfData, UTable>()
                    .ForMember(u => u.FileName, opt => opt.Ignore())
                    .ForMember(u => u.Password, opt => opt.Ignore()));
                config.CreateMapper().Map(data, user);

                user.LastLogin = DateTime.Now;
                user.LastIp = HttpContext.Current.Request.UserHostAddress;


                if (user.Role == URole.Trainer)
                {
                    if (user.Trainer == null)
                    {
                        user.Trainer = new TDTable
                        {
                            Activity = data.Activity,
                            Bio = data.Bio,
                            InstagramUrl = data.InstagramUrl,
                            FacebookUrl = data.FacebookUrl
                        };
                    }
                    else
                    {
                        user.Trainer.Activity = data.Activity;
                        user.Trainer.Bio = data.Bio;
                        user.Trainer.InstagramUrl = data.InstagramUrl;
                        user.Trainer.FacebookUrl = data.FacebookUrl;
                    }
                }

                debil.SaveChanges();
            }
            return SaveProfResp.Success;
        }

        internal bool DeleteUserByIdAction(int id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Include(u => u.Membership).FirstOrDefault(u => u.Id == id);
                if (user == null) return false;

                if (user.Membership != null)
                {
                    using var debil1 = new GymContext();
                    var locId = user.Membership.LocId;

                    var loc = debil1.Locations.FirstOrDefault(l => l.Id == locId);
                    if (loc == null) return false;

                    loc.CurrentNoOfVisitors--;
                    loc.LastUpdate = DateTime.Now;
                    debil1.SaveChanges();
                }

                debil.Users.Remove(user);
                debil.SaveChanges();
            }
            return true;
        }

        internal void RemoveUnconfirmedUsersAction()
        {
            List<RegisterToken> tokens;
            using (var debil = new TokenContext())
            {
                tokens = debil.RegisterTokens.ToList();
            }

            using (var debil = new UserContext())
            {
                foreach (var token in tokens)
                {
                    if (token.EndDate < DateTime.Now)
                    {
                        var user = debil.Users.FirstOrDefault(u => u.Email == token.Email);
                        debil.Users.Remove(user);
                    }
                }
                debil.SaveChanges();
            }
        }
    }
}
