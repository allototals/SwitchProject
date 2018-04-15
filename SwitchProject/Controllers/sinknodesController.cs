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
    public class sinknodesController : Controller
    {
        private ISinkNode _sinknodeService;

        public sinknodesController(ISinkNode sinknodeService)
        {
            if (sinknodeService == null)
                throw new Exception("SinkNode service injection failed.");
            _sinknodeService = sinknodeService;
        }
        // GET: /sinknodes/
        public async Task<ActionResult> Index()
        {
            var list = _sinknodeService.GetQueryable(x=>x.Id!=null);
            return View(list);
        }

        // GET: /sinknodes/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinkNode sinknode = await _sinknodeService.GetByIdAsync(id);
            if (sinknode == null)
            {
                return HttpNotFound();
            }
            return View(sinknode);
        }

        // GET: /sinknodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /sinknodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Name,HostName,IPAdress,Port,Status")] SinkNode sinknode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    sinknode.Id = Guid.NewGuid();
                    await _sinknodeService.SaveAsync(sinknode);
                }
                catch(Exception ex)
                {
                    throw ex;

                }
                //db.SinkNode.Add(sinknode);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sinknode);
        }

        // GET: /sinknodes/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinkNode sinknode = await _sinknodeService.GetByIdAsync(id);
            if (sinknode == null)
            {
                return HttpNotFound();
            }
            return View(sinknode);
        }

        // POST: /sinknodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,HostName,IPAdress,Port,Status")] SinkNode sinknode)
        {
            if (ModelState.IsValid)
            {
                await _sinknodeService.UpdateAsync(sinknode);
                //db.Entry(sinknode).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sinknode);
        }

        // GET: /sinknodes/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinkNode sinknode = await _sinknodeService.GetByIdAsync(id);// db.SinkNode.FindAsync(id);
            if (sinknode == null)
            {
                return HttpNotFound();
            }
            return View(sinknode);
        }

        // POST: /sinknodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _sinknodeService.DeleteAsync(id);
            //SinkNode sinknode = await db.SinkNode.FindAsync(id);
            //db.SinkNode.Remove(sinknode);

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
