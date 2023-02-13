using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CRUDTest.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [Route("Login")]
         public IActionResult Login()
        {
            static byte[] GenerateSalt()
            {
                using (var randomNumberGenerator = RandomNumberGenerator.Create())
                {
                    byte[] salt = new byte[16];
                    randomNumberGenerator.GetBytes(salt);
                    return salt;
                }
            }

            static byte[] HashPasswordWithSalt(string password, byte[] salt)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                    byte[] passwordAndSalt = new byte[passwordBytes.Length + salt.Length];
                    Array.Copy(passwordBytes, passwordAndSalt, passwordBytes.Length);
                    Array.Copy(salt, 0, passwordAndSalt, passwordBytes.Length, salt.Length);
                    return sha256.ComputeHash(passwordAndSalt);
                }
            }
            string username = "theerasak";
            string password = "1234";

            byte[] salt = GenerateSalt();
            byte[] passwordHash = HashPasswordWithSalt(password, salt);

            return Ok();
        }
    }
}
