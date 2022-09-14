using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public class Move
    {
        //First element in array is x, second y: For example: [1,2] => x: 1, y: 2
        public Position FromField { get; }
        public Position ToField { get; }

        public Move(Position fromField, Position toField)
        {
            this.FromField = fromField;
            this.ToField = toField;
        }
    }
}
