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
    builder.EntitySet<Training>("Trainings");
    builder.EntitySet<Exercise>("Exercises"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TrainingsController : ODataController
    {
        private EfContext db = new EfContext();

        // DELETE: odata/Trainings(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Training training = db.Trainings.Find(key);
            if (training == null)
            {
                return NotFound();
            }

            db.Trainings.Remove(training);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Trainings(5)/Exercises
        [EnableQuery]
        public IQueryable<Exercise> GetExercises([FromODataUri] int key)
        {
            return db.Trainings.Where(m => m.Id == key).SelectMany(m => m.Exercises);
        }

        // GET: odata/Trainings(5)
        [EnableQuery]
        public SingleResult<Training> GetTraining([FromODataUri] int key)
        {
            return SingleResult.Create(db.Trainings.Where(training => training.Id == key));
        }

        // GET: odata/Trainings
        [EnableQuery]
        public IQueryable<Training> GetTrainings()
        {
            return db.Trainings;
        }

        // PATCH: odata/Trainings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Training> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Training training = db.Trainings.Find(key);
            if (training == null)
            {
                return NotFound();
            }

            patch.Patch(training);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(training);
        }

        // POST: odata/Trainings
        public IHttpActionResult Post(Training training)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trainings.Add(training);
            db.SaveChanges();

            return Created(training);
        }

        // PUT: odata/Trainings(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Training> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Training training = db.Trainings.Find(key);
            if (training == null)
            {
                return NotFound();
            }

            patch.Put(training);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(training);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool TrainingExists(int key)
        {
            return db.Trainings.Count(e => e.Id == key) > 0;
        }
    }
}