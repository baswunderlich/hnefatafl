using Hnefatafl.Hnefatafl.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public class RulesCoopenhagen : IHnefataflRules
    {
        private Piece[,] Board;
        private int Boardheight { get; }
        private int Boardwidth { get; }
        private bool attackersMove = true;

        public RulesCoopenhagen()
        {
            this.Board = new Piece[11, 11];
            this.Boardheight = 11;
            this.Boardwidth = 11;
            Setup();
        }

        public Piece[,] GetBoard()
        {
            return Board;
        }

        public GameEventsObject PlayMove(Move move)
        {
            //Check whether a number is out of bounds
            var x1 = 0;
            var x2 = 0;
            var y1 = 0;
            var y2 = 0;

            //Check if the FromField contains a piece of the party whos turn it is
            if (attackersMove)
            {
                if(Board[move.FromField.X, move.FromField.Y] != Piece.Attacker)
                {
                    return new GameEventsObject(new PermittedMoveEvent());
                }
            }
            else
            {
                if (Board[move.FromField.X, move.FromField.Y] != Piece.Defender && Board[move.FromField.X, move.FromField.Y] != Piece.King)
                {
                    return new GameEventsObject(new PermittedMoveEvent());
                }
            }

            try
            {
                //Covnert the given move to coordinates
                x1 = int.Parse(""+move.FromField.X);
                x2 = int.Parse(""+move.ToField.X);
                y1 = int.Parse("" + move.FromField.Y);
                y2 = int.Parse("" + move.ToField.Y);
            }
            catch (Exception)
            {
                return new GameEventsObject(new PermittedMoveEvent());
            }

            //Check for out of bounds
            if (!(x1 > -1 && x1 < 11 && x2 > -1 && x2 < 11 &&
                y1 > -1 && y1 < 11 && y2 > -1 && y2 < 11) || (x1 == x2 && y1 == y2)) {
                return new GameEventsObject(new PermittedMoveEvent());
            }
            //Check if the way is a line and not diagonal of some kind
            if(!(x1 == x2 || y1 == y2))
            {
                return new GameEventsObject(new PermittedMoveEvent());
            }
            //Check if the way is free
            if (!WayIsFree(x1, x2, y1, y2))
            {
                return new GameEventsObject(new PermittedMoveEvent());
            }
            //Save the moved piece
            var save = GetPieceOnPosition(move.FromField);

            //Check for victory
            if (save.Equals(Piece.King) && GetPieceOnPosition(move.ToField).Equals(Piece.Escape))
            {
                Board[move.FromField.X, move.FromField.Y] = Piece.Empty;
                Board[move.ToField.X, move.ToField.Y] = save;
                return new GameEventsObject(new GameOverEvent(false, false));
            }

            //Check for capturing
            CapturePieces(move);

            Board[move.FromField.X, move.FromField.Y] = Piece.Empty;
            Board[move.ToField.X, move.ToField.Y] = save;

            //Reset the throne if the king left it
            if(Board[5,5] == Piece.Empty)
            {
                Board[5, 5] = Piece.Throne;
            }

            //Return an empty GameEventsObject when everything checked out
            return new GameEventsObject();
        }

        public int GetBoardWidth()
        {
            return this.Boardwidth;
        }

        public int GetBoardHeight()
        {
            return this.Boardheight;
        }
        
        private void Setup()
        {
            //Throne and escape fields
            Board[0, 0] = Piece.Escape;
            Board[0, 10] = Piece.Escape;
            Board[10, 0] = Piece.Escape;
            Board[10, 10] = Piece.Escape;
            Board[5, 5] = Piece.Throne;

            //Testpiece
            Board[0, 3] = Piece.Attacker;
            Board[3, 2] = Piece.Defender;
            Board[3, 4] = Piece.Defender;
            Board[3, 5] = Piece.Attacker;
        }

        private Piece GetPieceOnPosition(Position p)
        {
            var x1 = int.Parse("" + p.X);
            var y1 = int.Parse("" + p.Y);
            if (!(x1 > -1 && x1 < 11 && y1 > -1 && y1 < 11))
            {
                throw new IndexOutOfBoardException();
            }
            return Board[p.X, p.Y];
        }

        private bool WayIsFree(int x1, int x2, int y1, int y2)
        {
            //Check for no movement
            if(x1 == x2 && y1 == y2)
            {
                return true;
            }

            if(x1 == x2)
            {
                if(y1 < y2)
                {
                    for(int i = y1+1; i <= y2; i++)
                    {
                        if(Board[x1, i] != Piece.Empty)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = y1 - 1; i >= y2; i--)
                    {
                        if (Board[x1, i] != Piece.Empty)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if(x1 < x2)
                {
                    for (int i = x1 + 1; i <= x2; i++)
                    {
                        if (Board[i, y1] != Piece.Empty)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = x1 - 1; i >= x2; i--)
                    {
                        if (Board[i, y1] != Piece.Empty)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void CapturePieces(Move move)
        {
            if (attackersMove)
            {
                try
                {
                    if (Board[move.ToField.X, move.ToField.Y + 1] == Piece.Defender && Board[move.ToField.X, move.ToField.Y + 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X, move.ToField.Y + 1] = Piece.Empty;
                    }
                    if (Board[move.ToField.X, move.ToField.Y + 1] == Piece.King && IsSurrounded(move.ToField))
                    {
                        Board[move.ToField.X, move.ToField.Y + 1] = Piece.Empty;
                    }
                }
                catch (Exception) { }
                try
                {
                    if (Board[move.ToField.X, move.ToField.Y - 1] == Piece.Defender && Board[move.ToField.X, move.ToField.Y - 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X, move.ToField.Y - 1] = Piece.Empty;
                    }
                    if (Board[move.ToField.X, move.ToField.Y - 1] == Piece.King && IsSurrounded(move.ToField))
                    {
                        Board[move.ToField.X, move.ToField.Y - 1] = Piece.Empty;
                    }
                }
                catch (Exception) { }
                try
                {
                    if (Board[move.ToField.X + 1, move.ToField.Y] == Piece.Defender && Board[move.ToField.X, move.ToField.X + 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X + 1, move.ToField.Y] = Piece.Empty;
                    }
                    if (Board[move.ToField.X + 1, move.ToField.Y] == Piece.King && IsSurrounded(move.ToField))
                    {
                        Board[move.ToField.X + 1, move.ToField.Y] = Piece.Empty;
                    }
                }
                catch (Exception) { }
                try
                {
                    if (Board[move.ToField.X - 1, move.ToField.Y] == Piece.Defender && Board[move.ToField.X, move.ToField.X - 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X - 1, move.ToField.Y] = Piece.Empty;
                    }
                    if (Board[move.ToField.X - 1, move.ToField.Y] == Piece.King && IsSurrounded(move.ToField))
                    {
                        Board[move.ToField.X - 1, move.ToField.Y] = Piece.Empty;
                    }
                }
                catch (Exception) { }
            }
            else
            {
                if (move.ToField.Y + 2! > 10)
                {
                    if (Board[move.ToField.X, move.ToField.Y + 1] == Piece.Defender && Board[move.ToField.X, move.ToField.Y + 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X, move.ToField.Y + 1] = Piece.Empty;
                    }
                }
                if (move.ToField.Y - 2! < 0)
                {
                    if (Board[move.ToField.X, move.ToField.Y - 1] == Piece.Defender && Board[move.ToField.X, move.ToField.Y - 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X, move.ToField.Y - 1] = Piece.Empty;
                    }
                }
                if (move.ToField.X + 2! > 10)
                {
                    if (Board[move.ToField.X + 1, move.ToField.Y] == Piece.Defender && Board[move.ToField.X, move.ToField.X + 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X + 1, move.ToField.Y] = Piece.Empty;
                    }
                }
                if (move.ToField.X - 2! < 0)
                {
                    if (Board[move.ToField.X - 1, move.ToField.Y] == Piece.Defender && Board[move.ToField.X, move.ToField.X - 2] == Piece.Attacker)
                    {
                        Board[move.ToField.X - 1, move.ToField.Y] = Piece.Empty;
                    }
                }
            }
        }

        //Checks if the Position is surrounded by attackers
        private bool IsSurrounded(Position pos)
        {
            if (Board[pos.X + 1, pos.Y] == Piece.Attacker
                && Board[pos.X - 1, pos.Y] == Piece.Attacker
                && Board[pos.X, pos.Y + 1] == Piece.Attacker
                && Board[pos.X, pos.Y - 1] == Piece.Attacker)
            {
                return true;
            }
            return false;
        }
    }
}
