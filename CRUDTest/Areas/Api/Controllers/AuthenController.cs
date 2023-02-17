using CRUDTest.Areas.Api.Models;
using CRUDTest.ModelsDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly DB_Context db_context;
        public AuthenController(DB_Context context)
        {
            this.db_context = context;
        }

        [HttpPost]
        [Route("Register")]
         public IActionResult Register([FromBody] Req_Register req)
         {
            object Data = null;
            try
            {
                DateTime txTimeStamp = DateTime.Now;
                string username = req.username;
                var data_user = db_context.user_logins.Where(o => o.user_name == username).FirstOrDefault();
                if(data_user == null)
                {
                    string salt_key = GenerateSalt(20);
                    string passwordHash = HashPasswordWithSalt(req.password, salt_key);

                    user_login user_data = new user_login();
                    {
                        user_data.user_name = username.Trim();
                        user_data.salt_key = salt_key;
                        user_data.password = passwordHash;
                        user_data.created_time = txTimeStamp;
                    }
                    db_context.user_logins.Add(user_data);
                    db_context.SaveChanges();

                    Data = new
                    {
                        status = "success",
                        data = passwordHash
                };

                    static String GenerateSalt(int length)
                    {
                        Random res = new Random();
                        string charactors = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWWXYZ0123456789";
                        string salt = "";
                        for (var i = 0; i < length; i++)
                        {
                            int x = res.Next(charactors.Length);
                            salt = salt + charactors[x];
                        }
                        return salt;
                    }

                    //static byte[] HashPasswordWithSalt(string password, string salt)
                    //{
                    //    using (var sha256 = SHA256.Create())
                    //    {
                    //        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                    //        byte[] saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
                    //        byte[] passwordAndSalt = new byte[passwordBytes.Length + saltBytes.Length];
                    //        Array.Copy(passwordBytes, passwordAndSalt, passwordBytes.Length);
                    //        Array.Copy(saltBytes, 0, passwordAndSalt, passwordBytes.Length, salt.Length);
                    //        return sha256.ComputeHash(passwordAndSalt);
                    //    }
                    //}

                    static string HashPasswordWithSalt(string password, string salt)
                    {
                        using (var sha256 = SHA256.Create())
                        {
                            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                            return Convert.ToBase64String(hashBytes);
                        }
                    }
                }
                else
                {
                    Data = new
                    {
                        status = "fail",
                        data = "username use"
                    };
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            return Ok(Data);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Req_Register req)
        {
            object Data = null;
            try
            {
                DateTime txTimeStamp = DateTime.Now;
                string username = req.username;
                string password = req.password;
                var data_user = db_context.user_logins.Where(o => o.user_name == username).FirstOrDefault();
                if(data_user != null)
                {
                    string salt = data_user.salt_key;
                    byte[] passwordHash = HashPasswordWithSalt(password, salt);

                    if(data_user.password == BitConverter.ToString(passwordHash))
                    {
                        Data = new
                        {
                            status = "success",
                            data = "login success"
                        };
                    }
                    static byte[] HashPasswordWithSalt(string password, string salt)
                    {
                        using (var sha256 = SHA256.Create())
                        {
                            List<int> data = new List<int>();
                            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                            byte[] saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
                            byte[] passwordAndSalt = new byte[passwordBytes.Length + saltBytes.Length];
                            Array.Copy(passwordBytes, passwordAndSalt, passwordBytes.Length);
                            Array.Copy(saltBytes, 0, passwordAndSalt, passwordBytes.Length, salt.Length);
                            return sha256.ComputeHash(passwordAndSalt);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return Ok(Data);
        }
    }
}