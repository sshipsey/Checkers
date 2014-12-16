using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers2 {
    public class Board {

        public List<Piece> P { get; set; }

        public Board() {
            P = new List<Piece>();

            //red pieces
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
            int t;
            //returns team of piece at x, y position if x, y is valid position
            if ((0 <= x && x <= 7) && (0 <= y && y <= 7)) {
                t =
                    (from piece in P
                     where piece.xpos == x
                     && piece.ypos == y
                     select piece.team).SingleOrDefault();
            }
            else {
                t = 3;
            }
            return t;
        }

        public void printBoard() {

            //prints board to screen
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

        public string printMoves(List<Move> moves) {

            //prints list of moves passed into function
            string retVal = "";
            int i = 1;
            foreach (Move move in moves) {
                retVal += "(" + i + ")  (" + move.xi + ", " + move.yi + ") to (" + move.xn + ", " + move.yn + ")\n";
                i++;
            }
            for (int j = 0; j < 10 - moves.Count; j++) {
                retVal += "\n";
            }
            return retVal;

        }
        
        public List<Move> checkJumps(int player) {

            List<Move> moves = new List<Move>();
            IEnumerable<Piece> p;
            if (player == 1) {
                p =
                    (from piece in P
                     where checkPosition(piece.xpos + 1, piece.ypos + 1) == 2
                     && checkPosition(piece.xpos + 2, piece.ypos + 2) == 0
                     && piece.team == 1
                     select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos + 2, pi.ypos + 2, 1));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xpos - 1, piece.ypos + 1) == 2
                     && checkPosition(piece.xpos - 2, piece.ypos + 2) == 0
                     && piece.team == 1
                     select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos - 2, pi.ypos + 2, 1));
                }
            }
            else {
                p =
                   (from piece in P
                    where checkPosition(piece.xpos + 1, piece.ypos - 1) == 1
                    && checkPosition(piece.xpos + 2, piece.ypos - 2) == 0
                    && piece.team == 2
                    select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos + 2, pi.ypos - 2, 2));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xpos - 1, piece.ypos - 1) == 1
                     && checkPosition(piece.xpos - 2, piece.ypos - 2) == 0
                     && piece.team == 2
                     select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos - 2, pi.ypos - 2, 2));
                }

            }

            moves = moves.OrderBy(x => x.xi).ToList();
            return moves;

        }
        public List<Move> checkValidMoves(int player) {

            //finds and returns a list of valid moves for player 'player'
            List<Move> moves = new List<Move>();

            IEnumerable<Piece> p;
            if (player == 1) {
                p =
                    (from piece in P
                     where checkPosition(piece.xpos + 1, piece.ypos + 1) == 0
                     && piece.team == 1
                     select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos + 1, pi.ypos + 1, 1));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xpos - 1, piece.ypos + 1) == 0
                     && piece.team == 1
                     select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos - 1, pi.ypos + 1, 1));
                }
            }
            else {
                p =
                   (from piece in P
                    where checkPosition(piece.xpos + 1, piece.ypos - 1) == 0
                    && piece.team == 2
                    select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos + 1, pi.ypos - 1, 2));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xpos - 1, piece.ypos - 1) == 0
                     && piece.team  == 2
                     select piece);
                if (p != null) {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xpos, pi.ypos, pi.xpos - 1, pi.ypos - 1, 2));
                }

            }
            moves = moves.OrderBy(x => x.xi).ToList();
            return moves;
        }

        public void move(Move m) {
            var p =
                (from piece in P
                 where piece.xpos == m.xi
                 && piece.ypos == m.yi
                 select piece).Single();

            p.xpos = m.xn;
            p.ypos = m.yn;

        }
        public void removePiece(int x, int y) {
            Piece p = new Piece(0, -1, -1); //invalid piece

            //returns piece at x, y position if x, y is valid position
            if ((0 <= x && x <= 7) && (0 <= y && y <= 7)) {
                p =
                    (from piece in P
                     where piece.xpos == x
                     && piece.ypos == y
                     select piece).SingleOrDefault();
            }
            this.P.Remove(p);
        }
    }
}
