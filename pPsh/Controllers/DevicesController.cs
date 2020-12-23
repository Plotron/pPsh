using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using pPsh.Models;
using PagedList;

namespace pPsh.Controllers
{
    public class DevicesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Devices
        [Authorize(Roles = "user, admin")]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EnabledSortParam = sortOrder == "enabled_desc" ? "enabled_asc" : "enabled_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<Device> result = db.Profiles.Include(p => p.Devices).Single(p => p.UserName == User.Identity.Name).Devices.ToList();
            //IEnumerable<Device> result = db.Devices.Where(d => d.ProfileID == db.Profiles.FirstOrDefault(p => p.UserName == User.Identity.Name).ID).ToList();
            //IEnumerable<Device> result = db.Devices.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    result = result?.OrderByDescending(d => d.Name);
                    break;
                case "enabled_asc":
                    result = result?.OrderBy(d => d.Enabled);
                    break;
                case "enabled_desc":
                    result = result?.OrderByDescending(d => d.Enabled);
                    break;
                default:
                    result = result?.OrderBy(d => d.Name);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(result?.ToPagedList(pageNumber, pageSize));
        }

        // GET: Devices/Details/5
        [Authorize(Roles = "user, admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Device device = db.Devices.Find(id);
            if (device == null || device.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "user")]
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Key,Enabled")]
            Device device)
        {
            if (ModelState.IsValid)
            {
                device.ProfileID = db.Profiles.Single(p => p.UserName == User.Identity.Name).ID;

                var imageFile = Request.Files["imageFile"];
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    device.ImageName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    imageFile.SaveAs(Path.Combine(Server.MapPath("~/UserFiles/Images/Device"), device.ImageName));
                }

                db.Devices.Add(device);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(device);
        }

        // GET: Devices/Edit/5
        [Authorize(Roles = "user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = db.Devices.Find(id);
            if (device == null || device.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }

            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Key,Enabled")]
            Device device)
        {
            if (ModelState.IsValid)
            {
                device.ProfileID = db.Profiles.Single(p => p.UserName == User.Identity.Name).ID;
                var imageFile = Request.Files["imageFile"];
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    device.ImageName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    imageFile.SaveAs(Path.Combine(Server.MapPath("~/UserFiles/Images/Device"), device.ImageName));
                }

                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(device);
        }

        // GET: Devices/Delete/5
        [Authorize(Roles = "user, admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = db.Devices.Find(id);
            if (device == null || device.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [Authorize(Roles = "user, admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.Devices.Find(id);

            db.Devices.Remove(device);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult Toggle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = db.Devices.Find(id);

            if (device == null || device.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }

            device.Enabled = !device.Enabled;

            db.Entry(device).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
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