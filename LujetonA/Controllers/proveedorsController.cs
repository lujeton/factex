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
    public class proveedorsController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/proveedors
        public IQueryable<object> Getproveedor()
        {
            return db.proveedor.Select(x => new {
                nit = x.nit,
                razon_social = x.razon_social,
                representante = x.representante,
                direccion = x.direccion,
                ciudad = x.ciudad,
                telefono = x.telefono,
                email = x.email,
                estado = x.estado
            });
        }

        // GET: api/proveedors/5
        [ResponseType(typeof(proveedor))]
        public IHttpActionResult Getproveedor(string id)
        {
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor);
        }

        // PUT: api/proveedors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproveedor(string id, proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proveedor.nit)
            {
                return BadRequest();
            }

            db.Entry(proveedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!proveedorExists(id))
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

        // POST: api/proveedors
        [ResponseType(typeof(proveedor))]
        public IHttpActionResult Postproveedor(proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.proveedor.Add(proveedor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (proveedorExists(proveedor.nit))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = proveedor.nit }, proveedor);
        }

        // DELETE: api/proveedors/5
        [ResponseType(typeof(proveedor))]
        public IHttpActionResult Deleteproveedor(string id)
        {
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            db.proveedor.Remove(proveedor);
            db.SaveChanges();

            return Ok(proveedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool proveedorExists(string id)
        {
            return db.proveedor.Count(e => e.nit == id) > 0;
        }
    }
}