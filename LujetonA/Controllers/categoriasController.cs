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
    public class categoriasController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/categorias
        public IQueryable<object> Getcategoria()
        {
            return db.categoria.Select(x => new {
                id = x.id,
                nombre = x.nombre
            });
        }

        // GET: api/categorias/5
        [ResponseType(typeof(categoria))]
        public IHttpActionResult Getcategoria(string id)
        {
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        // PUT: api/categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategoria(string id, categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.id)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoriaExists(id))
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

        // POST: api/categorias
        [ResponseType(typeof(categoria))]
        public IHttpActionResult Postcategoria(categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categoria.Add(categoria);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (categoriaExists(categoria.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categoria.id }, categoria);
        }

        // DELETE: api/categorias/5
        [ResponseType(typeof(categoria))]
        public IHttpActionResult Deletecategoria(string id)
        {
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.categoria.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoriaExists(string id)
        {
            return db.categoria.Count(e => e.id == id) > 0;
        }
    }
}