﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
    public interface IBuildableUnit
    {
        Type Model { get; set; }

        double BuildTime { get; set; }
    }
}
