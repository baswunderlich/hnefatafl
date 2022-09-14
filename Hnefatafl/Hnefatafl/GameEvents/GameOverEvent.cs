using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl.GameEvents
{
    //Wird genutzt um das Ende des Spiels anzuzeigen. Der Sieger steht daraufhin in der Message
    public class GameOverEvent : GameEvent
    {
        //If AttackersWon == false && Tie == false: This means the defenders won
        public GameOverEvent(bool AttackersWon, bool Tie)
        {
            Statuscode = 1;
            if (Tie)
            {
                Message = "tie";
            }
            else if (AttackersWon)
            {
                Message = "the attackers won";
            }
            else
            {
                Message = "the defenders won";
            }
        }
    }
}
