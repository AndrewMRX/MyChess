using System.Data.SqlTypes;
using ChessEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private ChessContext context;
        public static Queue<string> FreeNames = new();
        public static Dictionary<string, string> Opponents = new();
        public static Dictionary<string, string> Colors = new();
        public BattleController(ChessContext _context)
        {
            context = _context;
        }

        [Route("api/[controller]/SendMyMove")]
        [HttpGet]
        public string SendMyMove(string name, string move)
        {
            Container.MainContainer.Add(name, move);
            return "Succes";
        }

        [Route("api/[controller]/CurrentMove")]
        [HttpGet]
        public string CurrentMove(string name)
        {
            if (Container.MainContainer.ContainsKey(name))
            {
                string move = Container.MainContainer[name];
                Container.MainContainer.Remove(name);
                return move;
            }
            else return "a1a1";

            //foreach(var el in Container.MainContainer)
            //{
            //    if (el.Name == name)
            //    {
            //        return el.Move;
            //    }
            //}

            //return "a1a1";

        }

        [Route("api/[controller]/EnemyName")]
        [HttpGet]
        public string EnemyName(string MyName)
        {
            if (Opponents.ContainsKey(MyName))
            {
                return Opponents[MyName]; // Возвратить имя соперника из словаря.
            }
            else return "";
        }

        [Route("api/[controller]/MyColor")]
        [HttpGet]
        public string MyColor(string MyName)
        {
            if (Colors.ContainsKey(MyName))
            {
                return Colors[MyName]; // Возвратить имя соперника из словаря.
            }
            else return "";
        }

        [Route("api/[controller]/AddRedyToPlay")]
        [HttpGet]
        public void AddRedyToPlay(string name)
        {
            FreeNames.Enqueue(name);
            if (FreeNames.Count > 1)
            {
                var player1 = FreeNames.Dequeue();
                var player2 = FreeNames.Dequeue();
                Opponents.Add(player1, player2);
                Opponents.Add(player2, player1);
                Colors.Add(player1, "white");
                Colors.Add(player2, "black");
            }
        }
    }
}
