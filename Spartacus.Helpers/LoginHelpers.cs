using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Helpers
{
    internal class LoginHelpers
    {
         public static string HashGen(string password)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password + "praWebUTM2");
            var encodedBytes = sha256.ComputeHash(originalBytes);

            return Encoding.Default.GetString(encodedBytes).ToLower();
        }
        public static void Main()
        {
            Console.WriteLine(HashGen("parolamea secreta"));

        }

    }
}
