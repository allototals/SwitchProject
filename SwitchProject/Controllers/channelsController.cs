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
using Project.Core.Exceptions;

namespace SwitchProject.Controllers
{
    public class channelsController : Controller
    {
        private IChannels _channelsService;
        public channelsController(IChannels channelsService)
        {
            if (channelsService == null)
                throw new ProjectException("Channels Service injection failed.");
            _channelsService = channelsService;

        }
       // private SwitchContext db = new SwitchContext();

        // GET: /channels/
        public async Task<ActionResult> Index()
        {
            var data = _channelsService.GetQueryable(x=>x.Id!=null);
            return View(data.OrderBy(x=>x.Name));
        }

        // GET: /channels/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channels channels = await _channelsService.GetByIdAsync(id.Value);
            if (channels == null)
            {
                return HttpNotFound();
            }
            return View(channels);
        }

        // GET: /channels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Name,Code,Description")] Channels channels)
        {
            if (ModelState.IsValid)
            {
                await _channelsService.SaveAsync(channels);
                //channels.Id = Guid.NewGuid();
                //db.channels.Add(channels);
              //  await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(channels);
        }

        // GET: /channels/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channels channels = await _channelsService.GetByIdAsync(id);
            if (channels == null)
            {
                return HttpNotFound();
            }
            return View(channels);
        }

        // POST: /channels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Code,Description")] Channels channels)
        {
            if (ModelState.IsValid)
            {
               await  _channelsService.UpdateAsync(channels);
              //  db.Entry(channels).State = EntityState.Modified;
              //  await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(channels);
        }

        // GET: /channels/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channels channels = await _channelsService.GetByIdAsync(id);// db.channels.FindAsync(id);
            if (channels == null)
            {
                return HttpNotFound();
            }
            return View(channels);
        }

        // POST: /channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Channels channels = await _channelsService.DeleteAsync(id);
           // db.channels.Remove(channels);
           // await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

       
    }
}
