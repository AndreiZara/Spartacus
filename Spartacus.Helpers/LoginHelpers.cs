using System.Security.Cryptography;
using System.Text;

namespace Spartacus.Helpers
{
    public class LoginHelpers
    {
        public static string HashGen(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            var originalBytes = Encoding.Default.GetBytes(password + "praWebUTM2");
            var encodedBytes = sha256.ComputeHash(originalBytes);
            return Encoding.Default.GetString(encodedBytes).ToLower();
        }
    }
}
