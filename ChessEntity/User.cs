using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessEntity
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get;set; }
        public string Password { get;set; }
        public string Salt { get;set; }

        public virtual ICollection<Game>? WhiteGames { get; set; }
        public virtual ICollection<Game>? BlackGames { get; set; }

    }
}
