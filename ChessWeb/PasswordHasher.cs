using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ChessWeb
{
    public static class PasswordHasher
    {
        public static string[] CreateHash(string password)
        {
            string[] saltHashPair = new string[2];
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            saltHashPair[0] = Convert.ToBase64String(salt);
           
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            saltHashPair[1] = hashed;
            return saltHashPair;
        }

        public static string[] CheckHash(string password, string saltstring)
        {
            string[] saltHashPair = new string[2];
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values

            byte[] salt = Convert.FromBase64String(saltstring);
            //using (var rngCsp = new RNGCryptoServiceProvider())
            //{
            //    rngCsp.GetNonZeroBytes(salt);
            //}
            ////Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            //saltHashPair[0] = Convert.ToBase64String(salt);
           
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            saltHashPair[1] = hashed;
            return saltHashPair;
        }



    }
}
