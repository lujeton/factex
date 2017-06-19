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
    public class datos_empresaController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/datos_empresa
        public IQueryable<datos_empresa> Getdatos_empresa()
        {
            return db.datos_empresa;
        }

        // GET: api/datos_empresa/5
        [ResponseType(typeof(datos_empresa))]
        public IHttpActionResult Getdatos_empresa(string id)
        {
            datos_empresa datos_empresa = db.datos_empresa.Find(id);
            if (datos_empresa == null)
            {
                return NotFound();
            }

            return Ok(datos_empresa);
        }

        // PUT: api/datos_empresa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdatos_empresa(string id, datos_empresa datos_empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != datos_empresa.nit)
            {
                return BadRequest();
            }

            db.Entry(datos_empresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!datos_empresaExists(id))
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

        // POST: api/datos_empresa
        [ResponseType(typeof(datos_empresa))]
        public IHttpActionResult Postdatos_empresa(datos_empresa datos_empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.datos_empresa.Add(datos_empresa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (datos_empresaExists(datos_empresa.nit))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = datos_empresa.nit }, datos_empresa);
        }

        // DELETE: api/datos_empresa/5
        [ResponseType(typeof(datos_empresa))]
        public IHttpActionResult Deletedatos_empresa(string id)
        {
            datos_empresa datos_empresa = db.datos_empresa.Find(id);
            if (datos_empresa == null)
            {
                return NotFound();
            }

            db.datos_empresa.Remove(datos_empresa);
            db.SaveChanges();

            return Ok(datos_empresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool datos_empresaExists(string id)
        {
            return db.datos_empresa.Count(e => e.nit == id) > 0;
        }
    }
}