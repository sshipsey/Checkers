using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers3 {
    public class Game {
        
        private bool isLive;
        private int turn;
        private bool lastMoveJump;
        private int otherTurn;

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

                if (this.turn == 1) {
                    this.otherTurn = 2;
                }
                else {
                    this.otherTurn = 1;
                }

                moves = b.checkJumps(this.turn);

                //Check if last move (jump) resulted in another available jump
                if (this.lastMoveJump && b.checkJumps(this.otherTurn).Count > 0) {
                    moves = b.checkJumps(this.otherTurn);
                    this.turn = this.otherTurn;
                    this.lastMoveJump = true;
                } else if (moves.Count == 0) {
                    moves = b.checkValidMoves(this.turn);
                    this.lastMoveJump = false;
                }
                else {
                    this.lastMoveJump = true;
                }

                if (moves.Count == 0) {
                    this.isLive = false;
                    continue;
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
                
                //promote king (works but piece cant move correctly after king)
                Piece p;
                if ((m.yn == 0 && this.turn == 2) || m.yn == 7 && this.turn == 1) {
                    
                    p =
                        (from piece in b.P
                         where piece.xpos == m.xn
                         && piece.ypos == m.yn
                         select piece).SingleOrDefault();
                    p.isKing = true;
                }

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
            Console.WriteLine("Team " + this.turn + " wins!");
            Console.Read();
        }
    }
}
