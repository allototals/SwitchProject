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
    public class schemeController : Controller
    {
       // private SwitchContext db = new SwitchContext();

        private ISchemes _schemeService;
        private IRoutes _routesService;
        private IFees _feesService;
        private IChannels _channelsService;
        private ITransactionType _transTypeService;
        public schemeController(ISchemes schemeService, IRoutes routeService, IFees feesService, IChannels channelsService, ITransactionType transService)
        {
            if (schemeService == null)
                throw new Exception("The Scheme service injection failed.");
            _schemeService = schemeService;
            _routesService = routeService;
            _feesService = feesService;
            _channelsService = channelsService;
            _transTypeService = transService;
        }
        // GET: /scheme/
        public async Task<ActionResult> Index()
        {
            var list = _schemeService.GetQueryable(x => x.Id != null);
            return View(list.ToList());//await db.Schemes.ToListAsync());
        }

        // GET: /scheme/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schemes schemes = await _schemeService.GetByIdAsync(id);
            if (schemes == null)
            {
                return HttpNotFound();
            }
            return View(schemes);
        }

        // GET: /scheme/Create
        public ActionResult Create()
        {
            ResultMessages<schemeViewModel> result = null;
            try
            {
                result = new ResultMessages<schemeViewModel>(true, string.Empty);
                var routes = _routesService.GetQueryable(x => x.Id != null);
                ViewBag.routes = new SelectList(routes.ToList(), "Id", "Name");
                ViewData["RoutesId"] = ViewBag.routes;

                var fees = _feesService.GetQueryable(x => x.Id != null);
                ViewBag.fees = new SelectList(fees.ToList(), "Id", "Name");

                var channels = _channelsService.GetQueryable(x => x.Id != null);
                ViewBag.channels = new SelectList(channels.ToList(), "Id", "Name");

                var trans = _transTypeService.GetQueryable(x => x.Id != null);
                ViewBag.trans = new SelectList(trans.ToList(), "Id", "Name");
            }
            catch(Exception ex) 
            {
                result = new ResultMessages<schemeViewModel>(false, ex.Message);
            }
            return View(result);
        }

        // POST: /scheme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ResultMessages<schemeViewModel> schemes)
        {
            ResultMessages<schemeViewModel> result = null;
            try
            {
                var routeslist = _routesService.GetQueryable(x => x.Id != null);
                ViewBag.routes = new SelectList(routeslist.ToList(), "Id", "Name");
                //ViewData["RoutesId"] = ViewBag.routes;

                var feeslist = _feesService.GetQueryable(x => x.Id != null);
                ViewBag.fees = new SelectList(feeslist.ToList(), "Id", "Name");

                var channelslist = _channelsService.GetQueryable(x => x.Id != null);
                ViewBag.channels = new SelectList(channelslist.ToList(), "Id", "Name");

                var translist = _transTypeService.GetQueryable(x => x.Id != null);
                ViewBag.trans = new SelectList(translist.ToList(), "Id", "Name");
               

                if (ModelState.IsValid)
                {
                    schemes.Data.Id = Guid.NewGuid();

                    //db.Schemes.Add(schemes);
                    var channels = await _channelsService.GetByIdAsync(schemes.Data.ChannelId);
                    var fees = await _feesService.GetByIdAsync(schemes.Data.FeesId);
                    var routes = await _routesService.GetByIdAsync(schemes.Data.RoutesId);
                    var transtype = await _transTypeService.GetByIdAsync(schemes.Data.TransTypeId);
                    await _schemeService.SaveAsync(schemes.Data.ConvertToEntityModel(channels, fees, routes, transtype));

                    result = new ResultMessages<schemeViewModel>();
                    result.Data = schemes.Data;
                   // return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                result = new ResultMessages<schemeViewModel>(false, ex.Message);
                
            }

            return View(result);
        }

        // GET: /scheme/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var routes = _routesService.GetQueryable(x => x.Id != null);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schemes schemes = await _schemeService.GetByIdAsync(id);//db.Schemes.FindAsync(id);
            ViewBag.routes = new SelectList(routes.ToList(), "Id", "Name", schemes.Route.Id);
            if (schemes == null)
            {
                return HttpNotFound();
            }
            return View(schemes);
        }

        // POST: /scheme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( schemeViewModel schemes)
        {
            if (ModelState.IsValid)
            {
                var channels = await _channelsService.GetByIdAsync(schemes.ChannelId);
                var fees = await _feesService.GetByIdAsync(schemes.FeesId);
                var routes = await _routesService.GetByIdAsync(schemes.RoutesId);
                var transtype = await _transTypeService.GetByIdAsync(schemes.TransTypeId);
                await _schemeService.UpdateAsync(schemes.ConvertToEntityModel(channels, fees, routes, transtype));   
               
                //db.Entry(schemes).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schemes);
        }

        // GET: /scheme/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schemes schemes = await  _schemeService.GetByIdAsync(id);
            if (schemes == null)
            {
                return HttpNotFound();
            }
            return View(schemes);
        }

        // POST: /scheme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
           await _schemeService.DeleteAsync(id);
            //Schemes schemes = await db.Schemes.FindAsync(id);
           // db.Schemes.Remove(schemes);
            //await db.SaveChangesAsync();
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
