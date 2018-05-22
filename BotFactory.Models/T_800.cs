using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class T_800 : WorkingUnit
    {
        public T_800(string name, double speed = 10000)
            : base(name, speed)
        {
            base.BuildTime = 10000;
        }
    }
}
