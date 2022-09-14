using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl.GameEvents
{
    internal class AllGoodEvent : GameEvent
    {
        public AllGoodEvent()
        {
            Statuscode = 0;
            Message = "All good, lets keep playing";
        }
    }
}
