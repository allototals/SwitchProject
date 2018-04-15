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
    public class sourcenodeController : Controller
    {
       // private SwitchContext db = new SwitchContext();
        private ISourceNode _sourceNodeService;
        // GET: /sourcenode/

        public sourcenodeController(ISourceNode sourceNodeService)
        {
            if (sourceNodeService == null)
                throw new Exception("source Node Injection failed.");
            _sourceNodeService = sourceNodeService;
        }
        public async Task<ActionResult> Index()
        {
            var list = _sourceNodeService.GetQueryable(x => x.Id != null);
            return View(list);
        }

        // GET: /sourcenode/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceNode sourcenode = await _sourceNodeService.GetByIdAsync(id);
            if (sourcenode == null)
            {
                return HttpNotFound();
            }
            return View(sourcenode);
        }

        // GET: /sourcenode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /sourcenode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Name,HostName,IPAdress,Port,Status")] SourceNode sourcenode)
        {
            if (ModelState.IsValid)
            {
                sourcenode.Id = Guid.NewGuid();
               // db.SourceNode.Add(sourcenode);
                await _sourceNodeService.SaveAsync(sourcenode);//db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sourcenode);
        }

        // GET: /sourcenode/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceNode sourcenode = await _sourceNodeService.GetByIdAsync(id);
            if (sourcenode == null)
            {
                return HttpNotFound();
            }
            return View(sourcenode);
        }

        // POST: /sourcenode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Name,HostName,IPAdress,Port,Status")] SourceNode sourcenode)
        {
            if (ModelState.IsValid)
            {
                await _sourceNodeService.UpdateAsync(sourcenode);
               // db.Entry(sourcenode).State = EntityState.Modified;

                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sourcenode);
        }

        // GET: /sourcenode/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceNode sourcenode = await _sourceNodeService.GetByIdAsync(id);//db.SourceNode.FindAsync(id);
            if (sourcenode == null)
            {
                return HttpNotFound();
            }
            return View(sourcenode);
        }

        // POST: /sourcenode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
           // SourceNode sourcenode = await _sourceNodeService.GetByIdAsync(id);//FindAsync(id);
           // db.SourceNode.Remove(sourcenode);
            await _sourceNodeService.DeleteAsync(id);//db.SaveChangesAsync();
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
