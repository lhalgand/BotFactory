﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;

namespace BotFactory.Common.Interface
{
    public interface IWorkingUnit : IBaseUnit
    {
        Coordinates ParkingPos { get; set; }

        Coordinates WorkingPos { get; set; }

        bool IsWorking { get; }

        bool WorkBegins();

        bool WorkEnds();
    }
}
