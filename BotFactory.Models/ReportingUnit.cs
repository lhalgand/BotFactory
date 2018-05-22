using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common;
using BotFactory.Common.Interface;

namespace BotFactory.Models
{
    public abstract class ReportingUnit : BuildableUnit, IReportingUnit
    {
        public event EventHandler<StatusChangedEventArgs> UnitStatusChanged;

        public void OnStatusChanged(StatusChangedEventArgs e)
        {
            if (this.UnitStatusChanged != null)
            { this.UnitStatusChanged(this, e); }
        }
    }
}
