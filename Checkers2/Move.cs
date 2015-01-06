using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers3 {
    
    public class Move {
        
        public int num { get; set; }
        public int xi { get; set; }
        public int yi { get; set; }
        public int xn { get; set; }
        public int yn { get; set; }

        public Move(int a, int b, int c, int d, int num) {
            xi = a;
            yi = b;
            xn = c;
            yn = d;
            num = 0;
        }
    }
}

