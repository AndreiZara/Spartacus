using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.User;
using Spartacus.Helpers;

namespace Spartacus.BusinessLogic.Core
{
    public class UserApi
    {
        internal ULoginResp UserLoginAction(ULoginData data)
        {
            UTable result;
            RegisterToken token;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credential))
            {
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Name && u.Password == data.Password);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    token = todo.RegTokens.FirstOrDefault(t => t.Email == result.Email);
                    if(token != null)
                    {
                        if(DateTime.Now < token.EndDate && token.Status == Domain.Enums.TokenStatus.Default)
                        {
                            result.LastIp = data.Ip;
                            result.LastLogin = data.LoginDateTime;
                            todo.Entry(result).State = EntityState.Modified;
                            todo.SaveChanges();
                        }
                        else if (DateTime.Now > token.EndDate&& token.Status == Domain.Enums.TokenStatus.Default)
                        {
                            new AdminApi().DeleteUserAction(result.Id);
                            new MainApi().DeleteRegTokenAction(token.Id);
                            return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                        }
                    }
                }

                return new ULoginResp() { Status = true };
            }

            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    token = todo.RegTokens.FirstOrDefault(t => t.Email == result.Email);
                    if (token != null)
                    {
                        if (DateTime.Now < token.EndDate && token.Status == Domain.Enums.TokenStatus.Default)
                        {
                            result.LastIp = data.Ip;
                            result.LastLogin = data.LoginDateTime;
                            todo.Entry(result).State = EntityState.Modified;
                            todo.SaveChanges();
                        }
                        else if (DateTime.Now > token.EndDate && token.Status == Domain.Enums.TokenStatus.Default)
                        {
                            new AdminApi().DeleteUserAction(result.Id);
                            new MainApi().DeleteRegTokenAction(token.Id);
                            return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                        }
                    }
                }

                return new ULoginResp { Status = true };
            }
        }

        internal UTable UserProfileAction(ULoginData data)
        {
            UTable user;

            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == data.Credential);
            }

            using (var db = new UserContext())
            {
                user = (from u in db.Users where u.Username == data.Credential select u).FirstOrDefault();
            }

            return user;
        }


        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(5);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(5)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<UTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }

    }
}

