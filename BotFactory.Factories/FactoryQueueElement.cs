using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;

namespace BotFactory.Factories
{
    public class FactoryQueueElement
    {
        public string Name;
        public Type Model;
        public Coordinates ParkingPos;
        public Coordinates WorkingPos;

        public FactoryQueueElement(string name, Type model, Coordinates parkingPos, Coordinates workingPos)
        {
            this.Name = name;
            this.Model = model;
            this.ParkingPos = parkingPos;
            this.WorkingPos = workingPos;
        }
    }
}
