using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;

namespace BotFactory.Factories
{
    public interface IFactoryQueueElement
    {
        string Name { get; }
        Type Model { get; }
        Coordinates ParkingPos { get; }
        Coordinates WorkingPos { get; }
    }
}
