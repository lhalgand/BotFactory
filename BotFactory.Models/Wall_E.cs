using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class Wall_E : WorkingUnit
    {
        public Wall_E(string name, double speed = 4000)
            : base(name, speed)
        {
            base.BuildTime = 4000;
        }
    }
}
