using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string title, string url, string message);
        void CreateToken(UToken guid);
        List<UToken> GetTokenList();
        UToken GetToken(string token);
        void UploadFile(UFile File);
        bool CheckFilePath(string Filepath);
    }
}
