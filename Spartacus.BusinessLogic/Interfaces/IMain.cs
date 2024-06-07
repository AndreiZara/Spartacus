using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.Trainer;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        Task SendEmailAsync(string email, string subject, string message);
        string PopulateBody(string userEmail, string url, string templatePath);
        string CreateToken<TEntity>(string email, int minutes) where TEntity : class, IToken, new();
        bool IsResetTokenValid(string value);
        bool ResetPasswordByToken(string value, string newPassword);
        void SendFeedback(FeedData data);
        List<TrainerData> GetTrainers();
        bool ConfirmRegisterToken(string token);
    }
}
