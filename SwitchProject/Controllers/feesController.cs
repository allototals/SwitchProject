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

namespace SwitchProject.Controllers
{
    public class feesController : Controller
    {
       // private SwitchContext db = new SwitchContext();
        private   IFees _feesService;
        public feesController(IFees feeService)
        {
            if (feeService == null)
                throw new Exception("Fees Service Injection failed.");
            _feesService = feeService;
        }

        // GET: /fees/

        public async Task<ActionResult> Index()
        {
            var data = _feesService.GetQueryable(x => x.Id != null);
            return View(data);
        }

        // GET: /fees/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees fees = await _feesService.GetByIdAsync(id);
            if (fees == null)
            {
                return HttpNotFound();
            }
            return View(fees);
        }

        // GET: /fees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /fees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Name,FlatAmount,Percent,Maximum,Minimum,")] Fees fees)
        {
            if (ModelState.IsValid)
            {
                fees.Id = Guid.NewGuid();
               await _feesService.SaveAsync(fees);
               // db.Fees.Add(fees);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fees);
        }

        // GET: /fees/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees fees = await _feesService.GetByIdAsync(id);
            if (fees == null)
            {
                return HttpNotFound();
            }
            return View(fees);
        }

        // POST: /fees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Name,FlatAmount,Percent,Maximum,Minimum")] Fees fees)
        {
            if (ModelState.IsValid)
            {
                await _feesService.UpdateAsync(fees);
               // db.Entry(fees).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fees);
        }

        // GET: /fees/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees fees = await _feesService.GetByIdAsync(id);
            if (fees == null)
            {
                return HttpNotFound();
            }
            return View(fees);
        }

        // POST: /fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Fees fees = await _feesService.DeleteAsync(id);
           // db.Fees.Remove(fees);
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
