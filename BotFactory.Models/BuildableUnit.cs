using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;

namespace BotFactory.Models
{
    public abstract class BuildableUnit : IBuildableUnit
    {
        private double buildTime;
        public double BuildTime
        {
            get { return this.buildTime; }
            set { this.buildTime = value; }
        }

        public BuildableUnit(double buildTime = 5000)
        {
            this.BuildTime = buildTime;
        }
    }
}
