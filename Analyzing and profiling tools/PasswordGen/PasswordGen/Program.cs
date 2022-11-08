﻿using System.Diagnostics;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        var wt = new Stopwatch();
        byte[] salt = new byte[16];
        wt.Start();
        var pass1 = GeneratePasswordHashUsingSaltNotOptimized("password", salt);
        Console.WriteLine($"password generated: {pass1}");
        Console.WriteLine($"GeneratePasswordHashUsingSaltNotOptimized - Milliseconds: {wt.ElapsedMilliseconds}");

        wt.Restart();
        var pass2 = GeneratePasswordHashUsingSaltOptimized("password", salt);
        Console.WriteLine($"password generated: {pass2}");
        Console.WriteLine($"GeneratePasswordHashUsingSaltOptimized - Milliseconds: {wt.ElapsedMilliseconds}");
    }

    public static string GeneratePasswordHashUsingSaltNotOptimized(string passwordText, byte[] salt)
    {

        var iterate = 10000;
        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var passwordHash = Convert.ToBase64String(hashBytes);

        return passwordHash;

    }

    public static string GeneratePasswordHashUsingSaltOptimized(string passwordText, byte[] salt)
    {

        var iterate = 10000;
        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var passwordHash = Convert.ToBase64String(hashBytes);
        pbkdf2.Dispose();

        return passwordHash;

    }
}