using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using attempt_api.Models;

namespace attempt_api.Controllers
{
    public class Shopbridge_inventory_1Controller : ApiController
    {
        private mulanEntities2 db = new mulanEntities2();

        // GET: api/Shopbridge_inventory_1
        public IQueryable<Shopbridge_inventory_1> GetShopbridge_inventory_1()
        {
            return db.Shopbridge_inventory_1;
        }

        // GET: api/Shopbridge_inventory_1/5
        [ResponseType(typeof(Shopbridge_inventory_1))]
        public IHttpActionResult GetShopbridge_inventory_1(string id)
        {
            Shopbridge_inventory_1 shopbridge_inventory_1 = db.Shopbridge_inventory_1.Find(id);
            if (shopbridge_inventory_1 == null)
            {
                return NotFound();
            }

            return Ok(shopbridge_inventory_1);
        }

        // PUT: api/Shopbridge_inventory_1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShopbridge_inventory_1(string id, Shopbridge_inventory_1 shopbridge_inventory_1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shopbridge_inventory_1.Name_)
            {
                return BadRequest();
            }

            db.Entry(shopbridge_inventory_1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Shopbridge_inventory_1Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Shopbridge_inventory_1
        [ResponseType(typeof(Shopbridge_inventory_1))]
        public IHttpActionResult PostShopbridge_inventory_1(Shopbridge_inventory_1 shopbridge_inventory_1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shopbridge_inventory_1.Add(shopbridge_inventory_1);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Shopbridge_inventory_1Exists(shopbridge_inventory_1.Name_))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shopbridge_inventory_1.Name_ }, shopbridge_inventory_1);
        }

        // DELETE: api/Shopbridge_inventory_1/5
        [ResponseType(typeof(Shopbridge_inventory_1))]
        public IHttpActionResult DeleteShopbridge_inventory_1(string id)
        {
            Shopbridge_inventory_1 shopbridge_inventory_1 = db.Shopbridge_inventory_1.Find(id);
            if (shopbridge_inventory_1 == null)
            {
                return NotFound();
            }

            db.Shopbridge_inventory_1.Remove(shopbridge_inventory_1);
            db.SaveChanges();
            DeleteFiles(shopbridge_inventory_1);
            return Ok(shopbridge_inventory_1);
        }

        public void DeleteFiles(Shopbridge_inventory_1 obj)
        {
            string filePath = "";
            if (!string.IsNullOrEmpty(obj.image_name))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Images/");
                if (File.Exists(filePath+obj.image_name))
                {
                    File.Delete(filePath + obj.image_name);
                }
                filePath = HttpContext.Current.Server.MapPath("~/Thumbnails/");
                if (File.Exists(filePath + obj.thumb_name))
                {
                    File.Delete(filePath + obj.thumb_name);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Shopbridge_inventory_1Exists(string id)
        {
            return db.Shopbridge_inventory_1.Count(e => e.Name_ == id) > 0;
        }
    }
}