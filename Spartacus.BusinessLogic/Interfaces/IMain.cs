using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        List<CatTable> GetCats();
        CatTable GetCatById(int id);
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string title, string url, string message);
        void CreateToken(UToken guid);
        List<UToken> GetTokenList();
        UToken GetToken(string token);
    }
}
