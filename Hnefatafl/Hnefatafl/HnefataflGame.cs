using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public class HnefataflGame
    {
        private IHnefataflRules RuleSet;

        public HnefataflGame(IHnefataflRules rules)
        {
            this.RuleSet = rules;
        }

        public GameEventsObject PlayMove(Move move)
        {
            //plays the move if possible else this method returns false
            return RuleSet.PlayMove(move);
        }

        public Piece[,] GetBoard()
        {
            return RuleSet.GetBoard();
        }

        public int GetBoardHeight()
        {
            return RuleSet.GetBoardHeight();
        }

        public int GetBoardWidth()
        {
            return RuleSet.GetBoardWidth();
        }
    }
}
