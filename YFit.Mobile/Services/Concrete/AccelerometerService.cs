namespace YFit.Mobile.Services.Concrete
{
    using System.Collections.Generic;
    using System.Linq;

    using YFit.Domain.Entities.Concrete;
    using YFit.Mobile.Services.Abstract;

    public class AccelerometerService : IAccelerometerService
    {
        public IEnumerable<AccelerometerPoint> AccelerometerPoints { get; set; }

        public IEnumerable<AccelerometerPoint> GetAccelerometerPoints(int number)
        {
            return AccelerometerPoints.Take(number);
        }
    }
}