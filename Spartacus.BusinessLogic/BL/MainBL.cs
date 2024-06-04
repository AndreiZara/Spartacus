using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Spartacus.BusinessLogic.BL
{
    public class MainBL:MainApi,IMain
    {
        public Task SendEmailAsync(string recipientEmail, string body, string subject)
        {
            return SendEmailAsyncAction(recipientEmail, body, subject);
        }

        public string PopulateBody(string title, string url, string message)
        {
            return PopulateBodyAction(title, url, message);
        }

        public string PopulateBodyFeedback(FBTable model)
        {
            return PopulateBodyFeedbackAction(model);
        }

        public void CreateToken(ResetToken guid)
        {
            CreateTokenAction(guid);    
        }

        public List<ResetToken> GetTokenList()
        {
            return GetTokenListAction();
        }

        public ResetToken GetToken(string token)
        {
            return GetTokenAction(token);
        }

        public void UploadFile(UFile File)
        {
            UploadFileAction(File);
        }
        public bool CheckFilePath(string Filepath)
        {
            return CheckFilePathAction(Filepath);
        }

        public bool DeleteFile(UFile file)
        {
            return DeleteFileAction(file);
        }

    }
}
