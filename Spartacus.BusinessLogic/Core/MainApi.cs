using Grpc.Core;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.Tokens;
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


        public string PopulateBodyAction(string PagePath, string[] pageParameters,params string[] emailParameters)
        {
            var uploadFile = System.Web.HttpContext.Current.Server.MapPath(PagePath);
            if (!Directory.Exists(uploadFile))
            {
                string body = string.Empty;

                using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(PagePath)))
                {
                    body = reader.ReadToEnd();
                }
                for(int i = 0; i<pageParameters.Length; i++)
                {
                    body = body.Replace(pageParameters[i], emailParameters[i]);
                }
                return body;
            }
            return "";
        }


        public void CreateTokenAction(ResetToken guid)
        {
            using (var debil = new UserContext())
            {
                debil.ResTokens.Add(guid);
                debil.SaveChanges();
            }
        }

        public List<ResetToken> GetTokenListAction()
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.ResTokens.ToList();
                return guidContext;
            }
            
        }
        
        public ResetToken GetTokenAction(string token)
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.ResTokens.Where(u => u.Token == token).SingleOrDefault();
                return guidContext;
            }
        }

        public ResetToken GetTokenByIdAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.ResTokens.Where(u => u.Id == Id).SingleOrDefault();
                return guidContext;
            }
        }

        internal bool DeleteTokenAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.ResTokens.SingleOrDefault(u => u.Id == Id);
                if (user != null)
                {
                    debil.ResTokens.Remove(user);
                    debil.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void CreateRegTokenAction(RegisterToken guid)
        {
            using (var debil = new UserContext())
            {
                debil.RegTokens.Add(guid);
                debil.SaveChanges();
            }
        }

        internal bool UpdateRegTokenAction(RegisterToken user, int Id)
        {
            using (var debil = new UserContext())
            {
                var data = debil.RegTokens.FirstOrDefault(x => x.Id == Id);

                if (data != null)
                {
                    data.Email = user.Email;
                    data.Token = user.Token;
                    data.EndDate = user.EndDate;
                    data.Status = user.Status;
                    debil.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public RegisterToken GetRegTokenAction(string token)
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.RegTokens.Where(u => u.Token == token).SingleOrDefault();
                return guidContext;
            }
        }

        public RegisterToken GetRegTokenByIdAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.RegTokens.Where(u => u.Id == Id).SingleOrDefault();
                return guidContext;
            }
        }

        public List<RegisterToken> GetRegTokenListAction()
        {
            using (var debil = new UserContext())
            {
                var guidContext = debil.RegTokens.ToList();
                return guidContext;
            }
        }

        internal bool DeleteRegTokenAction(int Id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.RegTokens.FirstOrDefault(u => u.Id == Id);
                if (user != null)
                {
                    debil.RegTokens.Remove(user);
                    debil.SaveChanges();
                    return true;
                }
                return false;
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
