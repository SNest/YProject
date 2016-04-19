namespace YFit.Web.Controllers.API
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.OData;

    using YFit.Domain.Entities.Concrete;
    using YFit.Web.EF;

    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using YFit.Web.Entities.Concrete;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Exercise>("Exercises");
    builder.EntitySet<AccelerometerPoint>("AccelerometerPoints"); 
    builder.EntitySet<GyroscopePoint>("GyroscopePoints"); 
    builder.EntitySet<Training>("Trainings"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExercisesController : ODataController
    {
        private EfContext db = new EfContext();

        // DELETE: odata/Exercises(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Exercise exercise = db.Exercises.Find(key);
            if (exercise == null)
            {
                return NotFound();
            }

            db.Exercises.Remove(exercise);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Exercises(5)/AccelerometerPoints
        [EnableQuery]
        public IQueryable<AccelerometerPoint> GetAccelerometerPoints([FromODataUri] int key)
        {
            return db.Exercises.Where(m => m.Id == key).SelectMany(m => m.AccelerometerPoints);
        }

        // GET: odata/Exercises(5)
        [EnableQuery]
        public SingleResult<Exercise> GetExercise([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exercises.Where(exercise => exercise.Id == key));
        }

        // GET: odata/Exercises
        [EnableQuery]
        public IQueryable<Exercise> GetExercises()
        {
            return db.Exercises;
        }

        // GET: odata/Exercises(5)/GyroscopePoints
        [EnableQuery]
        public IQueryable<GyroscopePoint> GetGyroscopePoints([FromODataUri] int key)
        {
            return db.Exercises.Where(m => m.Id == key).SelectMany(m => m.GyroscopePoints);
        }

        // GET: odata/Exercises(5)/Training
        [EnableQuery]
        public SingleResult<Training> GetTraining([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exercises.Where(m => m.Id == key).Select(m => m.Training));
        }

        // PATCH: odata/Exercises(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Exercise> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exercise exercise = db.Exercises.Find(key);
            if (exercise == null)
            {
                return NotFound();
            }

            patch.Patch(exercise);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exercise);
        }

        // POST: odata/Exercises
        public IHttpActionResult Post(Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exercises.Add(exercise);
            db.SaveChanges();

            return Created(exercise);
        }

        // PUT: odata/Exercises(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Exercise> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exercise exercise = db.Exercises.Find(key);
            if (exercise == null)
            {
                return NotFound();
            }

            patch.Put(exercise);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exercise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool ExerciseExists(int key)
        {
            return db.Exercises.Count(e => e.Id == key) > 0;
        }
    }
}