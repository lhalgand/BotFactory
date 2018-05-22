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
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private double speed;
        public double Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        private Coordinates currentPos;
        public Coordinates CurrentPos
        {
            get { return this.currentPos; }
            set { this.currentPos = value; }
        }

        private Vector vector;
        public Vector Vector
        {
            get { return this.vector; }
            set { this.vector = value; }
        }

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
