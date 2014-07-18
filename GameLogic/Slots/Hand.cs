using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Slots
{
    public class Hand : Slot
    {
        public override string SlotType
        {
            get { return "Hand";  }
        }
    }
}
