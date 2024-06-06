using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.Tokens;
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
        public Task SendEmailAsync(string recipientEmail, string body, string subject) => SendEmailAsyncAction(recipientEmail, body, subject);
        public string PopulateBody(string PagePath, string[] pageParameters, params string[] emailParameters) => PopulateBodyAction(PagePath, pageParameters, emailParameters);

        public void CreateToken(ResetToken guid) => CreateTokenAction(guid);    
        public List<ResetToken> GetTokenList() => GetTokenListAction();
        public ResetToken GetToken(string token) => GetTokenAction(token);
        public ResetToken GetTokenById(int Id) => GetTokenByIdAction(Id);
        public bool DeleteToken(int Id) => DeleteTokenAction(Id);

        public void UploadFile(UFile File) => UploadFileAction(File);
        public bool CheckFilePath(string Filepath) => CheckFilePathAction(Filepath);
        public bool DeleteFile(UFile file) => DeleteFileAction(file);

        public void CreateRegToken(RegisterToken guid) => CreateRegTokenAction(guid);
        public bool UpdateRegToken(RegisterToken user, int Id) => UpdateRegTokenAction(user, Id);
        public RegisterToken GetRegToken(string token) => GetRegTokenAction(token);    
        public RegisterToken GetRegTokenById(int Id) => GetRegTokenByIdAction(Id);
        public List<RegisterToken> GetRegTokenList() => GetRegTokenListAction();
        public bool DeleteRegToken(int Id) => DeleteRegTokenAction(Id);

    }
}
