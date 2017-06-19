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
    public class clientesController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/clientes
        public IQueryable<cliente> Getcliente()
        {
            return db.cliente;
        }

        // GET: api/clientes/5
        [ResponseType(typeof(cliente))]
        public IHttpActionResult Getcliente(string id)
        {
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcliente(string id, cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.idcliente)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
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

        // POST: api/clientes
        [ResponseType(typeof(cliente))]
        public IHttpActionResult Postcliente(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cliente.Add(cliente);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (clienteExists(cliente.idcliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.idcliente }, cliente);
        }

        // DELETE: api/clientes/5
        [ResponseType(typeof(cliente))]
        public IHttpActionResult Deletecliente(string id)
        {
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(string id)
        {
            return db.cliente.Count(e => e.idcliente == id) > 0;
        }
    }
}