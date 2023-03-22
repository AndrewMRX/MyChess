using ChessEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ChessContext context;
        public UserController(ChessContext _context)
        {
            context = _context;
        }

        [Route("api/[controller]/Register")]
        [HttpPost]
        public string Register(string username, string password)
        {
            string[] saltHashPair = PasswordHasher.CreateHash(password);
            Console.WriteLine("Genered salt: " + saltHashPair[0]);
            Console.WriteLine("Hashed pass: " + saltHashPair[1]);

            var UsersInDb = from u in context.Users
                            where u.UserName.ToLower() == username
                            select u;

            if (UsersInDb.Count() != 0) 
            {
                return ("fail");
            }

            User newUser = new User { UserName = username, Password = saltHashPair[1], Salt = saltHashPair[0] };

            context.Users.Add(newUser);
            context.SaveChanges();

            return ("success");
        }

        [Route("api/[controller]/Login")]
        [HttpPost]
        public string Login(string username, string password)
        {
            string[] saltHashPair = PasswordHasher.CreateHash(password);
            Console.WriteLine("Genered salt: " + saltHashPair[0]);
            Console.WriteLine("Hashed pass: " + saltHashPair[1]);

            var UsersInDb = from u in context.Users
                            where u.UserName.ToLower() == username
                            select u;

            if (UsersInDb.Count() == 0)
            {
                return ("fail");
            }
            else
            {
                foreach (var u in UsersInDb)
                {
                    if (u.Password == saltHashPair[1])
                    {
                        return ("success");
                    }
                }
            }

            return ("fail");
        }
    }
}
