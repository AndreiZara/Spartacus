using System;

namespace Spartacus.Domain.Entities.Tokens
{
    public interface IToken
    {
        string Value { get; set; }
        string Email { get; set; }
        DateTime EndDate { get; set; }
    }
}
