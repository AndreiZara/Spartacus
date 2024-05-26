using Newtonsoft.Json.Linq;
using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Logics
{
    public class MainBL : MainApi, IMain
    {
        public CatTable GetCatById(int id) => GetCatByIdAction(id);

        public List<CatTable> GetCats() => GetCatsAction();
        public Task SendEmailAsync(string recipientEmail, string body, string subject) => SendEmailAsyncAction(recipientEmail, body, subject);

        public string PopulateBody(string userEmail, string url) => PopulateBodyAction(userEmail, url);

        public string CreateToken(string email) => CreateTokenAction(email);

        public bool IsResetTokenValid(string value) => IsResetTokenValidAction(value);
        public bool ResetPasswordByToken(string value, string newPassword) => ResetPasswordByTokenAction(value, newPassword);
    }
}
