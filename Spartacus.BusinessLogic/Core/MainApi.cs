﻿using Grpc.Core;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System;
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
      public async Task SendEmailAsyncAction(string email, string subject, string message)
      {
        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        using (MailMessage mm = new MailMessage(smtpSection.From, email))
        {
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

        public void CreateTokenAction(GUID guid)
        {
            using (var debil = new UserContext())
            {
                debil.Tokens.Add(guid);
                debil.SaveChanges();
            }
        }

        public List<GUID> GetTokenAction()
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.Tokens.ToList();
                return guidContext;
            }
            
        }






    }
}
