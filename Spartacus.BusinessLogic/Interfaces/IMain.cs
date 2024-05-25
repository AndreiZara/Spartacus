using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        List<CatTable> GetCats();
        CatTable GetCatById(int id);
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string title, string url, string message);
        string CreateToken(string email);
        //List<UToken> GetTokenList();
        //UToken GetToken(string token);
    }
}
