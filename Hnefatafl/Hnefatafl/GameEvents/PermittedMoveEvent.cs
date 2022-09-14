using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl.GameEvents
{
    public class PermittedMoveEvent : GameEvent
    {
        public PermittedMoveEvent()
        {
            Statuscode = 2;
            Message = "The tried move is permitted and wasnt made";
        }
    }
}
