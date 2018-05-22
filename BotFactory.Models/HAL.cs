using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class HAL : WorkingUnit
    {
        public HAL(string name, double speed = 0.5f)
            : base(name, speed)
        {
            base.BuildTime = 7000;
        }
    }
}
