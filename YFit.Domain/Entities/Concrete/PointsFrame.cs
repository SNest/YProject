namespace YFit.Domain.Entities.Concrete
{
    using System.Collections.Generic;

    using YFit.Domain.Entities.Abstract;

    public class PointsFrame : Entity
    {
        public PointsFrame()
        {
            AccelerometerPoints = new HashSet<AccelerometerPoint>();
            GyroscopePoints = new HashSet<GyroscopePoint>();
        }

        public ICollection<AccelerometerPoint> AccelerometerPoints { get; set; }

        public ICollection<GyroscopePoint> GyroscopePoints { get; set; }
    }
}
