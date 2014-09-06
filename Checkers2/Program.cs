using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1 = red
//2 = black

namespace Checkers2 {

    public class Board {

        public List<Piece> P { get; set; }

        public Board() {
            P = new List<Piece>();

            for (int i = 0; i < 4; i++) {
                P.Add((new Piece(1, 2 * i, 0)));
                P.Add(new Piece(1, 2 * i + 1, 1));
                P.Add(new Piece(1, 2 * i, 2));
            }

            //black pieces
            for (int i = 0; i < 4; i++) {
                P.Add(new Piece(2, 2 * i + 1, 7));
                P.Add(new Piece(2, 2 * i, 6));
                P.Add(new Piece(2, 2 * i + 1, 5));
            }
        }

        public int checkPosition(int x, int y) {

            foreach (Piece p in P) {
                if (p.xpos == x && p.ypos == y && p.team == 1)
                    return 1;
                if (p.xpos == x && p.ypos == y && p.team == 2)
                    return 2;
            }

            return 0;
        }
        public void printBoard() {

            string retVal = "";
            retVal += "   _________________\n";

            for (int i = 0; i < 8; i++) { //y
                for (int j = 0; j < 8; j++) { //x


                    if (j == 0)
                        retVal += i + " | "; 

                    if (checkPosition(j, i) == 1)
                        retVal += "X ";
                    else if (checkPosition(j, i) == 2)
                        retVal += "O ";
                    else
                        retVal += "- ";

                    if (j == 7)
                        retVal += "|\n";
                }
            }

            retVal += "  |_________________|\n\n    0 1 2 3 4 5 6 7\n";
            Console.WriteLine(retVal);
        }

        public List<string> checkValidMoves(Piece p, int turn) {

            List<string> retVal = new List<string>();
            int x = p.xpos;
            int y = p.ypos;
            int player = checkPosition(x, y);

            if (player == 1 && turn == 1) { //X's piece, move is up (toward y = 7)
                if (x != 7 && y != 7)
                    if (checkPosition(x + 1, y + 1) == 0)
                        retVal.Add("(" + x + "," + y + ") to (" + (x + 1) + "," + (y + 1) + ")\n");

                if (x != 0 && y != 7)
                    if (checkPosition(x - 1, y + 1) == 0)
                        retVal.Add("(" + x + "," + y + ") to (" + (x - 1) + "," + (y + 1) + ")\n");
            }

            if (player == 2 && turn == 2) { //O's piece, move is up (toward y = 7)
                if (x != 0 && y != 0)
                    if (checkPosition(x - 1, y - 1) == 0)
                        retVal.Add("(" + x + "," + y + ") to (" + (x - 1) + "," + (y - 1) + ")\n");

                if (x != 7 && y != 0)
                    if (checkPosition(x + 1, y - 1) == 0)
                        retVal.Add("(" + x + "," + y + ") to (" + (x + 1) + "," + (y - 1) + ")\n");
            }
            return retVal;
        }
    }
    
    public class Move {
        public int xi { get; set; }
        public int yi { get; set; }
        public int xn { get; set; }
        public int yn { get; set; }
    }
    
    public class Piece {

        internal int team { get; set; }
        internal int xpos { get; set; }
        internal int ypos { get; set; }

        public Piece(int t, int x, int y) {
            team = t;
            xpos = x;
            ypos = y;
        }
    }

    class Program {

        static void Main(string[] args) {
            Console.WriteLine(@"/  __ \ |             | |                
| /  \/ |__   ___  ___| | _____ _ __ ___ 
| |   | '_ \ / _ \/ __| |/ / _ \ '__/ __|
| \__/\ | | |  __/ (__|   <  __/ |  \__ \
 \____/_| |_|\___|\___|_|\_\___|_|  |___/" + "\n\n");
            Board b = new Board();
            startGame(b);
        }

        public static void startGame(Board b) {

            List<string> moves = new List<string>();
            
            b.printBoard();
            
           // while (b.P1.Any() && b.P2.Any()) {
                
                foreach (Piece p in b.P)
                    moves.AddRange(b.checkValidMoves(p, 2));

                int c = 1;
                foreach (string s in moves) {
                    Console.Write(c + ": " + s);
                    c++;
                }
                
                Console.Read();
            //}
        }
    }
}