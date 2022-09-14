using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public interface IHnefataflRules
    {
        public GameEventsObject PlayMove(Move move);
        public Piece[,] GetBoard();
        public int GetBoardWidth();
        public int GetBoardHeight();
    }
}
