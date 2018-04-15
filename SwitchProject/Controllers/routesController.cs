using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Switch.Data.Models;
using Switch.Data;
using Switch.Service.Interface;
using SwitchProject.Models;

namespace SwitchProject.Controllers
{
    public class routesController : Controller
    {
       // private SwitchContext db = new SwitchContext();
        private IRoutes _routeService;
        private ISinkNode _sinkNodeService;

        public routesController(IRoutes routeService, ISinkNode sinkNodeService)
        {
            if (routeService == null)
                throw new Exception("Route Service Injection failed.");
            _routeService = routeService;
            if (sinkNodeService == null)
                throw new Exception("Sink Node Service Injection failed.");
            _sinkNodeService = sinkNodeService;
        }

        // GET: /routes/
        public async Task<ActionResult> Index()
        {
            var list = _routeService.GetQueryable(x => x.Id != null).ToList();
            return View(list);
        }

        // GET: /routes/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await _routeService.GetByIdAsync(id);//await db.Routes.FindAsync(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // GET: /routes/Create
        public ActionResult Create()
        {
           var items = _sinkNodeService.GetQueryable(x => x.Id != null);
           ViewBag.sinknodes = new SelectList(items.ToList(), "Id", "Name");
            return View();
        }

        // POST: /routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(/*[Bind(Include = "Name,CardPAN,Description,SinkNodeId")]*/ routesViewModel routes)
        {
            if (ModelState.IsValid)
            {
                var obj = new Routes()
                {
                    CardPAN = routes.CardPAN,
                    Description = routes.Description,
                    Name = routes.Name
                };
                obj.Id = Guid.NewGuid();
                obj.SinkNode = await _sinkNodeService.GetByIdAsync(routes.SinkNodeId);
                await _routeService.SaveAsync(obj);
                //db.Routes.Add(routes);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(routes);
        }

        // GET: /routes/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var sinks=_sinkNodeService.GetQueryable(x => x.Id != null);
           
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await _routeService.GetByIdAsync(id);//db.Routes.FindAsync(id);
             ViewBag.sinknodes = new SelectList(sinks, "Id", "Name",routes.SinkNode.Id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes.ConvertToViewModel());
        }

        // POST: /routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*[Bind(Include="Id,Name,CardPAN,Description,SinkNode")]*/ routesViewModel routes)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(routes).State = EntityState.Modified;
                Routes obj = routes.ConvertToEntityModel();
                obj.SinkNode = await _sinkNodeService.GetByIdAsync(routes.SinkNodeId);
                await _routeService.UpdateAsync(obj);//db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(routes);
        }

        // GET: /routes/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await _routeService.GetByIdAsync(id);//db.Routes.FindAsync(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: /routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
           
            try
            {
                Routes routes = await _routeService.GetByIdAsync(id);//db.Routes.FindAsync(id);

                // db.Routes.Remove(routes);
                await _routeService.DeleteAsync(id);//db.SaveChangesAsync();
                new ResultMessages<string>(true, "The operation was successful.");
            }
            catch(Exception ex)
            {
                new ResultMessages<string>(false, ex.Message);
            }
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
