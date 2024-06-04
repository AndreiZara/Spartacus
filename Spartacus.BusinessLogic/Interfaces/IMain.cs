using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string title, string url, string message);
        void CreateToken(ResetToken guid);
        List<ResetToken> GetTokenList();
        ResetToken GetToken(string token);
        void UploadFile(UFile File);
        bool CheckFilePath(string Filepath);
        bool DeleteFile(UFile file);
        string PopulateBodyFeedback(FBTable model);
    }
}
