namespace YFit.Web.Controllers.Web
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using YFit.Domain.Entities.Concrete;
    using YFit.Web.EF;

    public class PointsFramesController : Controller
    {
        private EfContext db = new EfContext();

        // GET: PointsFrames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PointsFrames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] PointsFrame pointsFrame)
        {
            if (ModelState.IsValid)
            {
                db.PointsFrames.Add(pointsFrame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pointsFrame);
        }

        // GET: PointsFrames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PointsFrame pointsFrame = db.PointsFrames.Find(id);
            if (pointsFrame == null)
            {
                return HttpNotFound();
            }

            return View(pointsFrame);
        }

        // POST: PointsFrames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PointsFrame pointsFrame = db.PointsFrames.Find(id);
            db.PointsFrames.Remove(pointsFrame);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: PointsFrames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PointsFrame pointsFrame = db.PointsFrames.Find(id);
            if (pointsFrame == null)
            {
                return HttpNotFound();
            }

            return View(pointsFrame);
        }

        // GET: PointsFrames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PointsFrame pointsFrame = db.PointsFrames.Find(id);
            if (pointsFrame == null)
            {
                return HttpNotFound();
            }

            return View(pointsFrame);
        }

        // POST: PointsFrames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] PointsFrame pointsFrame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pointsFrame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pointsFrame);
        }

        // GET: PointsFrames
        public ActionResult Index()
        {
            return View(db.PointsFrames.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}