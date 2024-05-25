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
        public List<CatTable> GetCatsAction()
        {
            List<CatTable> cats;
            using (var debil = new CategoryContext())
            {
                cats = debil.Categories.ToList();
            }
            return cats;
        }

        public CatTable GetCatByIdAction(int id)
        {
            CatTable cat;
            using (var debil = new CategoryContext())
            {
                cat = debil.Categories.FirstOrDefault(c => c.Id == id);
            }
            return cat;
        }

        public Task SendEmailAsyncAction(string email, string subject, string message)
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

        public string PopulateBodyAction(string title, string url, string message)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/Template/Email.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", message);
            return body;
        }

        public string CreateTokenAction(string email)
        {
            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Email == email);
            }
            if (user == null) return null;

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
                    EndDate = DateTime.Now.AddMinutes(10),
                });

                debil.SaveChanges();
            }
            return value;
        }

        //public List<UToken> GetTokenListAction()
        //{
        //    using (var debil = new UserContext())
        //    {
        //        var guidContext = debil.Tokens.ToList();
        //        return guidContext;
        //    }

        //}

        //public UToken GetTokenAction(string token)
        //{
        //    UToken guidContext;
        //    using (var debil = new UserContext())
        //    {
        //        guidContext = debil.Tokens.Where(u => u.Value == token).SingleOrDefault();
        //    }
        //    return guidContext;
        //}
    }
}
