using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Tools;
using BotFactory.Common.Interface;

namespace BotFactory.Models
{
    public abstract class WorkingUnit : BaseUnit, IWorkingUnit
    {
        private Coordinates parkingPos;
        public Coordinates ParkingPos
        {
            get { return this.parkingPos; }
            set { this.parkingPos = value; }
        }

        private Coordinates workingPos;
        public Coordinates WorkingPos
        {
            get { return this.workingPos; }
            set { this.workingPos = value; }
        }

        private bool isWorking;
        public bool IsWorking
        {
            get { return this.isWorking; }
        }

        //public WorkingUnit(string name, double speed, Coordinates parkingPos, Coordinates workingPos, bool isWorking)
        //    : base (name, speed)
        //{
        //    this.ParkingPos = parkingPos;
        //    this.WorkingPos = workingPos;
        //    this.isWorking = isWorking;
        //}

        public WorkingUnit(string name, double speed)
            : base(name, speed)
        {
            this.ParkingPos = new Coordinates(0, 0);
            this.WorkingPos = new Coordinates(0, 0);
            this.isWorking = false;
        }

        public virtual void WorkBegins()
        {
            if (!base.CurrentPos.Equals(this.WorkingPos))
            {
                this.isWorking = true;
                base.Move(this.WorkingPos);

                // Temps de construction
                Task.Delay(Convert.ToInt32(base.BuildTime)).Wait();
            }
        }

        public virtual void WorkEnds()
        {
            if (!base.CurrentPos.Equals(this.ParkingPos))
            {
                base.Move(this.ParkingPos);
                this.isWorking = false;
            }
        }
    }
}
