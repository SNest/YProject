namespace YFit.Mobile.Services.Abstract
{
    using System.Collections.Generic;

    using YFit.Domain.Entities.Concrete;

    public interface IAccelerometerService : ISensorService
    {
        IEnumerable<AccelerometerPoint> AccelerometerPoints { get; set; }
    }
}