using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers2 {
    public class Game {
        
        private bool isLive;
        private int turn;
        
        public Game() {
            this.isLive = true;
            this.turn = 2;
        }

        public void startGame() {
            
            string input;
            int x;
            Board b = new Board();
            List<Move> moves = new List<Move>();
            
            while (this.isLive) {
                b.printBoard();

                moves = b.checkJumps(this.turn);
                
                if (moves.Count == 0) {
                    moves = b.checkValidMoves(this.turn);
                }
                Console.WriteLine(b.printMoves(moves));
                input = Console.ReadLine();
                while (true) {
                    if (!int.TryParse(input, out x)) {
                        Console.WriteLine("Please enter a valid move");
                        input = Console.ReadLine();
                    }
                    if (x <= moves.Count && x > 0) {
                        break;
                    }
                    else {
                        Console.WriteLine("Please enter a valid move");
                        input = Console.ReadLine();
                    }
                }
                Move m = moves[x-1];
                b.move(m);
                if (Math.Abs(m.xn - m.xi) > 1) { //this was a jump
                    b.removePiece((m.xn + m.xi)/2, (m.yn + m.yi)/2);
                }
                //exchange turn
                if (this.turn == 1) {
                    this.turn = 2;
                } else {
                    this.turn = 1;
                }

            }
        }
    }
}
