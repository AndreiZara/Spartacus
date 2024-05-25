using System.Security.Cryptography;
using System.Text;

namespace Spartacus.Helpers
{
    public class LoginHelpers
    {
        public static string HashGen(string password)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password + "praWebUTM2");
            var encodedBytes = sha256.ComputeHash(originalBytes);

            return Encoding.Default.GetString(encodedBytes).ToLower();
        }
    }
}
