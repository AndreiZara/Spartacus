using Grpc.Core;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System;
using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Core
{
    public class MainApi
    {
        public List<CatTable> GetCatsAction()
      public async Task SendEmailAsyncAction(string email, string subject, string message)
      {
            List<CatTable> cats;
            using (var debil = new CategoryContext())
        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        using (MailMessage mm = new MailMessage(smtpSection.From, email))
        {
                cats = debil.Categories.ToList();
            mm.Subject = subject;
            mm.Body = message;
            mm.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
            }
            return cats;
        var client = new SmtpClient("smtp.gmail.com", 587 )
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("cebotavictor14@gmail.com", "vqts afun ndcb facz")
        };

        await client.SendMailAsync(
            new MailMessage(from: "cebotavictor14@gmail.com",
            to: email,
            subject,
            message)
            { IsBodyHtml = true }) ;
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

        public void CreateTokenAction(UToken guid)
        {
            using (var debil = new UserContext())
            {
                debil.Tokens.Add(guid);
                debil.SaveChanges();
            }
        }

        public List<UToken> GetTokenListAction()
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.Tokens.ToList();
                return guidContext;
            }
            
        }
        //public CatTable GetCatByHashAction(byte[] hash)
        //{
        //    CatTable cat;
        //    using (var debil = new CategoryContext())
        //    using (var service = new SHA256CryptoServiceProvider())
        //    {
        //        cat = debil.Categories.FirstOrDefault(c => service.ComputeHash(Encoding.UTF8.GetBytes(c.Title)) == hash);
        //    }
        //    return cat;
        //}
        
        public CatTable GetCatByIdAction(int id)
        public UToken GetTokenAction(string token)
        {
            CatTable cat;
            using (var debil = new CategoryContext())
            using (var debil = new UserContext())
            {
                cat = debil.Categories.FirstOrDefault(c => c.Id == id);
                var guidContext = debil.Tokens.Where(u => u.Token == token).SingleOrDefault();
                return guidContext;
            }
            return cat;
        }

    }
}
