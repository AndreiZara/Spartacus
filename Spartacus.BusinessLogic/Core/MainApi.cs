using Grpc.Core;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
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
using System.Web;

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
            }
      }




        public string PopulateBodyAction(string title, string url, string message)  
    {
        var uploadFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/Template/Email.html");
            if (!Directory.Exists(uploadFile))
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
            return "";
    }

        public string PopulateBodyFeedbackAction(FBTable model)   
        {
            var uploadFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/Template/Feedback.html");
            if (!Directory.Exists(uploadFile))
            {
                string body = string.Empty;

                using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/Template/Feedback.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{AdminUsername}",model.AdminUsername);
                body = body.Replace("{Username}", model.Username);
                body = body.Replace("{Email}", model.Email);
                body = body.Replace("{Subject}", model.Subject);
                body = body.Replace("{Message}", model.Message);
                return body;
            }
            return "";
        }

        public void CreateTokenAction(ResetToken guid)
        {
            using (var debil = new UserContext())
            {
                debil.Tokens.Add(guid);
                debil.SaveChanges();
            }
        }

        public List<ResetToken> GetTokenListAction()
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.Tokens.ToList();
                return guidContext;
            }
            
        }
        
        public ResetToken GetTokenAction(string token)
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.Tokens.Where(u => u.Token == token).SingleOrDefault();
                return guidContext;
            }
        }

        public void UploadFileAction(UFile file)
        {
            HttpPostedFileBase File = file.FileModel;

            if (File != null && File.ContentLength > 0)
            {
                // Check if the file is an image
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(File.FileName).ToLower();



                if (allowedExtensions.Contains(extension))
                {
                    string filepath = "~/Content/Upload/" + file.Username;
                    var uploadsDir = HttpContext.Current.Server.MapPath(filepath);
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(uploadsDir, fileName);
                    File.SaveAs(path);
                }
            }
        }

        public bool DeleteFileAction(UFile file)
        {
            string filepath = "~/Content/Upload/" + file.Username;
            var uploadsDir = HttpContext.Current.Server.MapPath(filepath);
            if (!Directory.Exists(uploadsDir))
            {
                return false;
            }
            Directory.Delete(uploadsDir, true);
            return true;
        }

        public bool CheckFilePathAction(string Filepath)
        {

            var uploadsDir = HttpContext.Current.Server.MapPath(Filepath);
            if (!Directory.Exists(uploadsDir))
            {
                return false;
            }
            return true;

        }



    }
}
