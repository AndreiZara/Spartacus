using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string PagePath, string[] pageParameters, params string[] emailParameters);
        void CreateToken(ResetToken guid);
        ResetToken GetTokenByIdAction(int Id);
        List<ResetToken> GetTokenList();
        ResetToken GetToken(string token);
        bool DeleteToken(int Id);
        void UploadFile(UFile File);
        bool CheckFilePath(string Filepath);
        bool DeleteFile(UFile file);

        void CreateRegToken(RegisterToken guid);
        bool UpdateRegToken(RegisterToken user, int Id);
        RegisterToken GetRegTokenById(int Id);
        RegisterToken GetRegToken(string token);
        List<RegisterToken> GetRegTokenList();
        bool DeleteRegToken(int Id);

    }
}
