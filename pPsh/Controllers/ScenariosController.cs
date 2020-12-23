using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pPsh.Models;
using PagedList;

namespace pPsh.Controllers
{
    public class ScenariosController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Authorize(Roles = "user, admin")]
        // GET: Scenarios
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
            
            IEnumerable<Scenario> result = db.Profiles.Include(p => p.Scenarios)
                .Single(p => p.UserName == User.Identity.Name)
                .Scenarios.ToList();
            
            //IEnumerable<Scenario> result = db.Scenarios.ToList();

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

        [Authorize(Roles = "user")]
        // GET: Scenarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.Scenarios.Find(id);
            if (scenario == null || scenario.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }
            return View(scenario);
        }

        [Authorize(Roles = "user")]
        // GET: Scenarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scenarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Enabled,Name")] Scenario scenario)
        {
            if (ModelState.IsValid)
            {
                scenario.ProfileID = db.Profiles.Single(p => p.UserName == User.Identity.Name).ID;

                db.Scenarios.Add(scenario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scenario);
        }

        // GET: Scenarios/Edit/5
        [Authorize(Roles = "user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.Scenarios.Find(id);
            if (scenario == null || scenario.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }
            return View(scenario);
        }

        // POST: Scenarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Enabled,Name")] Scenario scenario)
        {
            if (ModelState.IsValid)
            {
                scenario.ProfileID = db.Profiles.Single(p => p.UserName == User.Identity.Name).ID;
                db.Entry(scenario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scenario);
        }

        // GET: Scenarios/Delete/5
        [Authorize(Roles = "user")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.Scenarios.Find(id);
            if (scenario == null || scenario.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }
            return View(scenario);
        }

        // POST: Scenarios/Delete/5
        [Authorize(Roles = "user")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scenario scenario = db.Scenarios.Find(id);
            db.Scenarios.Remove(scenario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult Toggle(int id)
        {
            Scenario scenario = db.Scenarios.Find(id);

            if (scenario == null || scenario.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }

            scenario.Enabled = !scenario.Enabled;

            db.Entry(scenario).State = EntityState.Modified;
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

        [Authorize(Roles = "user")]
        public ActionResult ModifyActions(int id)
        {
            Scenario scenario = db.Scenarios.Find(id);

            if (scenario == null || scenario.Profile != db.Profiles.Single(p => p.UserName == User.Identity.Name))
            {
                return HttpNotFound();
            }

            ViewBag.Scenario = scenario;
            ViewBag.EmailActions = scenario.ScenarioEmails.ToList();

            return View();
        }

        public ActionResult EditEmailAction(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteEmailAction(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult CreateEmailAction(object id)
        {
            throw new NotImplementedException();
        }
    }
}
