using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Remote
{
    public class FilterClass
    {
        public int WindowSize { get; set; }
        private Queue<Point> DataQueue;

        public FilterClass()
        {
            WindowSize = 0;
            DataQueue = new Queue<Point>();
        }

        public virtual Point Estimate() 
        {
            return new Point(this.DataQueue.Last().X+5, this.DataQueue.Last().Y+5);
        }

        public void Add(Point p)
        {
            this.DataQueue.Enqueue(p);
        }

        public Point Remove()
        {
            return this.DataQueue.Dequeue();
        }
    }

    public class FilterLinear : FilterClass
    {
        override public Point Estimate()
        {
            return new Point();
        }
    }
}
