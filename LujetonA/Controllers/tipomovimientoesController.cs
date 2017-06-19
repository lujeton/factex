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
    public class tipomovimientoesController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/tipomovimientoes
        public IQueryable<object> Gettipomovimiento()
        {
            return db.tipomovimiento.Select(x => new {
                idTMovimiento = x.idTMovimiento,
                descripcion = x.descripcion
            });
        }

        // GET: api/tipomovimientoes/5
        [ResponseType(typeof(tipomovimiento))]
        public IHttpActionResult Gettipomovimiento(string id)
        {
            tipomovimiento tipomovimiento = db.tipomovimiento.Find(id);
            if (tipomovimiento == null)
            {
                return NotFound();
            }

            return Ok(tipomovimiento);
        }

        // PUT: api/tipomovimientoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttipomovimiento(string id, tipomovimiento tipomovimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipomovimiento.idTMovimiento)
            {
                return BadRequest();
            }

            db.Entry(tipomovimiento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipomovimientoExists(id))
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

        // POST: api/tipomovimientoes
        [ResponseType(typeof(tipomovimiento))]
        public IHttpActionResult Posttipomovimiento(tipomovimiento tipomovimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipomovimiento.Add(tipomovimiento);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tipomovimientoExists(tipomovimiento.idTMovimiento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipomovimiento.idTMovimiento }, tipomovimiento);
        }

        // DELETE: api/tipomovimientoes/5
        [ResponseType(typeof(tipomovimiento))]
        public IHttpActionResult Deletetipomovimiento(string id)
        {
            tipomovimiento tipomovimiento = db.tipomovimiento.Find(id);
            if (tipomovimiento == null)
            {
                return NotFound();
            }

            db.tipomovimiento.Remove(tipomovimiento);
            db.SaveChanges();

            return Ok(tipomovimiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipomovimientoExists(string id)
        {
            return db.tipomovimiento.Count(e => e.idTMovimiento == id) > 0;
        }
    }
}