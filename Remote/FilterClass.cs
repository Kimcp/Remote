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
        public double tick { get; set; }
        private Queue<Point> DataQueue;

        Point prv;

        public FilterClass()
        {
            WindowSize = 0;
            tick = 0;
            DataQueue = new Queue<Point>();
        }

        public virtual Point Estimate()
        {
            Point result;

            if (DataQueue.Count > 1)
            {
                Point dff = diff();
                result = new Point(this.DataQueue.Last().X + 5, this.DataQueue.Last().Y + dff.Y * 0.2);
            }
            else
            {
                result = new Point(this.DataQueue.Last().X + 5, this.DataQueue.Last().Y + 5);
            }
            prv = this.DataQueue.Last();
            return result;
        }

        private Point diff()
        {
            return new Point(this.DataQueue.Last().X - prv.X, this.DataQueue.Last().Y - prv.Y);
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
