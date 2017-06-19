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
    public class detalle_facturaController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/detalle_factura
        public IQueryable<detalle_factura> Getdetalle_factura()
        {
            return db.detalle_factura;
        }

        // GET: api/detalle_factura/5
        [ResponseType(typeof(detalle_factura))]
        public IHttpActionResult Getdetalle_factura(int id)
        {
            detalle_factura detalle_factura = db.detalle_factura.Find(id);
            if (detalle_factura == null)
            {
                return NotFound();
            }

            return Ok(detalle_factura);
        }

        // PUT: api/detalle_factura/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdetalle_factura(int id, detalle_factura detalle_factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalle_factura.idFactura)
            {
                return BadRequest();
            }

            db.Entry(detalle_factura).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detalle_facturaExists(id))
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

        // POST: api/detalle_factura
        [ResponseType(typeof(detalle_factura))]
        public IHttpActionResult Postdetalle_factura(detalle_factura detalle_factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.detalle_factura.Add(detalle_factura);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (detalle_facturaExists(detalle_factura.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = detalle_factura.idFactura }, detalle_factura);
        }

        // DELETE: api/detalle_factura/5
        [ResponseType(typeof(detalle_factura))]
        public IHttpActionResult Deletedetalle_factura(int id)
        {
            detalle_factura detalle_factura = db.detalle_factura.Find(id);
            if (detalle_factura == null)
            {
                return NotFound();
            }

            db.detalle_factura.Remove(detalle_factura);
            db.SaveChanges();

            return Ok(detalle_factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detalle_facturaExists(int id)
        {
            return db.detalle_factura.Count(e => e.idFactura == id) > 0;
        }
    }
}