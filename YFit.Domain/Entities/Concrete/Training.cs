namespace YFit.Domain.Entities.Concrete
{
    using System.Collections.Generic;

    using YFit.Domain.Entities.Abstract;

    public class Training : Entity
    {
        public Training()
        {
            Exercises = new HashSet<Exercise>();
        }

        public ICollection<Exercise> Exercises { get; set; }

        public string Name { get; set; }
    }
}