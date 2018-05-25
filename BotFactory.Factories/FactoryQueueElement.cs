using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;

namespace BotFactory.Factories
{
    public class FactoryQueueElement : IFactoryQueueElement
    {
        public string Name { get; private set; }
        public Type Model { get; private set; }
        public Coordinates ParkingPos { get; private set; }
        public Coordinates WorkingPos { get; private set; }

        public FactoryQueueElement(string name, Type model, Coordinates parkingPos, Coordinates workingPos)
        {
            this.Name = name;
            this.Model = model;
            this.ParkingPos = parkingPos;
            this.WorkingPos = workingPos;
        }
    }
}
