using AutoMapper;
using Spartacus.BusinessLogic.DBContext;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.Trainer;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Core
{
    public class MainApi
    {
        internal List<CatTable> GetCatsAction()
        {
            using var debil = new GymContext();
            var cats = debil.Categories.ToList();
            return cats;
        }

        internal CatTable GetCatByIdAction(int id)
        {
            using var debil = new GymContext();
            var cat = debil.Categories.FirstOrDefault(c => c.Id == id);
            return cat;
        }

        internal Task SendEmailAsyncAction(string email, string subject, string message)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password)
            };

            return client.SendMailAsync(
                new MailMessage(from: smtpSection.Network.UserName,
                to: email,
                subject,
                message)
                { IsBodyHtml = true });
        }

        internal string PopulateBodyAction(string userEmail, string url, string templatePath)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(templatePath)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{userEmail}", userEmail);
            body = body.Replace("{actionUrl}", url);
            return body;
        }

        internal string CreateTokenAction<TEntity>(string email, int minutes) where TEntity : class, IToken, new()
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Email == email);
                if (user == null) return null;
            }

            string value;
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[12]; // Use a multiple of 3 (e.g. 3, 6, 12) to prevent output with trailing padding '=' characters in Base64).
                rng.GetBytes(bytes);

                // The `.Replace()` methods convert the Base64 string returned from `ToBase64String` to Base64Url.
                value = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
            }

            using (var debil = new TokenContext())
            {
                var hashedValue = LoginHelpers.HashGen(value);
                var token = debil.Set<TEntity>().FirstOrDefault(t => t.Value == hashedValue);
                //var token = debil.ResetTokens.FirstOrDefault(t => t.Value == hashedValue);
                if (token != null) return null;

                debil.Set<TEntity>().Add(new TEntity
                {
                    Value = hashedValue,
                    Email = email,
                    EndDate = DateTime.Now.AddMinutes(minutes),
                });

                debil.SaveChanges();
                
                // TODO: RemoveExpiredResetTokensAction();
            }
            return value;
        }

        internal bool IsResetTokenValidAction(string value)
        {
            var hashedValue = LoginHelpers.HashGen(value);
            using var debil = new TokenContext();
            var token = debil.ResetTokens.FirstOrDefault(t => t.Value == hashedValue);
            if (token == null) return false;

            RemoveExpiredTokensAction<ResetToken>();
            return token.EndDate > DateTime.Now;
        }

        internal ConTokenResp ConfirmRegisterTokenAction(string value)
        {
            var hashedValue = LoginHelpers.HashGen(value);
            using (var debil = new TokenContext())
            {
                var token = debil.RegisterTokens.FirstOrDefault(t => t.Value == hashedValue);
                if (token == null) return ConTokenResp.Expired;

                using (var debil1 = new UserContext())
                {
                    var user = debil1.Users.FirstOrDefault(u => u.Email == token.Email);
                    if (user == null) return ConTokenResp.Failed;

                    if (token.EndDate < DateTime.Now)
                    {
                        debil1.Users.Remove(user);
                    }
                    else
                    {
                        user.IsConfirmed = true;
                        debil.RegisterTokens.Remove(token);
                    }

                    debil.SaveChanges();
                    debil1.SaveChanges();
                }
            }

            RemoveExpiredTokensAction<RegisterToken>();
            return ConTokenResp.Success;
        }

        internal bool ResetPasswordByTokenAction(string value, string newPassword)
        {
            string userEmail;
            using (var debil = new TokenContext())
            {
                var token = debil.ResetTokens.FirstOrDefault(t => t.Value == value);
                if (token == null) return false;
                userEmail = token.Email;

                RemoveExpiredTokensAction<ResetToken>();
            }

            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null) return false;

                user.Password = newPassword;
                debil.SaveChanges();
            }
            return true;
        }

        private void RemoveExpiredTokensAction<TEntity>() where TEntity : class, IToken, new()
        {
            using var debil = new TokenContext();
            debil.Set<TEntity>().RemoveRange(debil.Set<TEntity>().Where(t => t.EndDate < DateTime.Now));
            debil.SaveChanges();
        }

        internal void SendFeedbackAction(FeedData data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FeedData, FBTable>());
            var feed = config.CreateMapper().Map<FBTable>(data);

            using var debil = new UserContext();
            debil.Feedbacks.Add(feed);
            debil.SaveChanges();
        }

        internal List<TrainerData> GetTrainersAction()
        {
            using var debil = new UserContext();
            var trainers = debil.Trainers.Include(t => t.User).Select(t =>
                new TrainerData
                {
                    Firstname = t.User.Firstname,
                    Lastname = t.User.Lastname,
                    FileName = t.User.FileName,
                    FacebookUrl = t.FacebookUrl,
                    InstagramUrl = t.InstagramUrl,
                    Activity = t.Activity,
                    Bio = t.Bio
                }).ToList();
            return trainers;
        }
    }
}
