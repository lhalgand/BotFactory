using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;
using BotFactory.Common.Interface;

namespace BotFactory.Models
{
    public abstract class BaseUnit : ReportingUnit, IBaseUnit
    {
        public string Name { get; private set; }
        
        public double Speed { get; private set; }
        
        public Coordinates CurrentPos { get; private set; }
        
        public Vector Vector { get; private set; }

        public BaseUnit(string name, double speed = 1)
        {
            this.Name = name;
            this.Speed = speed;

            this.CurrentPos = new Coordinates(0, 0);
            this.Vector = new Vector();
        }

        public double Move(Coordinates wishPos)
        {
            return this.Vector.Length(this.CurrentPos, wishPos) / this.Speed;
        }
    }
}
