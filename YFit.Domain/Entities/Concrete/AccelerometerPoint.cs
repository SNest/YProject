namespace YFit.Domain.Entities.Concrete
{
    public class AccelerometerPoint : Point
    {
        public AccelerometerPoint(float x, float y, float z)
            : base(x, y, z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Exercise Exercise { get; set; }

        public int ExerciseId { get; set; }
    }
}