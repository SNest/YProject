namespace YFit.Domain.Entities.Concrete
{
    using System;
    using System.Collections.Generic;

    using YFit.Domain.Entities.Abstract;

    public class Exercise : Entity
    {
        public Exercise()
        {
            AccelerometerPoints = new HashSet<AccelerometerPoint>();
            GyroscopePoints = new HashSet<GyroscopePoint>();
        }

        public ICollection<AccelerometerPoint> AccelerometerPoints { get; set; }

        public int CurrentQuantity { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<GyroscopePoint> GyroscopePoints { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public Training Training { get; set; }

        public int? TrainingId { get; set; }
    }
}