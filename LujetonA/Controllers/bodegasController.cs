using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LujetonA.Models;
using System.Web.Http.Cors;

namespace LujetonA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class bodegasController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/bodegas
        public IQueryable<object> Getbodega()
        {
            return db.bodega.Select(x => new {
                id = x.id,
                nombre = x.nombre
            });
        }

        // GET: api/bodegas/5
        [ResponseType(typeof(bodega))]
        public IHttpActionResult Getbodega(int id)
        {
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return NotFound();
            }

            return Ok(bodega);
        }

        // PUT: api/bodegas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbodega(int id, bodega bodega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bodega.id)
            {
                return BadRequest();
            }

            db.Entry(bodega).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bodegaExists(id))
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

        // POST: api/bodegas
        [ResponseType(typeof(bodega))]
        public IHttpActionResult Postbodega(bodega bodega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bodega.Add(bodega);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (bodegaExists(bodega.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bodega.id }, bodega);
        }

        // DELETE: api/bodegas/5
        [ResponseType(typeof(bodega))]
        public IHttpActionResult Deletebodega(int id)
        {
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return NotFound();
            }

            db.bodega.Remove(bodega);
            db.SaveChanges();

            return Ok(bodega);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bodegaExists(int id)
        {
            return db.bodega.Count(e => e.id == id) > 0;
        }
    }
}