﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
    public interface IReportingUnit
    {
        event EventHandler<StatusChangedEventArgs> UnitStatusChanged;

        void OnStatusChanged(StatusChangedEventArgs e);
    }
}
