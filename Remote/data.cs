using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Remote
{
    public enum PointType
    {
        Transmission,
        reception
    }

    public class data
    {
        public PointType TransmissionPoint { get; set; }
        public Point EstimatedPoint { get; set; }
        public Point PredictedPoint { get; set; }
    }
}
