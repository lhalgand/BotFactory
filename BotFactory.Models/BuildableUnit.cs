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
        public double BuildTime { get; set; }

        public Type Model { get; set; }

        public BuildableUnit(double buildTime = 5000)
        {
            this.BuildTime = buildTime;
        }
    }
}
