using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers3 {
    public class Piece {

        internal int team { get; set; }
        internal int xpos { get; set; }
        internal int ypos { get; set; }
        internal bool isKing { get; set; }

        public Piece(int t, int x, int y) {
            team = t;
            xpos = x;
            ypos = y;
        }

    }

}
