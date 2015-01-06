using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1 = red
//2 = black

namespace Checkers3 {

    class Program {

        static void Main(string[] args) {
            Console.WriteLine(@"/  __ \ |             | |                
| /  \/ |__   ___  ___| | _____ _ __ ___ 
| |   | '_ \ / _ \/ __| |/ / _ \ '__/ __|
| \__/\ | | |  __/ (__|   <  __/ |  \__ \
 \____/_| |_|\___|\___|_|\_\___|_|  |___/" + "\n\n");

            Game g = new Game();
            g.startGame();
        }
    }
}