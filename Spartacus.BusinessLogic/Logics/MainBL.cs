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
        //public CatTable GetCatByHash(byte[] hash) => GetCatByHashAction(hash);

        public CatTable GetCatById(int id) => GetCatByIdAction(id);

        public List<CatTable> GetCats() => GetCatsAction();
        public Task SendEmailAsync(string recipientEmail, string body, string subject) => SendEmailAsyncAction(recipientEmail, body, subject);

        public string PopulateBody(string title, string url, string message) => PopulateBodyAction(title, url, message);

        public string CreateToken(string email) => CreateTokenAction(email);

        //public List<UToken> GetTokenList() => GetTokenListAction();

        //public UToken GetToken(string token) => GetTokenAction(token);
    }
}
