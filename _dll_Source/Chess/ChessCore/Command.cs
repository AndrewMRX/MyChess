using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    public class Command
    {
        public string player;
        public string value;
        public int[] intvalues;

        public Command(string player, string value)
        {
            this.player = player;
            this.value = value;
            this.intvalues = new int[4] { 0, 0, 0, 0 };
        }

        public Command(Command cmd)
        {
            this.player = cmd.player;
            this.value = cmd.value;
            this.intvalues = new int[4] { cmd.intvalues[0], cmd.intvalues[1], cmd.intvalues[2], cmd.intvalues[3] };
        }

        public static string ToChessCoords(string cmd)
        {
            StringBuilder sb = new StringBuilder(cmd);
            sb[0] = Convert.ToChar(sb[0] + 49);
            sb[1] = Convert.ToChar('0' + ('8' - sb[1]));
            sb[2] = Convert.ToChar(sb[2] + 49);
            sb[3] = Convert.ToChar('0' + ('8' - sb[3]));
            cmd = sb.ToString();
            return cmd;
        }

        public static string ToSystemCoords(string cmd)
        {
            StringBuilder sb = new StringBuilder(cmd);
            sb[0] = Convert.ToChar(sb[0] - 49);
            sb[1] = Convert.ToChar('0' + ('8' - sb[1]));
            sb[2] = Convert.ToChar(sb[2] - 49);
            sb[3] = Convert.ToChar('0' + ('8' - sb[3]));
            cmd = sb.ToString();
            return cmd;
        }

        public static Command FillIntCmd(Command cmd)
        {
            cmd.intvalues[0] = Convert.ToInt32(cmd.value[0] - '0');
            cmd.intvalues[1] = Convert.ToInt32(cmd.value[1] - '0');
            cmd.intvalues[2] = Convert.ToInt32(cmd.value[2] - '0');
            cmd.intvalues[3] = Convert.ToInt32(cmd.value[3] - '0');
            return cmd;
        }
    }
}
