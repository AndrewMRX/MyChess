using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    public class Figure
    {
        public char name;
        public string color;
        public int posx;
        public int posy;
        public bool firstMove;

        public Figure(char name, string color, int posx, int posy)
        {
            this.name = name;
            this.color = color;
            this.posx = posx;
            this.posy = posy;
            this.firstMove = false;
        }

        public Figure(Figure copy)
        {
            this.name = copy.name;
            this.color = copy.color;
            this.posx = copy.posx;
            this.posy = copy.posy;
            this.firstMove = copy.firstMove;
        }
    }
}

