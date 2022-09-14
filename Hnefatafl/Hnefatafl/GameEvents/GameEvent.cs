using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public abstract class GameEvent
    {
        protected int Statuscode = 0;
        protected string Message = "";
        public int GetStatuscode()
        {
            return Statuscode;
        }
        public string GetMessage()
        {
            return Message;
        }
    }
}
