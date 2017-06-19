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
    public class carterasController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/carteras
        public IQueryable<object> Getcartera()
        {
            return db.cartera.Select(x => new {
                idcartera = x.idcartera,
                abono = x.abono,
                codigo_factura = x.factura.id
            });
        }

        // GET: api/carteras/5
        [ResponseType(typeof(cartera))]
        public IHttpActionResult Getcartera(int id)
        {
            cartera cartera = db.cartera.Find(id);
            if (cartera == null)
            {
                return NotFound();
            }

            return Ok(cartera);
        }

        // PUT: api/carteras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcartera(int id, cartera cartera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cartera.idcartera)
            {
                return BadRequest();
            }

            db.Entry(cartera).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!carteraExists(id))
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

        // POST: api/carteras
        [ResponseType(typeof(cartera))]
        public IHttpActionResult Postcartera(cartera cartera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cartera.Add(cartera);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (carteraExists(cartera.idcartera))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cartera.idcartera }, cartera);
        }

        // DELETE: api/carteras/5
        [ResponseType(typeof(cartera))]
        public IHttpActionResult Deletecartera(int id)
        {
            cartera cartera = db.cartera.Find(id);
            if (cartera == null)
            {
                return NotFound();
            }

            db.cartera.Remove(cartera);
            db.SaveChanges();

            return Ok(cartera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool carteraExists(int id)
        {
            return db.cartera.Count(e => e.idcartera == id) > 0;
        }
    }
}