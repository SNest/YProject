namespace YFit.Domain.Entities.Concrete
{
    using YFit.Domain.Entities.Abstract;

    public class Point : Entity
    {
        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }
    }
}