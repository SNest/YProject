namespace YFit.Mobile.Services.Abstract
{
    using System.Collections.Generic;

    using YFit.Domain.Entities.Concrete;

    public interface IGyroscopeService : ISensorService
    {
        IEnumerable<GyroscopePoint> GyroscopePoints { get; set; }
    }
}