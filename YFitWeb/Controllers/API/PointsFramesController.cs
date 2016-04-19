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
    using YFit.Domain.Entities.Concrete;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PointsFrame>("PointsFrames");
    builder.EntitySet<AccelerometerPoint>("AccelerometerPoints"); 
    builder.EntitySet<GyroscopePoint>("GyroscopePoints"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PointsFramesController : ODataController
    {
        private EfContext db = new EfContext();

        // DELETE: odata/PointsFrames(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PointsFrame pointsFrame = db.PointsFrames.Find(key);
            if (pointsFrame == null)
            {
                return NotFound();
            }

            db.PointsFrames.Remove(pointsFrame);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PointsFrames(5)/AccelerometerPoints
        [EnableQuery]
        public IQueryable<AccelerometerPoint> GetAccelerometerPoints([FromODataUri] int key)
        {
            return db.PointsFrames.Where(m => m.Id == key).SelectMany(m => m.AccelerometerPoints);
        }

        // GET: odata/PointsFrames(5)/GyroscopePoints
        [EnableQuery]
        public IQueryable<GyroscopePoint> GetGyroscopePoints([FromODataUri] int key)
        {
            return db.PointsFrames.Where(m => m.Id == key).SelectMany(m => m.GyroscopePoints);
        }

        // GET: odata/PointsFrames(5)
        [EnableQuery]
        public SingleResult<PointsFrame> GetPointsFrame([FromODataUri] int key)
        {
            return SingleResult.Create(db.PointsFrames.Where(pointsFrame => pointsFrame.Id == key));
        }

        // GET: odata/PointsFrames
        [EnableQuery]
        public IQueryable<PointsFrame> GetPointsFrames()
        {
            return db.PointsFrames;
        }

        // PATCH: odata/PointsFrames(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PointsFrame> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PointsFrame pointsFrame = db.PointsFrames.Find(key);
            if (pointsFrame == null)
            {
                return NotFound();
            }

            patch.Patch(pointsFrame);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointsFrameExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pointsFrame);
        }

        // POST: odata/PointsFrames
        public IHttpActionResult Post(PointsFrame pointsFrame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PointsFrames.Add(pointsFrame);
            db.SaveChanges();

            return Created(pointsFrame);
        }

        // PUT: odata/PointsFrames(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PointsFrame> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PointsFrame pointsFrame = db.PointsFrames.Find(key);
            if (pointsFrame == null)
            {
                return NotFound();
            }

            patch.Put(pointsFrame);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointsFrameExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pointsFrame);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool PointsFrameExists(int key)
        {
            return db.PointsFrames.Count(e => e.Id == key) > 0;
        }
    }
}