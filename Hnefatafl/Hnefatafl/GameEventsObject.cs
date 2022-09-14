using Hnefatafl.Hnefatafl.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public class GameEventsObject
    {
        public GameEvent eve;

        public GameEventsObject()
        {
            eve = new AllGoodEvent();
        }

        public GameEventsObject(GameEvent eve)
        {
            this.eve = eve;
            if(eve != null)
                SetEvent(eve);
        }

        public void SetEvent(GameEvent eve)
        {
            this.eve = eve;
        }

        public GameEvent GetEvent()
        {
            return eve;
        }
    }
}
