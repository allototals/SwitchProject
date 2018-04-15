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
    public class transactiontypesController : Controller
    {
       // private SwitchContext db = new SwitchContext();

        // GET: /transactiontypes/

        private ITransactionType _transactionService;
        public transactiontypesController(ITransactionType transactionService)
        {
            if (transactionService == null)
                throw new Exception("Transaction service injection failed.");
            _transactionService = transactionService;
        }
        public async Task<ActionResult> Index()
        {
            var list = _transactionService.GetQueryable(x => x.Id != null);
            return View(list);//await db.TransactionType.ToListAsync());
        }

        // GET: /transactiontypes/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionType transactiontype = await _transactionService.GetByIdAsync(id);//.TransactionType.FindAsync(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        // GET: /transactiontypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /transactiontypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Name,Code,Description")] TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                transactiontype.Id = Guid.NewGuid();
              await  _transactionService.SaveAsync(transactiontype);
              //  db.TransactionType.Add(transactiontype);
               // await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(transactiontype);
        }

        // GET: /transactiontypes/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionType transactiontype = await _transactionService.GetByIdAsync(id);// db.TransactionType.FindAsync(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        // POST: /transactiontypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Code,Description")] TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
               await _transactionService.UpdateAsync(transactiontype);
               // db.Entry(transactiontype).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transactiontype);
        }

        // GET: /transactiontypes/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionType transactiontype = await _transactionService.GetByIdAsync(id);//db.TransactionType.FindAsync(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        // POST: /transactiontypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _transactionService.DeleteAsync(id);
           // TransactionType transactiontype = await db.TransactionType.FindAsync(id);
           // db.TransactionType.Remove(transactiontype);
           // await db.SaveChangesAsync();
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
