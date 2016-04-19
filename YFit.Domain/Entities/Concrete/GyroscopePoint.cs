namespace YFit.Domain.Entities.Concrete
{
    public class GyroscopePoint : Point
    {
        public GyroscopePoint(float x, float y, float z)
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