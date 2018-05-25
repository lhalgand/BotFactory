using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common;
using BotFactory.Common.Tools;
using BotFactory.Common.Interface;

namespace BotFactory.Models
{
    public abstract class WorkingUnit : BaseUnit, IWorkingUnit
    {
        public Coordinates ParkingPos { get; set; }
        
        public Coordinates WorkingPos { get; set; }
        
        public bool IsWorking { get; private set; }

        public WorkingUnit(string name, double speed)
            : base(name, speed)
        {
            this.ParkingPos = new Coordinates(0, 0);
            this.WorkingPos = new Coordinates(0, 0);
            this.IsWorking = false;
        }

        public virtual bool WorkBegins()
        {
            if (!base.CurrentPos.Equals(this.WorkingPos))
            {
                this.IsWorking = true;
                base.Move(this.WorkingPos);

                // Temps de construction
                Task.Delay(Convert.ToInt32(base.BuildTime)).Wait();
                return true;
            }

            return false;
        }

        public virtual bool WorkEnds()
        {
            if (!base.CurrentPos.Equals(this.ParkingPos))
            {
                base.Move(this.ParkingPos);
                this.IsWorking = false;
                return true;
            }

            return false;
        }
    }
}
