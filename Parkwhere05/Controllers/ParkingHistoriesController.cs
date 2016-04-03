using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Parkwhere05.DAL;
using Parkwhere05.Models;


namespace Parkwhere05.Controllers
{
    public class ParkingHistoriesController : GeneralController<ParkingHistory>
    {
        private ParkWhereDBEntities db = new ParkWhereDBEntities();

        public ParkingHistoriesController()
        {
            dataGateway = new ParkingHistoryGateway();
        }

        [Authorize]
        // GET: ParkingHistories
        override public ActionResult Index(int? id)
        {

            if (User.IsInRole("Admin"))
            {
                return View(dataGateway.SelectAll());
            }

            else
            {
                var userParkingHistory = from j in dataGateway.SelectAll() select j ;
                userParkingHistory = userParkingHistory.Where(ParkingHistory => ParkingHistory.username == User.Identity.Name);
                return View(userParkingHistory.OrderByDescending(ParkingHistory => ParkingHistory.date));
            }
            //var parkingHistories = db.ParkingHistories.Include(p => p.Carpark);
            //return View(parkingHistories.ToList());
        }

        

        // GET: ParkingHistories/Create
        public ActionResult Create(int carparkId, String address)
        {
            ParkingHistory parkinghistory = new ParkingHistory();
            //ViewBag.carparkId = new SelectList(db.Carparks, "id", "carparkNo");
            ViewBag.address = address;
            parkinghistory.username = User.Identity.Name;
            parkinghistory.date = DateTime.Now;
            return View(parkinghistory);
        }

        // POST: ParkingHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "parkingHistoryId,carparkId,username,date,description")] ParkingHistory parkingHistory)
        {
            if (ModelState.IsValid)
            {
                db.ParkingHistories.Add(parkingHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.carparkId = new SelectList(db.Carparks, "id", "carparkNo", parkingHistory.carparkId);
            return View(parkingHistory);
        }

        // GET: ParkingHistories/Edit/5
        public ActionResult Edit(int? id, String address)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingHistory parkingHistory = db.ParkingHistories.Find(id);
            if (parkingHistory == null)
            {
                return HttpNotFound();
            }
            //ViewBag.carparkId = new SelectList(db.Carparks, "id", "carparkNo", parkingHistory.carparkId);
            ViewBag.address = address;
            return View(parkingHistory);
        }

        // POST: ParkingHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "parkingHistoryId,carparkId,username,date,description")] ParkingHistory parkingHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.carparkId = new SelectList(db.Carparks, "id", "carparkNo", parkingHistory.carparkId);
            return View(parkingHistory);
        }

        // GET: ParkingHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingHistory parkingHistory = db.ParkingHistories.Find(id);
            if (parkingHistory == null)
            {
                return HttpNotFound();
            }
            return View(parkingHistory);
        }

        // POST: ParkingHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkingHistory parkingHistory = db.ParkingHistories.Find(id);
            db.ParkingHistories.Remove(parkingHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
