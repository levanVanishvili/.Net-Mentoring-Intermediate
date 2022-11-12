using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var wt = new Stopwatch();
        byte[] salt = new byte[16];
        var iterate = 10000;    

        wt.Start();
        var pass1 = GeneratePasswordHashUsingSaltOptimized("password", salt, iterate);
        Console.WriteLine($"password generated: {pass1}");
        Console.WriteLine($"GeneratePasswordHashUsingSaltOptimized - Milliseconds: {wt.ElapsedMilliseconds}");
        wt.Stop();

        wt.Start();
        var pass2 = GeneratePasswordHashUsingSaltNotOptimized("password", salt, iterate);
        Console.WriteLine($"password generated: {pass2}");
        Console.WriteLine($"GeneratePasswordHashUsingSaltNotOptimized - Milliseconds: {wt.ElapsedMilliseconds}");
        wt.Stop();
    }

    public static string GeneratePasswordHashUsingSaltNotOptimized(string passwordText, byte[] salt, int iterate)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
        
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var passwordHash = Convert.ToBase64String(hashBytes);

        return passwordHash;

    }

    public static string GeneratePasswordHashUsingSaltOptimized(string passwordText, byte[] salt, int iterate)
    {
        var keyDerHash = KeyDerivation.Pbkdf2(passwordText, salt, KeyDerivationPrf.HMACSHA1, iterate, 20);
        
        byte[] hashBytes = new byte[36];
        salt.CopyTo(hashBytes, 0);
        keyDerHash.CopyTo(hashBytes, 16);

        var passwordHash = Convert.ToBase64String(hashBytes);

        return passwordHash;

    }
}
