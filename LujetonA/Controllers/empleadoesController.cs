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
    public class empleadoesController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();
        
        // GET: api/empleadoes
        public IQueryable<empleado> Getempleado()
        {
            return db.empleado;
        }

        // GET: api/empleadoes/5
        [ResponseType(typeof(empleado))]
        public IHttpActionResult Getempleado(string id)
        {
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // PUT: api/empleadoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putempleado(string id, empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.id_empleado)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!empleadoExists(id))
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

        // POST: api/empleadoes
        [ResponseType(typeof(empleado))]
        public IHttpActionResult Postempleado(empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.empleado.Add(empleado);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (empleadoExists(empleado.id_empleado))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empleado.id_empleado }, empleado);
        }

        // DELETE: api/empleadoes/5
        [ResponseType(typeof(empleado))]
        public IHttpActionResult Deleteempleado(string id)
        {
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.empleado.Remove(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool empleadoExists(string id)
        {
            return db.empleado.Count(e => e.id_empleado == id) > 0;
        }
    }
}