namespace YFit.Mobile.Services.Concrete
{
    using System.Collections.Generic;
    using System.Linq;

    using YFit.Domain.Entities.Concrete;
    using YFit.Mobile.Services.Abstract;

    public class GyroscopeService : IGyroscopeService
    {
        public IEnumerable<GyroscopePoint> GyroscopePoints { get; set; }

        public IEnumerable<GyroscopePoint> GetGyroscopePoints(int number)
        {
            return GyroscopePoints.Take(number);
        }
    }
}