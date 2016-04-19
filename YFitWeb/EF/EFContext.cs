namespace YFit.Web.EF
{
    using System.Data.Entity;

    using YFit.Domain.Entities.Concrete;

    public class EfContext : DbContext
    {
        public EfContext()
            : base("YFitDbConnection")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }

        public EfContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<AccelerometerPoint> AccelerometerPoints { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<GyroscopePoint> GyroscopePoints { get; set; }

        public DbSet<PointsFrame> PointsFrames { get; set; }

        public DbSet<Training> Trainings { get; set; }
    }
}