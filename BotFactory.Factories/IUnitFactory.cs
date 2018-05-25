using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;
using BotFactory.Common.Tools;

namespace BotFactory.Factories
{
    public interface IUnitFactory
    {
        int QueueCapacity { get; }

        int StorageCapacity { get; }

        int QueueFreeSlots { get; }

        int StorageFreeSlots { get; }

        TimeSpan QueueTime { get; }

        List<IFactoryQueueElement> Queue { get; }

        List<ITestingUnit> Storage { get; }

        event EventHandler FactoryProgress;

        bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingPos, Coordinates workingPos);
    }
}
