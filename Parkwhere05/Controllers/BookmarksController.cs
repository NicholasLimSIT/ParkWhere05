using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Parkwhere05.Models;
using Parkwhere05.DAL;

namespace Parkwhere05.Controllers
{
    public class BookmarksController : GeneralController<Bookmark>
    {
        private ParkWhereDBEntities db = new ParkWhereDBEntities();

        public BookmarksController()
        {
            dataGateway = new BookmarkGateway();
        }

        
        [Authorize]
        // GET: Bookmarks/Create
        public ActionResult Create(int carparkId, String address)
        {
            
            Bookmark bookmark = new Bookmark();
            bookmark.username = User.Identity.Name;
            bookmark.date = DateTime.Now;
            bookmark.carparkId = carparkId;
            ViewBag.address = address;
            return View(bookmark);
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "carparkId,date,username")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
       
            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        public ActionResult Edit(int? id, String address)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            //ViewBag.carparkId = new SelectList(db.Carparks, "id", "carparkNo", bookmark.carparkId);
            ViewBag.address = address;
            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookmarkId,carparkId,date,username")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookmark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }        
            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            db.Bookmarks.Remove(bookmark);
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

        [Authorize]
        override public ActionResult Index(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                return View(dataGateway.SelectAll());
            }

            else
            {
                var userBookmark = from j in dataGateway.SelectAll() select j;
                userBookmark = userBookmark.Where(Bookmark => Bookmark.username == User.Identity.Name);
                return View(userBookmark);
            }

        }

       
    }
}
