using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BotFactory.Common.Interface;

namespace BotFactory.Common.Tools
{
    public class Vector : IVector
    {
        private double x;
        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        private double y;
        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        protected static Vector FromCoordinates(Coordinates begin, Coordinates end)
        {
            Vector vector = new Vector();
            vector.X = end.X - begin.X;
            vector.Y = end.Y - begin.Y;
            return vector;
        }

        public double Length(Coordinates begin, Coordinates end)
        {
            return Math.Sqrt(Math.Pow(end.X - begin.X, 2) + Math.Pow(begin.X - begin.X, 2)); ;
        }
    }
}
