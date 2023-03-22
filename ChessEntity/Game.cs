using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEntity
{
    public class Game
    {
        public int Id { get; set; }
        public int? PlayerIdWhite { get; set; }
        public int? PlayerIdBlack { get; set; }
        public string GameResult { get; set; }
        public TimeSpan Time { get; set; }

        public virtual User WhitePlayer { get; set; }
        public virtual User BlackPlayer { get; set; }
    }
}
