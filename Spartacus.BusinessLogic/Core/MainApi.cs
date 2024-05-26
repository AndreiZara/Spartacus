using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using Spartacus.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            using (var debil = new CategoryContext())
            {
                var cats = debil.Categories.ToList();
                return cats;
            }
        }

        internal CatTable GetCatByIdAction(int id)
        {
            using (var debil = new CategoryContext())
            {
                var cat = debil.Categories.FirstOrDefault(c => c.Id == id);
                return cat;
            }
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

        internal string PopulateBodyAction(string userEmail, string url)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/Template/Email.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{userEmail}", userEmail);
            body = body.Replace("{actionUrl}", url);
            return body;
        }

        internal string CreateTokenAction(string email)
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
                var token = debil.ResetTokens.FirstOrDefault(t => t.Value == hashedValue);
                if (token != null) return null;

                debil.ResetTokens.Add(new ResetToken
                {
                    Value = hashedValue,
                    Email = email,
                    EndDate = DateTime.Now.AddMinutes(60),
                });

                debil.SaveChanges();
                RemoveExpiredResetTokensAction();
            }
            return value;
        }

        internal bool IsResetTokenValidAction(string value)
        {
            var hashedValue = LoginHelpers.HashGen(value);
            using (var debil = new TokenContext())
            {
                var token = debil.ResetTokens.FirstOrDefault(t => t.Value == hashedValue);
                if (token == null) return false;

                RemoveExpiredResetTokensAction();
                return token.EndDate > DateTime.Now;
            }
        }

        internal bool ResetPasswordByTokenAction(string value, string newPassword)
        {
            var hashedValue = LoginHelpers.HashGen(value);
            string userEmail;
            using (var debil = new TokenContext())
            {
                var token = debil.ResetTokens.FirstOrDefault(t => t.Value == hashedValue);
                if (token == null) return false;
                userEmail = token.Email;

                RemoveExpiredResetTokensAction();
            }

            using(var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null) return false;

                user.Password = newPassword;
                debil.SaveChanges();
            }
            return true;
        }

        private void RemoveExpiredResetTokensAction()
        {
            using (var debil = new TokenContext())
            {
                debil.ResetTokens.RemoveRange(debil.ResetTokens.Where(t => t.EndDate < DateTime.Now));
                debil.SaveChanges();
            }
        }
    }
}
