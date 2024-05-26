using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

using Spartacus.Web.Models;
namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        List<CatTable> GetCats();
        CatTable GetCatById(int id);
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string userEmail, string url);
        string CreateToken(string email);
        bool IsResetTokenValid(string value);
        bool ResetPasswordByToken(string value, string newPassword);
        string PopulateBody(string title, string url, string message);
        void CreateToken(UToken guid);
        List<UToken> GetTokenList();
        UToken GetToken(string token);
        void UploadFile(UFile File);
        bool CheckFilePath(string Filepath);
    }
}
