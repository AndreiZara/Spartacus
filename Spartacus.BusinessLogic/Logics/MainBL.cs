using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.Trainer;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Logics
{
    public class MainBL : MainApi, IMain
    {
        public Task SendEmailAsync(string recipientEmail, string body, string subject) => SendEmailAsyncAction(recipientEmail, body, subject);

        public string PopulateBody(string userEmail, string url, string templatePath) => PopulateBodyAction(userEmail, url, templatePath);

        public string CreateToken<TEntity>(string email, int minutes) where TEntity : class, IToken, new() => CreateTokenAction<TEntity>(email, minutes);

        public bool IsResetTokenValid(string value) => IsResetTokenValidAction(value);

        public bool ResetPasswordByToken(string value, string newPassword) => ResetPasswordByTokenAction(value, newPassword);

        public void SendFeedback(FeedData data) => SendFeedbackAction(data);

        public List<TrainerData> GetTrainers() => GetTrainersAction();

        public bool ConfirmRegisterToken(string token) => ConfirmRegisterTokenAction(token);
    }
}
