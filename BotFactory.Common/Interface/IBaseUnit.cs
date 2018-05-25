using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;

namespace BotFactory.Common.Interface
{
    public interface IBaseUnit : IReportingUnit
    {
        string Name { get; }

        double Speed { get; }

        Coordinates CurrentPos { get; }

        Vector Vector { get; }

        double Move(Coordinates wishPos);
    }
}
