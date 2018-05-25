using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;

namespace BotFactory.Models
{
    public class TestingUnit : WorkingUnit, ITestingUnit
    {
        //public Type Model { get; private set; }

        public TestingUnit(string name, double speed)
            : base(name, speed)
        {
            //
        }
    }
}
