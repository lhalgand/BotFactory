using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;

namespace BotFactory.Common.Tools
{
    public class Coordinates : ICoordinates
    {
        public double X { get; set; }
        
        public double Y { get; set; }

        public Coordinates(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinates)
            {
                Coordinates coordinates = obj as Coordinates;
                if (coordinates != null && coordinates.X == this.X && coordinates.Y == this.Y)
                { return true; }
            }
            return false;
        }
    }
}
