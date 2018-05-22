using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;

namespace BotFactory.Common.Interface
{
    public interface IVector
    {
        double X { get; set; }
        double Y { get; set; }

        double Length(Coordinates begin, Coordinates end);
    }
}
